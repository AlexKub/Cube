using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Логика разрешения ссылок на файлы библиотек
    /// </summary>
    internal class AppDomainResolver : MarshalByRefObject
    {
        static AppDomainResolver AsmResover;

        /*
         * Поскольку AppDomain не умеет(!О_о) загружать DLL по удалённому пути
         * приходится делать отдельный класс-загрузчик с подпиской на событие Разрешение сборки
         * через обработку которого, прописывать пути к подгружаемым файлам библиотек
         *
         * гайд найден здесь https://stackoverflow.com/a/23689445/5454565
         * 
         */

        /// <summary>
        /// Экземпляр для текущего AppDomain
        /// </summary>
        public static AppDomainResolver Instance
        {
            get
            {
                if (AsmResover == null)
                    AsmResover = new AppDomainResolver();

                return AsmResover;
            }
        }

        /// <summary>
        /// Базовый каталог, в котором следует искать сборки
        /// </summary>
        private string ApplicationBase { get; set; } //если сборка не найдена по имени среди Binded, искать здесь

        /// <summary>
        /// Коллекция путей, где лежат файлы загружаемых сборок
        /// </summary>
        readonly Dictionary<string, string> m_asmPathes = new Dictionary<string, string>(); //имя сборки | файл сборки

        /// <summary>
        /// Создание нового обработчика ссылок (Singleton)
        /// </summary>
        private AppDomainResolver()
        {
            //берём основную папку (где искать сборки)
            //из свойства, которое заполняли при инициализации текущего AppDomain
            ApplicationBase = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;

            //подписываемся на загрузку сборок в AppDomain
            AppDomain.CurrentDomain.AssemblyResolve += Resolve;
        }

        /// <summary>
        /// Обработка события Разрешения ссылки на сборка в AppDomain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly Resolve(object sender, ResolveEventArgs args)
        {
            /*
             * Обработка события Разрешения ссылки на сборку в AppDomain
             * 
             * попадаем сюда при Assembly.LoadFrom и прочем подобном
             * 
             * разрешение ссылок идёт с корневых зависимостей
             * по-этому, если мы используем сборку с зависимостями
             * то сюда мы попадём, в первую очередь, для зависимых сборок
             * 
             * а, т.к. у зависимых сборок имена файлов другие,
             * то мы не находим по имени сборки соответствующего пути
             * который был соранён через this.Bind используемой сборки
             * (я надеюсь, что был сохранён)
             * по-этому нужно Bind'ить в т.ч. все зависимые сборки
             * 
             */
            AssemblyName assemblyName = new AssemblyName(args.Name);
            var asmName = assemblyName.Name;
            var asmPath = ApplicationBase;
            var pathName = string.Empty;

            if (m_asmPathes.ContainsKey(asmName))
            {
                asmPath = m_asmPathes[asmName];

                if (string.IsNullOrEmpty(asmPath))
                    asmPath = ApplicationBase;

                //предполагается, что здесь полный путь к файлу
                pathName = asmPath;
            }
            else
            {
                string fileName = string.Format("{0}.dll", asmName);

                pathName = Path.Combine(ApplicationBase, fileName);
            }

            return Assembly.LoadFile(pathName);
        }

        /// <summary>
        /// Привязка пути к файлу библиотеки к имени библиотеки
        /// </summary>
        /// <param name="pathName">Полный путь к файлу библиотеки</param>
        internal void Bind(string pathName)
        {
            /*
             * при Resolve необходимо знать, где искать
             * 
             * поскольку библиотеки могут быть в разных папках
             * (для манифеста обновления)
             * то для каждой из библиотек необходимо указать папку, в которой её искать
             * 
             * поскольку в Resolve мы имеем только имя библиотеки
             * ты приходится делать привязку по нему
             * ожидая, что имя файла (без расширения) и етсь имя библиотеки
             */

            if (string.IsNullOrEmpty(pathName))
                return;

            Bind(pathName, AssemblyName.GetAssemblyName(pathName));
        }

        /// <summary>
        /// Привязка имени сборки к файлу, из которого её необходимо загрузить
        /// </summary>
        /// <param name="pathName">Путь к файлу сборки</param>
        /// <param name="asmName">Имя сборки</param>
        internal void Bind(string pathName, AssemblyName asmName)
        {
            if (string.IsNullOrEmpty(pathName))
                return;

            var name = asmName.Name;

            if (m_asmPathes.ContainsKey(name))
                return;
            else
            {
                //добавляем привязку сборки и файла, из которого её необходимо загрузить
                m_asmPathes.Add(name, pathName);
            }
        }
    }
}
