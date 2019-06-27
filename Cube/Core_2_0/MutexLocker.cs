using System;
using System.Threading;

namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Одноразовый Mutex для межпроцессной синхронизации
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class MutexLocker : IDisposable
    {
        //подробнее https://docs.microsoft.com/en-us/dotnet/api/system.threading.mutex.-ctor?redirectedfrom=MSDN&view=netframework-4.7.2#System_Threading_Mutex__ctor_System_Boolean_System_String_

        Mutex m_mutex;
        bool requestInitialOwnership = true;
        bool mutexWasCreated;

        /// <summary>
        /// Имя Mutex'a
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Текущее состояние
        /// </summary>
        public MutexState State { get; private set; }

        /// <summary>
        /// Новый Mutex
        /// </summary>
        /// <param name="name">Уникальное имя для межпроцессной синхронизации</param>
        public MutexLocker(string name)
        {
            State = MutexState.None;

            Name = name;
            m_mutex = new Mutex(requestInitialOwnership,
                            name,
                            out mutexWasCreated);
        }

        /// <summary>
        /// Блокировка текущего потока для прочих процессов
        /// </summary>
        /// <param name="name">Имя Mutex'a</param>
        /// <returns>Возвращает блокирующий/ожидающий экземпляр</returns>
        public static MutexLocker Lock(string name)
        {
            var m = new MutexLocker(name);

            //ожидание прочих Mutex'ов из других процессов.
            if (!(m.requestInitialOwnership && m.mutexWasCreated))
            {
                m.State = MutexState.Awaited;
                m.m_mutex.WaitOne();
            }

            m.State = MutexState.Locked;

            return m;
        }

        /// <summary>
        /// Освобождение блокировки Mutex'a
        /// </summary>
        public void Dispose()
        {
            if (m_mutex != null)
            {

                try
                {
                    m_mutex.ReleaseMutex(); //разблокировка текущего потока
                    State = MutexState.Relised;
                    m_mutex.Close();
                    m_mutex = null;
                }
                catch(Exception ex)
                {
                    Log.WindowsLoger.LogError("ECR", "Возникло исключение при освобождении Mutex'a", ex, new Log.LogParameter("Имя", Name));
                }
            }
        }

        string DebugDisplay()
        {
            return (String.IsNullOrEmpty(Name) ? "NO_NAME" : Name) + " | State: " + State.ToString();
        }

        /// <summary>
        /// Текущее состояние Mutex'a
        /// </summary>
        public enum MutexState
        {
            /// <summary>
            /// По умолчанию - никакое
            /// </summary>
            None,
            /// <summary>
            /// Заблокирован - активен текущий поток, остальные ожидают
            /// </summary>
            Locked,
            /// <summary>
            /// Разблокирован (Disposed вызыван)
            /// </summary>
            Relised,
            /// <summary>
            /// В ожидании
            /// </summary>
            Awaited
        }
    }
}
