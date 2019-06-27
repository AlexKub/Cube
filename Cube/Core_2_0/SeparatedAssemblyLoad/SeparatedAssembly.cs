using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core_2_0;
using System;
using System.IO;
using System.Reflection;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Обёртка над сборкой, загруженной в отдельный домен
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class SeparatedAssembly : IDisposable
    {
        bool m_disposed = false;

        readonly object m_locker = new object();

        ExternalAppDomainAssembly m_externalAsmWrapper;

        #region Properties

        #region internal

        /// <summary>
        /// Обёртка над AppDomain, в который выгружен сборка
        /// </summary>
        public SeparatedAppDomain Domain { get; set; }

        /// <summary>
        /// Данные о сборке
        /// </summary>
        public AssemblyName AssemblyName { get; set; }

        /// <summary>
        /// Хеш файла библиотеки
        /// </summary>
        internal string FileHash { get; set; } //для случая, когда подгружаются одинаковые сборки, но разные файлы

        #endregion

        /// <summary>
        /// Последнее исключение
        /// </summary>
        public Exception LastEx { get; internal set; }

        /// <summary>
        /// Информация о файле сборки
        /// </summary>
        public FileInfo FileInfo { get; private set; }

        Exception pv_LoadException = null;
        /// <summary>
        /// Исключение при загрузке бибилотеки в домен
        /// </summary>
        public Exception LoadException
        {
            get { return pv_LoadException; }
            private set
            {
                pv_LoadException = value;
                LastEx = value;
            }
        }

        /// <summary>
        /// Флаг успешной загрузки библиотеки
        /// </summary>
        public bool Loaded { get { return LoadException == null; } }

        /// <summary>
        /// Флаг наличия исключения при работе с библиотекой
        /// </summary>
        public bool HasException { get { return LastEx != null; } }

        #endregion

        internal SeparatedAssembly(ExternalAppDomainAssembly asm, SeparatedAppDomain domain)
        {
            m_externalAsmWrapper = asm;
            Domain = domain;
        }

        internal SeparatedAssembly(Exception ex) { LoadException = ex; }

        /// <summary>
        /// Загрузка указанной сборки в отдельном домене в отдельном домене
        /// </summary>
        /// <param name="assembly">Файл загружаемой сборки</param>
        /// <returns>Возвращает ссылку на сборку из отдельного домена</returns>
        public static SeparatedAssembly Load(FileInfo assembly)
        {
            var session = SeparatedAssemblyManager.OpenNewSession();
            var sepAsm = session.LoadAssembly(assembly);

            return sepAsm;
        }

        #region ExternalAppDomainAssembly

        /// <summary>
        /// Получение токена открытого ключа у сборки
        /// </summary>
        /// <returns>Возвращает токен публичного ключа или null</returns>
        public string GetPublicKeyToken()
        {
            if (m_externalAsmWrapper == null)
                return string.Empty;

            if (FileInfo == null)
                return string.Empty;

            return Utilites.GetPublicKeyToken(FileInfo.FullName);
        }

        /// <summary>
        /// Получение первого имени контрола для NAV'а в текущей библиотеке
        /// </summary>
        /// <param name="logEx">Логирование исключений. По умолчанию отключено, т.к. бывают невъебенные логи, но в целом информация бессмысленна: нет аттрибута/типа - не контрол</param>
        /// <returns>Возвращает значение первого атрибута 'ControlAddInExport' в библиотеке</returns>
        public string GetControlAddinName(bool logEx = false)
        {
            if (m_externalAsmWrapper == null)
                return string.Empty;

            return GetResultFromMarshaled(m_externalAsmWrapper?.GetControlAddinName(logEx));
        }

        /// <summary>
        /// Проверка сборки на наличие публичного ключа и аттрибута control-addin'а
        /// </summary>
        /// <returns>Возвращает флаг наличия в сборке контрола для NAV'а и публичного ключа</returns>
        public bool IsControlAddinAssembly()
        {
            if (m_externalAsmWrapper == null)
                return false;

            return GetResultFromMarshaled(m_externalAsmWrapper.IsControlAddinAssembly());
        }

        /// <summary>
        /// Получение версии .NET сборки
        /// </summary>
        /// <returns>Возвращает версию .NET для сборки</returns>
        public RuntimeVersions GetDotNetVersion()
        {
            if (m_externalAsmWrapper == null)
                return RuntimeVersions.UnKnown;

            return GetResultFromMarshaled(m_externalAsmWrapper.GetDotNetVersion());
        }

        T GetResultFromMarshaled<T>(MarshalingResult<T> marshalRes)
        {
            if (marshalRes.HasError)
                LastEx = new Exception("При выполнении внешней операции возникло исключение: " + marshalRes.ErrorText);

            return marshalRes.Result;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Событие при освобождении ресурсов библиотеки
        /// </summary>
        public event Action<SeparatedAssembly> UnLoaded;

        /// <summary>
        /// Логика освобождения ресурсов
        /// </summary>
        public void Dispose()
        {
            try
            {
                /*
                 * даём знать, что юзер захотел "освободить сборку"
                 * 
                 * поскольку фактически сборки из домена выгружать нельзя,
                 * то реальный Dispose имеет смысл только при выгрузке домена
                 * поэтому решение о нём принимаем на стороне домена,
                 * а здесь лишь уведомляем, что она условно "выгружена"
                 */
                UnLoaded?.Invoke(this);
            }
            catch (Exception ex)
            {
                if (Utilites.m_loger != null) //проверяем, т.к. возможен вызов через деструктор
                    Utilites.m_loger.Log("Возникло исключение при освобождении ресурсов SeparatedAssembly", ex
                        , new LogParameter("Имя файла сборки", GetAssemblyFileName()));
            }
        }

        internal void DomainDisposed()
        {
            lock (m_locker)
            {
                if (m_disposed)
                    return;
                else
                    m_disposed = true;

                m_externalAsmWrapper = null;
                Domain = null;
                LoadException = null;
                LastEx = null;
                AssemblyName = null;
                FileInfo = null;
                m_disposed = true;
            }

            //на случай если сборка была "потеряна и забыта"
            //и методы был вызван из деструктора
            //уведомляем домен, что сборка освобождена
            //вдруг он ещё есть :)
            try
            {
                UnLoaded?.Invoke(this);
            }
            catch { }
        }

        /// <summary>
        /// Деструктор, на случай если забыли выгрузить
        /// </summary>
        ~SeparatedAssembly()
        {
            DomainDisposed();
        }

        #endregion

        string GetAssemblyFileName()
        {
            return FileInfo == null ? "NULL_INFO" : FileInfo.FullName;
        }

        string DebugDisplay()
        {
            return AssemblyName?.FullName ?? "NULL";
        }
    }
}
