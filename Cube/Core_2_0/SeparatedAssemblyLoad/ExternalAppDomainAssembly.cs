using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core_2_0;
using System;
using System.IO;
using System.Reflection;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Обёртка, используемая в другом AppDomain
    /// </summary>
    internal class ExternalAppDomainAssembly : MarshalByRefObject
    {
        /*
         * здесь реализовываем методы для вызова загруженной dll
         * 
         * этот код должен выполняться в другом AppDomain
         */

        //не Marshal'able - не даём видеть
        Assembly m_assembly;

        string m_filePathName;

        /// <summary>
        /// Библиотека RTC, в которой объявлен аттрибут, используемый для маркировки классов-Add-In'ов
        /// </summary>
        const string ExtesibilityName = "Microsoft.Dynamics.Framework.UI.Extensibility.dll";

        /// <summary>
        /// Библиотека, используемая в контролах NAV'а
        /// </summary>
        static readonly string m_ExtesibilityDLL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ExtesibilityName);

        /// <summary>
        /// Полное имя текущего типа
        /// </summary>
        internal static readonly string TypeFullName = typeof(ExternalAppDomainAssembly).FullName;

        public ExternalAppDomainAssembly() { } //конструктор для AppDomain.CreateInstanceAndUnwrap

        /// <summary>
        /// Загрузка библиотеки в AppDomain и привязка к текущему экземпляру
        /// </summary>
        /// <param name="pathName">Полный путь к файлу подгружаемой библиотеки</param>
        internal void BindAssembly(string pathName)
        {
            m_filePathName = pathName;

            //учитываем каталог сборки
            //после этого, сборка загрузится не явно по указанному пути при первом обращении к типу из сборки
            AppDomainResolver.Instance.Bind(pathName);

            //учитываем каталог для всех зависимых сборок
            BindReferences(pathName);

            //после учёта всех зависимостей, можно грузить саму сборку
            m_assembly = Assembly.LoadFrom(pathName);
        }

        private void BindReferences(string pathName)
        {
            //основная сборка в текущем контексте (в рамках этого метода)

            var contextAssembly = Assembly.ReflectionOnlyLoadFrom(pathName);
            /*
             * П.С.
             * ! LoadFrom тут не делаем ! 
             * (т.к. это начнёт грузить зависимые сборки по стандартной логике)
             * 
             * А т.к. мы взяли эту сборки хз откуда,
             * то и грузить зависимые сборки надо оттуда же (как минимум),
             * но стандартный загрузчик сборок забьёт на это болт
             * а наш ничего ещё об этом пути не знает 
             * (и не узнает, пока явно не указать)
             * 
             * по этому, прежде чем грузить сборку,
             * необходимо сначала проставить адреса файлов всех зависимых сборок
             * для переопределённой логики загрузки сборок
             * 
             */

            //получаем все зависимые сборки
            var references = contextAssembly.GetReferencedAssemblies();

            if (references == null || references.Length == 0)
                return;

            //папка, в которой лежит основная сборка
            //ожидаем, что все зависимые сборки лежат рядом
            //TO DO: иное логикой не предусмотрено
            var contextPath = Path.GetDirectoryName(pathName);
            var resolver = AppDomainResolver.Instance;
            for (int i = 0; i < references.Length; i++)
            {
                var asmName = references[i];

                //указываем файлы, из которых нужно будет загрузить зависимые сборки
                var asmFile = Path.Combine(contextPath, $"{asmName.Name}.dll");

                //т.к. в ссылках указаны и .NET сборки (да и вообще чего угодно, что может быть в GAC)
                //имеет смысл указывать привязки сборок только к тем файлам, которое реально есть
                //какой-то будет не доставать - что ж, печально
                if (File.Exists(asmFile))
                    resolver.Bind(Path.Combine(contextPath, $"{asmName.Name}.dll"), asmName);
            }
        }

        /// <summary>
        /// Получение токена открытого ключа у сборки
        /// </summary>
        /// <returns>Возвращает токен публичного ключа или null</returns>
        [Obsolete("Использовать версию, не требующую выгрузки в отдельный домен: 'Utils.GetPublicKeyToken'")]
        public MarshalingResult<string> GetPublicKeyToken()
        {
            try
            {
                var bytes = m_assembly.GetName().GetPublicKeyToken();

                if (bytes == null || bytes.Length == 0)
                    return new MarshalingResult<string>(string.Empty);

                var publicKeyToken = string.Empty;
                for (int i = 0; i < bytes.GetLength(0); i++)
                    publicKeyToken += string.Format("{0:x2}", bytes[i]);

                return new MarshalingResult<string>(publicKeyToken);
            }
            catch (Exception ex)
            {
                Utilites.m_loger.Log("Возникло исключение при попытке получения публичного ключа сборки", ex
                    , new LogParameter("Имя сборки", m_assembly == null ? "NULL" : m_assembly.FullName));

                return new MarshalingResult<string>(string.Empty, ex.Message);
            }
        }

        /// <summary>
        /// Получение первого имени контрола для NAV'а в текущей библиотеке
        /// </summary>
        /// <param name="logEx">Логирование исключений. По умолчанию отключено, т.к. бывают невъебенные логи, но в целом информация бессмысленна: нет аттрибута/типа - не контрол</param>
        /// <returns></returns>
        public MarshalingResult<string> GetControlAddinName(bool logEx = false)
        {
            try
            {
                object[] customAttrs;
                foreach (var type in m_assembly.GetExportedTypes())
                {
                    /*
                    тут не явно подгружается библиотека Microsoft.Dynamics.Framework.UI.Extensibility.dll, в которой находится аттрибут
                    нет библиотеки - нет проверок
                    */
                    customAttrs = type.GetCustomAttributes(false);

                    if (customAttrs == null || customAttrs.Length == 0)
                        continue;

                    foreach (var attr in customAttrs)
                    {
                        var attrType = attr.GetType();
                        if (attrType.Name.Equals("ControlAddInExportAttribute")) //имя аттрибута для контролов
                        {
                            var nameProperty = attrType.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);

                            if (nameProperty != null)
                            {
                                //получаем имя свойства через рефлексию, чтобы не тянуть библиотеку Microsoft.Dynamics.Framework.UI.Extensibility
                                var controlName = nameProperty.GetValue(attr, null) as string;
                                if (controlName == null)
                                    return new MarshalingResult<string>(string.Empty);
                                else
                                    return new MarshalingResult<string>(controlName);
                            }
                        }
                    }
                }

                return new MarshalingResult<string>(string.Empty);
            }

            catch (Exception ex)
            {
                if (ex is ReflectionTypeLoadException)
                {
                    var typeLoadException = ex as ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                }

                if (logEx)
                    Utilites.m_loger.Log("Возникло исключение при попытке получить имя контрола NAV'а из библиотеки. Возвращена пустая строка", ex
                        , new LogParameter("Имя файла сборки", m_assembly.FullName));

                return new MarshalingResult<string>(string.Empty, ex.Message);
            }
        }

        /// <summary>
        /// Проверка сборки на наличие публичного ключа и аттрибута control-addin'а
        /// </summary>
        /// <returns>Возвращает флаг наличия в сборке контрола для NAV'а и публичного ключа</returns>
        public MarshalingResult<bool> IsControlAddinAssembly()
        {
            try
            {
                if (m_assembly != null)
                {
                    var addinName = GetControlAddinName(true);

                    if (!string.IsNullOrEmpty(addinName.Result))
                        return new MarshalingResult<bool>(true);
                    else if (addinName.HasError)
                        new MarshalingResult<bool>(false, addinName.ErrorText);
                }

                return new MarshalingResult<bool>(false);
            }
            catch (Exception ex)
            {
                return new MarshalingResult<bool>(false, ex.Message);
            }
        }

        /// <summary>
        /// Получение основной версии .NET, под которую была собрана библиотека
        /// </summary>
        /// <returns></returns>
        public MarshalingResult<RuntimeVersions> GetDotNetVersion()
        {
            if (m_assembly == null)
                return new MarshalingResult<RuntimeVersions>(RuntimeVersions.UnKnown, "Отсутствует ссылка на сборку");

            try
            {
                var strVersion = m_assembly.ImageRuntimeVersion;

                var parts = strVersion.Split('.');

                var mainVersion = parts[0];

                int version = 0;
                int.TryParse(mainVersion[1].ToString(), out version);

                var res = RuntimeVersions.UnKnown;
                switch (version)
                {
                    case 1:
                        res = RuntimeVersions.v1;
                        break;
                    case 2:
                        res = RuntimeVersions.v2;
                        break;
                    case 3:
                        res = RuntimeVersions.v3;
                        break;
                    case 4:
                        res = RuntimeVersions.v4;
                        break;
                    default:
                        res = RuntimeVersions.UnKnown;
                        break;
                }

                return new MarshalingResult<RuntimeVersions>(res);
            }
            catch (Exception ex)
            {
                return new MarshalingResult<RuntimeVersions>(RuntimeVersions.UnKnown, ex.Message);
            }
        }

    }
}
