using System;

namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Ручной (виртуальный) таймер
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ManualTimer
    {
        /*
         * обёртка над основной логикой тайминга
         * 
         * чтобы не плодить таймера на каджый чих
         * сделал общую логику тайминга
         * 
         * с помощью этого класса, можно коллекцию таких таймеров повесить на один реальный
         * и на каждом его (реального таймера) тике вызывать "Tick()" для каждого из виртуальных таймеров
         * эмулируя тайминг в каждом из экземпляров
         * 
         * сделано, в т.ч. для сокращения общего числа потоков
         * поскольку каждый таймер содаёт свой
         * 
         * отсюда, "внутренности" каждого таймера принадлежат потоку реального таймера, на котором он "хостится"
         * если не указано иного (в Action вложен отдельный Task / Thread)
         * 
         * ! Внимание ! 
         * в виду логики эмуляции, допускается погрешность определения времени срабатывания таймера
         * равная (максимум) интервалу реального таймера + скорости выполнения предстоящей логики
         */

        /// <summary>
        /// Времени прошло
        /// </summary>
        public TimeSpan Elapsed { get; private set; }

        /// <summary>
        /// Шаг интервала
        /// </summary>
        public TimeSpan Period { get; private set; }

        /// <summary>
        /// Контрольное время
        /// </summary>
        public TimeSpan Timeout { get; private set; }

        /// <summary>
        /// Безконечный
        /// </summary>
        public bool Repeatable { get; private set; }

        /// <summary>
        /// Действие по истечению времени
        /// </summary>
        public Action Action { get; private set; }

        /// <summary>
        /// Пауза
        /// </summary>
        public bool Pause { get; set; }

        /// <summary>
        /// Новый таймер
        /// </summary>
        /// <param name="period">Шаг таймера</param>
        /// <param name="timeOut">Контрольное время</param>
        /// <param name="action">Действие по истечению времени</param>
        /// <param name="repeatable">Флаг сброса таймера по истечению время (безконечный таймер)</param>
        public ManualTimer(TimeSpan period, TimeSpan timeOut, Action action, bool repeatable = true)
        {
            Period = period;
            Timeout = timeOut;
            Repeatable = repeatable;
            Action = action;
        }

        /// <summary>
        /// Шаг таймера
        /// </summary>
        public void Tick()
        {
            if (Pause)
                return;

            Elapsed += Period;

            if (Elapsed > Timeout)
            {
                if (Repeatable)
                    Elapsed = new TimeSpan();

                Action?.Invoke();
            }
        }

        string DebugDisplay()
        {
            return $"Timeout: {Timeout.ToString()} | Elapsed: {Elapsed.ToString()}";
        }
    }

}
