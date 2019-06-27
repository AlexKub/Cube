using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core_2_0;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Доменная сессия
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    internal class DomainSession : IDisposable
    {
        /*
         * сессия загрузки сборок
         * 
         * сделана с целью решения проблемы загрузки схожих сборок
         * (грузят два разных файла одной сборки)
         * 
         * в этом случае открываем новые домены для загрузки отдельных файлов сборок
         */
        private static ILoger m_Loger = Utilites.m_loger;

        bool m_Disposed = false;

        /// <summary>
        /// ID сессии
        /// </summary>
        internal int ID { get; private set; }

        /// <summary>
        /// Домены сессии
        /// </summary>
        internal List<SeparatedAppDomain> Domains { get; private set; }

        /// <summary>
        /// Объект для синхронизации
        /// </summary>
        internal object SessionLocker { get; private set; }

        internal DomainSession(int id)
        {
            ID = id;
            Domains = new List<SeparatedAppDomain>();
            SessionLocker = new object();
        }

        /// <summary>
        /// Загрузка библиотеки в сессию
        /// </summary>
        /// <param name="asmFile"></param>
        /// <returns></returns>
        internal SeparatedAssembly LoadAssembly(FileInfo asmFile)
        {
            try
            {
                string pathName = asmFile.FullName;
                //получение информации о сборке без загрузки в домен
                //https://stackoverflow.com/questions/30306702/assemblyinfo-without-loading
                var name = AssemblyName.GetAssemblyName(pathName);

                /*
                 * получение уникального HASH'а файла
                 * на случай, если грузятся разные файлы (даже по названию), но это одна сборка
                 * по хешу смотрим, одинаковые ли файлы или нет
                 * 
                 * теоретически, возможны ложные срабатывания проверки
                 * когда одну сборку переименовали в другую и их хэш совпал
                 * но вероятность подобного крайне мала ;)
                 */
                var asmHash = Utilites.GetFileHash(pathName);

                SeparatedAppDomain domain = null;
                bool same = false; //флаг, что нашли домен с такой же сборкой (и таким же файлом)
                lock (SessionLocker)
                {
                    foreach (var dom in Domains)
                    {
                        var loadedState = dom.HasLoaded(name, asmHash);

                        switch (loadedState)
                        {

                            case LoadedState.NotLoaded:
                                //если текущий домен свободен - грузим сюда

                                domain = dom;
                                break;
                            case LoadedState.LoadedDifferent:
                                //если занят - проверяем следующий. Может быть свободен
                                continue;
                            case LoadedState.LoadedSame:
                                //если точно такая же уже загружена - выдаём её же
                                domain = dom;
                                same = true;
                                break;
                        }

                        //если есть куда
                        if (domain != null)
                            break;
                    }
                }

                //если имеющиеся домены не подходят для загрузки
                //в них уже есть сборки со схожим именем, но из других файлов
                if (domain == null)
                {
                    //создаём новый домен в текущей сессии
                    domain = CreateDomain();
                }

                //загрузка новой сборки в домен
                return same
                    ? domain.GetAssembly(name) //выдаём уже загруженный ранее файл
                    : domain.LoadAssembly(asmFile, name, asmHash); //загружаем новую сборку
            }
            catch (Exception ex)
            {
                return new SeparatedAssembly(ex);
            }
        }

        /// <summary>
        /// Создание нового домена
        /// </summary>
        /// <returns>Возвращает новый экземпляр с новым доменом для текущей сессии</returns>
        internal SeparatedAppDomain CreateDomain()
        {
            var domain = new SeparatedAppDomain(this);
            lock (SessionLocker)
                Domains.Add(domain);

            return domain;
        }

        /// <summary>
        /// Добавление домена в сессию
        /// </summary>
        /// <param name="domain">Домен</param>
        internal void Add(SeparatedAppDomain domain)
        {
            if (domain == null)
                return;

            try
            {
                lock (SessionLocker)
                    Domains.Add(domain);

                domain.Disposed += OnDomainDisposed;
            }
            catch (Exception ex)
            {
                m_Loger.Log("Возникло исключение при добавлении домена в сессию", ex
                    , new LogParameter("ID сессии", ID.ToString()));
            }
        }

        #region Dispose

        internal event Action<DomainSession> Disposed;

        /// <summary>
        /// Учёт выгрузки Домена в Сессии
        /// </summary>
        /// <param name="domain">Выгружаемый домен</param>
        internal void OnDomainDisposed(SeparatedAppDomain domain)
        {
            if (domain == null)
                return;

            domain.Disposed -= OnDomainDisposed;

            var dCount = 0;
            lock (SessionLocker)
            {
                Domains.Remove(domain);

                dCount = Domains.Count;
            }

            if (dCount == 0)
                Dispose();
        }

        /// <summary>
        /// Выгрузка Доменов Сессии
        /// </summary>
        public void Dispose()
        {
            lock (SessionLocker)
                if (m_Disposed)
                    return;
                else
                    m_Disposed = true;

            try
            {
                lock (SessionLocker)
                {
                    foreach (var domain in Domains)
                    {
                        domain.Disposed -= OnDomainDisposed;
                        domain.Dispose();
                    }

                    Domains.Clear();
                }
            }
            catch (Exception ex)
            {
                Utilites.m_loger.Log("Возникло исключение при освобождении доменной сессиии", ex, new LogParameter("ID сессии", ID.ToString()));
            }
            finally
            {
                Disposed?.Invoke(this);
            }
        }

        #endregion

        string DebugDisplay()
        {
            return ID.ToString() + " | Count: " + Domains.Count.ToString();
        }
    }
}
