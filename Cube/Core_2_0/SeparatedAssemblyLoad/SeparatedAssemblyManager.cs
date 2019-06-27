using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core_2_0;
using System;
using System.Collections.Generic;
using System.IO;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Управление логикой общих сборок
    /// </summary>
    internal static class SeparatedAssemblyManager
    {
        static Dictionary<int, List<SeparatedAppDomain>> m_createdDomains = new Dictionary<int, List<SeparatedAppDomain>>();

        static Dictionary<int, DomainSession> m_sessions = new Dictionary<int, DomainSession>();

        /// <summary>
        /// Счётчик сессий
        /// </summary>
        static int SessionID_Counter = 0;

        /// <summary>
        /// Объект ля синхронизации сессии
        /// </summary>
        internal static object ManagerLocker = new object();

        /// <summary>
        /// Загрузка библиотеки в новой сессии
        /// </summary>
        /// <param name="asmFile">Файл библиотеки</param>
        /// <returns>Возвращает домен, куда подгружена библиотека</returns>
        public static SeparatedAssembly LoadAssembly(FileInfo asmFile)
        {
            var newSessionId = OpenNewSession().ID;

            //создание новой сессии и загрузка сборки в её контексте
            return LoadAssembly(newSessionId, asmFile);
        }

        /// <summary>
        /// Загрузка библиотеки
        /// </summary>
        /// <param name="sessionID">ID сессии, к которой будет принадлежать домен</param>
        /// <param name="asmFile">Файл библиотеки</param>
        /// <returns>Возвращает домен, куда подгружена библиотека</returns>
        public static SeparatedAssembly LoadAssembly(int sessionID, FileInfo asmFile)
        {
            //получение существующей или создание новой сессии
            var session = GetOrCreateDomainSession(sessionID);

            //загрузка сборки в сессию
            return session.LoadAssembly(asmFile);
        }

        /// <summary>
        /// Открывает новую сессию AppDomain's
        /// </summary>
        /// <returns>Новая сессия</returns>
        public static DomainSession OpenNewSession()
        {
            var newSessionId = 0;

            lock (ManagerLocker)
                newSessionId = ++SessionID_Counter;

            //получение существующей или создание новой сессии
            var session = GetOrCreateDomainSession(newSessionId);

            return session;
        }

        /// <summary>
        /// Освобождение ресурсов доменной сессии
        /// </summary>
        /// <param name="sessionID">ID сессии</param>
        public static void DisposeSession(int sessionID)
        {
            try
            {
                lock (ManagerLocker)
                {
                    if (m_createdDomains.ContainsKey(sessionID))
                    {
                        var domains = m_createdDomains[sessionID];
                        foreach (var item in domains)
                        {
                            item.Dispose();
                        }

                        domains.Clear();

                        m_createdDomains.Remove(sessionID);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilites.m_loger.Log("Возникло исключение при освобождении доменной сессиии", ex, new LogParameter("ID сессии", sessionID.ToString()));
            }
        }

        /// <summary>
        /// При DomainSession.Dispose()
        /// </summary>
        /// <param name="session">Сылка на сессию</param>
        static void SessionUnloaded(DomainSession session)
        {
            if (session == null)
                return;

            session.Disposed -= SessionUnloaded;
            var id = session.ID;

            lock (ManagerLocker)
            {
                if (m_sessions != null)
                    if (m_sessions.ContainsKey(id))
                        m_sessions.Remove(id);
            }
        }

        /// <summary>
        /// Получение или создание доменной сессии
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        static DomainSession GetOrCreateDomainSession(int sessionID)
        {
            lock (ManagerLocker)
            {
                if (m_createdDomains.ContainsKey(sessionID))
                {
                    return m_sessions[sessionID];
                }
                else
                {
                    var newSession = new DomainSession(sessionID);
                    newSession.Disposed += SessionUnloaded;
                    m_sessions.Add(sessionID, newSession);

                    return newSession;
                }
            }
        }

        
    }
}
