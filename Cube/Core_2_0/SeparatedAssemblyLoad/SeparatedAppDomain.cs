using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core_2_0;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Policy;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Обёртка над отдельным AppDomian для загрузки сборок
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class SeparatedAppDomain : IDisposable
    {
        /*
         * обёртка над отдельным AppDomain для файлов сборок, которые необходимо загрузить отдельно.
         * 
         * problem:
         * при проверке файлов сборок на всякую фигню приходится их подгружать в домен.
         * если грузить в текущий, то они залипают: при закрытии приложения, оно остаётся висеть в процессах,
         * а также не всегда получаешь корректную ссылку на сборку через reflection, т.к. в текущий домен может быть загружена другая версия той же сборки
         */

        const string DomainNamePrefix = "SeparatedDomain_";

        static int CreatedDomainsCounter = 0;
        static ILoger m_loger = Utilites.m_loger;

        static readonly string CoreAssemblyName = typeof(SeparatedAppDomain).Assembly.FullName;

        AppDomain m_domain;

        ObjectHandle m_AssemblyResolverHandle;

        bool m_disposed = false;

        /// <summary>
        /// Флаг, что домен был создан явно
        /// </summary>
        bool m_Managed = false;
        /*
         * если домен был создан явно,
         * то не освобождаем домен при нулевом счётчике сборок
         * 
         * если пользователь сам создал домен, 
         * то пусть сам и даёт знать, когда его нужно освободить
         */

        readonly string m_generatedName;

        readonly object m_locker = new object();

        /// <summary>
        /// Сборки, загруженные в текущий домен
        /// </summary>
        readonly Dictionary<SeparatedAssembly, bool> m_loadedAssemblies = new Dictionary<SeparatedAssembly, bool>();

        /// <summary>
        /// Сборки, загруженные через этот экземпляр, но по факту в другой домен (не явно)
        /// </summary>
        readonly Dictionary<SeparatedAssembly, bool> m_relatedAssemblies = new Dictionary<SeparatedAssembly, bool>();

        /*
         * если юзер Dispose'ит библиотеку явно, 
         * то считаем её выгруженной из домена
         * 
         * поскольку фактически сборку из домена выгрузить нельзя
         * коллекцию сборок не трём до последнего
         * но ведём учёт "выгруженных пользователем"
         * 
         * если все сборки домена явно "выгружены" пользователем
         * (был вызван Dispose у сборки)
         * то и домен можно выгружать
         * 
         * сделано на случай, если сборку юзер за'Dispose'ил
         * а потом опять подгрузил
         * но при этом домен не релизнулся (были другие сборки)
         * тогда, при проворки свободного домена, этот даст знак, что в нём сборки нет
         * т.к. она была удалена из загруженных при Dispose
         * но фактически сборка в AppDomain есть
         * это приведёт к загрузке двух (и более) одинаковых сборок в домен
         */
        /// <summary>
        /// Счётчик высвобожденных библиотек
        /// </summary>
        int m_assemblyDisposeCounter;

        #region properties

        #region internal

        /// <summary>
        /// Локальный ID домена (!= AppDomain.Id)
        /// </summary>
        internal int ID { get; private set; }

        /// <summary>
        /// ID AppDomain, куда выгржена сборка
        /// </summary>
        public int AppDomainId { get { return m_domain?.Id ?? 0; } }

        /// <summary>
        /// Сессия домена
        /// </summary>
        internal DomainSession Session { get; private set; }

        #endregion

        #endregion

        #region ctor

        /// <summary>
        /// Новый AppDomain
        /// </summary>
        private SeparatedAppDomain()
        {
            //ведём отдельный ID'шник для генерации уникальных AppDomainNames
            ID = ++CreatedDomainsCounter;

            m_generatedName = DomainNamePrefix + ID.ToString();

#if Test

            /* 
             * 
             * для автотестов добавляем обработку проблем при поиске загружаемых сборок
             * 
             * Описание проблемы:
             * Т.к. тестовые сборки (aka SomeProject.Tests) запускаются каким-то Богом забытым exe'шником, то Core рядом с ним нету (естественно)
             * (не удивлюсь, если ещё и временным)
             * 
             * отсюда великий разрешатель ссылок .Net, при загрузке ЭТОЙ.dll
             * (в которой ты читаешь этот комментарий), 
             * теряется и плюёт Exception
             * 
             * приходится делать "третью" сборку, в которой создавать класс с подпиской на разрешение ссылок у библиотек
             * в котором добавлять свою логику обработки ссылок
             * фактически, расширяя логику загрузки библиотек для нового AppDomain
             * 
             * подробнее: https://stackoverflow.com/a/23689445/5454565
             * и в комментариях в классе
             * 
             */

            //куда сбилден проект AppDomainResolver
            //сбилдил его в папку ко всем остальным, чтобы пользоваться одним методом
            var build = GetBuildDirectory();

            m_domain = CreateAppDomain(m_generatedName, build);

            var classNamespace = "AppDomainResolver"; //имя класса и namespace'a 
            //достаточно создать Instance
            // += Subscribe inside ®
            m_domain.CreateInstanceFrom(
                Path.Combine(build, $"{classNamespace}.dll"), //путь сборки третьей библиотеки
                $"{classNamespace}.{classNamespace}", //полное имя класса-помощника
                true,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                null, //new object[] { build }, //хрен знает зачем, пусть так
                null,
                null,
                null);

#else
            var appDomDir = AppDomain.CurrentDomain.BaseDirectory;

            m_domain = CreateAppDomain(m_generatedName, appDomDir);
#endif
        }

        internal SeparatedAppDomain(DomainSession session) : this() { Session = session; }



        #endregion

#if Test
        string GetBuildDirectory()
        {
            //папка сборки для запущенного проекта с тестами
            var test_debug = Directory.GetCurrentDirectory(); //Debug (or other)
            var bin = Directory.GetParent(test_debug); //bin
            var ecr = bin.Parent.Parent.Parent;
            var build = Path.Combine(ecr.FullName //ECR
                , "Build"); //ECR/Build

            return build;
        }
#endif

        /// <summary>
        /// Открывает отдельный домен для загрузки сборок
        /// </summary>
        /// <returns>Возвращает новый домен для проверки сборок</returns>
        public static SeparatedAppDomain Create()
        {
            var session = SeparatedAssemblyManager.OpenNewSession();

            var domain = session.CreateDomain();
            domain.m_Managed = true;

            return domain;
        }

        /// <summary>
        /// Общая логика создания AppDomain
        /// </summary>
        /// <param name="name">Имя домена (FriendlyName)</param>
        /// <param name="root">Корневой каталог для поиска связанных библиотек</param>
        /// <returns>Возвращает новый AppDomain с указанным именем</returns>
        AppDomain CreateAppDomain(string name, string root)
        {
            var setup = new AppDomainSetup
            {
                ApplicationName = name,

                //this! будет считано в обработчике событий разрешения ссылок на сборку! 
                //Все загружаемые во вспомогательные домены сборки кладём туда
                ApplicationBase = root,

                PrivateBinPath = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPathProbe = AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            };

            Evidence evidence = new Evidence(AppDomain.CurrentDomain.Evidence);

            return AppDomain.CreateDomain(name, evidence, setup);
        }

        /// <summary>
        /// Загрузка сборки в отдельный AppDomain
        /// </summary>
        /// <param name="assembly">Файл сборки</param>
        /// <returns>Возвращает обёртку над сборкой, загруженной в отдельный AppDomain</returns>
        public SeparatedAssembly Load(FileInfo assembly)
        {
            if (Session == null)
                return new SeparatedAssembly(new ObjectDisposedException("Не удалось загрузить библиотеку в отдельный AppDomain: Доменная сессия не инициализирована"));

            /*
             * в выборе домена полагаемся на сессию:
             * если такая сборка уже есть в текущем домене, то она откроет новый и его учтёт
             */
            var asm = Session.LoadAssembly(assembly);

            /* 
             * если сборка подгружена не явно в другой домен
             * (в этом домене сессия нашла схожую сборку и открыла новый / загрузила в другой свободный)
             * то ведём учёт ссылки в текущем
             * 
             * если пользователь захотел подгрузить сборку в контексте этого домена
             * то при его Dispose нужно выгрузить все домены, которые были открыты не явно
             */
            if (asm.Domain != this)
                lock (m_locker)
                {
                    m_assemblyDisposeCounter++;
                    asm.UnLoaded += OnAssemblyDispose;
                    m_relatedAssemblies.Add(asm, false);
                }

            return asm;
        }

        /// <summary>
        /// Проверка на наличие библиотеки в текущем домене
        /// </summary>
        /// <param name="name">имя сборки</param>
        /// <param name="hash">Hash файла сборки</param>
        /// <returns>Возвращает наличие подобной сборки в Домене</returns>
        internal LoadedState HasLoaded(AssemblyName name, string hash)
        {
            var shortName = name.Name;

            lock (m_locker)
                foreach (var asm in m_loadedAssemblies)
                {
                    var a = asm.Key; //загруженная сборка
                    //сборка с таким именем уже есть в текущем домене
                    if (a.AssemblyName.Name.Equals(shortName))
                    {
                        if (a.FileHash.Equals(hash))
                            return LoadedState.LoadedSame;
                        else
                            return LoadedState.LoadedDifferent;
                    }
                }

            return LoadedState.NotLoaded;
        }

        /// <summary>
        /// Получение сборки с таким же именем из домена
        /// </summary>
        /// <param name="name">Имя сборки</param>
        /// <returns>Возвращает первую сборку с таким же именем или null</returns>
        internal SeparatedAssembly GetAssembly(AssemblyName name)
        {
            var shortName = name.Name;

            lock (m_locker)
                foreach (var asm in m_loadedAssemblies)
                {
                    var a = asm.Key; //обёртка над загруженной сборкой
                    if (a.AssemblyName.Name.Equals(shortName))
                    {
                        if (asm.Value) //если сборка была ранее выгружена
                        {
                            //снимаем флаг "выгружена"
                            m_loadedAssemblies[a] = false;
                            //и !ВАЖНО! снижаем счётчик выгруженных сборок
                            //иначе домен никогда не выгрузится сам при рассинхронизации счётчика
                            m_assemblyDisposeCounter--;
                        }
                        return a;
                    }
                }

            return null;
        }

        /// <summary>
        /// Загрузка сборки в AppDomain
        /// </summary>
        /// <param name="fInfo">Информация о файле сборки</param>
        /// <param name="name">Имя сборки</param>
        /// <param name="hash">HASH файла</param>
        /// <returns>Возвращает обёртку над выгруженной сборкой</returns>
        internal SeparatedAssembly LoadAssembly(FileInfo fInfo, AssemblyName name, string hash)
        {
            lock (m_locker)
            {
                if (m_disposed)
                    throw new ObjectDisposedException($"Домен был выгружен перед загрузкой сборки '{(fInfo?.Name ?? "NULL")}'");

                //получаем экземпляр из другого AppDomain
                var extAsm = (ExternalAppDomainAssembly)m_domain.CreateInstanceAndUnwrap(CoreAssemblyName, ExternalAppDomainAssembly.TypeFullName);
                extAsm.BindAssembly(fInfo.FullName);

                var asm = new SeparatedAssembly(extAsm, this);
                asm.AssemblyName = name;
                asm.FileHash = hash;
                asm.UnLoaded += OnAssemblyDispose;
                m_loadedAssemblies.Add(asm, false);

                return asm;
            }
        }

        private static string GetNewName()
        {
            //генерируем уникальные имена, чтобы было хоть немного показательно
            var newName = DomainNamePrefix + (CreatedDomainsCounter++).ToString();

            return newName;
        }

        #region Dispose

        internal event Action<SeparatedAppDomain> Disposed;

        void OnAssemblyDispose(SeparatedAssembly asm)
        {
            if (asm == null)
                return;

            /*
             * попадаем сюда при явном вызове клиентом Dispose у сборки
             * 
             * поскольку её фактически из домена выгрузить нельзя
             * то ведём учёт "выгруженных"
             */
            var asmCount = -1;
            lock (m_locker)
            {
                if (m_loadedAssemblies.ContainsKey(asm))
                {
                    asmCount = UnLoadAsmRef(asm, m_loadedAssemblies);
                }
                else if (m_relatedAssemblies.ContainsKey(asm))
                {
                    asmCount = UnLoadAsmRef(asm, m_relatedAssemblies);
                }

            }

            //выгружаем текущий домен, если все его сборки помечены как "выгружена"
            if (asmCount == 0)
                if (!m_Managed) //если домен был создан не явно
                    Dispose(); //самоликвидируемся
        }

        int UnLoadAsmRef(SeparatedAssembly asm, Dictionary<SeparatedAssembly, bool> collection)
        {
            if (!collection[asm])
            {
                m_assemblyDisposeCounter++;
                collection[asm] = true;
            }

            //получаем разницу между загруженными сборками и "выгруженными"
            return collection.Count - m_assemblyDisposeCounter;
        }

        void DisposeCollection(Dictionary<SeparatedAssembly, bool> collection)
        {
            if (collection != null)
            {
                foreach (var lAsm in collection)
                {
                    var asm = lAsm.Key;
                    if (asm != null)
                    {
                        asm.UnLoaded -= OnAssemblyDispose;
                        m_assemblyDisposeCounter--;
                        //вызываем реальный Dispose сборки
                        asm.DomainDisposed();
                    }
                }

                collection.Clear();
            }
        }

        /// <summary>
        /// Выгрузка домена и освобождение связанных ресурсов (включая все сборки, загруженные в контескте этого домена)
        /// </summary>
        public void Dispose()
        {
            lock (m_locker)
                if (m_disposed)
                    return;
                else
                    m_disposed = true;

            /*
             * основная логика очистки ресурсов
             */
            try
            {

                lock (m_locker)
                {
                    if (m_domain != null)
                        AppDomain.Unload(m_domain);

                    //чистим ссылки в сборках текущего домена
                    DisposeCollection(m_loadedAssemblies);
                    //чистим ссылки в связанных сборках (про них читать public Load)
                    DisposeCollection(m_relatedAssemblies);

                    Session = null;
                }
            }
            catch (Exception ex)
            {
                if (m_loger != null)
                    m_loger.Log("Возникло исключение при освобождении ресурсов домена", ex
                        , new LogParameter("Имя домена", m_generatedName));
            }
            finally
            {
                Disposed?.Invoke(this);
            }
        }

        /// <summary>
        /// Деструктор SeparatedAppDomain на случай, если забыли закрыть
        /// </summary>
        ~SeparatedAppDomain()
        {
            //на случай, если забыли закрыть домены            
            Dispose();
        }

        #endregion


        string DebugDisplay()
        {
            return $"{ID.ToString()} - {AppDomainId.ToString()} | {m_generatedName}";
        }
    }
}
