using Merlion.ECR.Update.Core.Log;
using System;
using System.Management;
using System.ServiceProcess;

namespace Merlion.ECR.Update.Core
{

    /// <summary>
    /// Общие методы управления службами Windows
    /// </summary>
    public partial class WinService
    {
        static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);
        static readonly ILoger m_loger = LogManager.GetDefaultLogSet(nameof(WinService));

        /// <summary>
        /// Возвращает таймаут по умолчанию для операций ожидания состония Службы
        /// </summary>
        public static TimeSpan DefaultTimeout
        {
            get { return _defaultTimeout; }
        }

        /// <summary>
        /// Перезапуск Службы
        /// </summary>
        /// <param name="serviceName">Имя службы</param>
        public static void RestartService(string serviceName)
        {
            RestartService(serviceName, _defaultTimeout);
        }
        /// <summary>
        /// Перезапуск Службы
        /// </summary>
        /// <param name="serviceName">Имя службы</param>
        /// <param name="timeout">Таймаут ожидания на запуск сервиса</param>
        public static void RestartService(string serviceName, TimeSpan timeout)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException("Имя Службы не может быть пустым");

            using (var sc = new ServiceController(serviceName))
            {
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        sc.Stop();
                        break;
                    case ServiceControllerStatus.Stopped:
                        break;
                    default:
                        m_loger.Log("При перезапуске, Cлужба находится в неожиданном состоянии", MessageType.Warning
                            , new LogParameter("Имя службы", serviceName)
                            , new LogParameter("Текущее состояние", sc.Status.ToString()));
                        break;
                }

                sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                sc.Start();
            }
        }

        /// <summary>
        /// Запуск Службы с таймаутом по умолчанию
        /// </summary>
        /// <param name="serviceName">Имя Службы</param>
        public static void StartService(string serviceName)
        {
            StartService(serviceName, _defaultTimeout);
        }
        /// <summary>
        /// Запуск Службы с указанным таймаутом ожидания
        /// </summary>
        /// <param name="serviceName">Имя Службы</param>
        /// <param name="timeout">Таймаут ожидания соответствующего состония Службы</param>
        public static void StartService(string serviceName, TimeSpan timeout)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException("Имя Службы не может быть пустым");

            using (var sc = new ServiceController(serviceName))
            {
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        return;
                    case ServiceControllerStatus.Stopped:
                        break;
                    default:
                        m_loger.Log("При запуске, Cлужба находится в неожиданном состоянии", MessageType.Warning
                            , new LogParameter("Имя службы", serviceName)
                            , new LogParameter("Текущее состояние", sc.Status.ToString()));
                        break;
                }

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
        }

        /// <summary>
        /// Остановка Службы с таймаутом по умолчанию
        /// </summary>
        /// <param name="serviceName">Имя Службы</param>
        public static void StopService(string serviceName)
        {
            StopService(serviceName, _defaultTimeout);
        }
        /// <summary>
        /// Остановка Службы с указанным таймаутом ожидания
        /// </summary>
        /// <param name="serviceName">Имя Службы</param>
        /// <param name="timeout">Таймаут ожидания соответствующего состония Службы</param>
        public static void StopService(string serviceName, TimeSpan timeout)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException("Имя Службы не может быть пустым");

            using (var sc = new ServiceController(serviceName))
            {
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        return;
                    case ServiceControllerStatus.Running:
                        break;
                    default:
                        m_loger.Log("При остановке, Cлужба находится в неожиданном состоянии", MessageType.Warning
                            , new LogParameter("Имя службы", serviceName)
                            , new LogParameter("Текущее состояние", sc.Status.ToString()));
                        break;
                }

                if (!sc.CanStop)
                    throw new InvalidOperationException($"Служба {serviceName} не может быть остановлена. CanStop: false");

                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
        }

        /// <summary>
        /// Проверка наличия и доступности Службы на локальном компьютере
        /// </summary>
        /// <param name="serviceName">Имя Службы</param>
        /// <returns>Возвращает флаг наличия и доступности для пользователя Службы на локальном компьютере</returns>
        public static bool Exist(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException("Имя сервиса не может быть пустым");

            //взято из https://stackoverflow.com/questions/4554116/how-to-check-if-a-windows-service-is-installed-in-c-sharp
            using (var sc = new ServiceController(serviceName)) //создание экземпляра по несуществующему имени допустимо
            {
                try
                {
                    return !string.IsNullOrEmpty(sc.DisplayName); //доступ к первому свойству должен вызывать исключение, если сервиса нет
                }
                catch
                {
                    return false;
                }
            }
        }

        public static void WaitForStatus(string serviceName, ServiceStatus status, TimeSpan timeout)
        {
            using (var sc = new ServiceController(serviceName)) //создание экземпляра по несуществующему имени допустимо
            {
                sc.WaitForStatus((ServiceControllerStatus)status, timeout);
            }
        }

        public static ServiceStatus GetStatus(string serviceName)
        {
            using (var sc = new ServiceController(serviceName)) //создание экземпляра по несуществующему имени допустимо
            {
                return (ServiceStatus)sc.Status;
            }
        }

        

    }
}
