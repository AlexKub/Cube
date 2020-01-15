using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Менеджер логирования
    /// </summary>
    /// <typeparam name="TContext">Тип контекста логирования</typeparam>
    /// <typeparam name="TData">Тип логируемых данных</typeparam>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    public abstract class LogManager<TContext, TData, TMessage>
        where TContext : ILogingContext
    {
        /// <summary>
        /// Конечные точки логирования
        /// </summary>
        public IReadOnlyCollection<ILogEndpoint<TMessage>> Endpoints { get; private set; }
        /// <summary>
        /// Построитель сообщения
        /// </summary>
        public IMeassageBuilder<TData, TMessage> MeassageBuilder { get; private set; }

        /// <summary>
        /// Проверка на пропуск логирования для текущего сообщения
        /// </summary>
        /// <param name="context">Контекст логирования</param>
        /// <returns>ВОзвращает флаг необходимости пропуска сообщения</returns>
        public abstract bool ShoudSkip(TContext context);
    }
}
