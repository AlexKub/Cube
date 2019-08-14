using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.Log
{
    /// <summary>
    /// Менеджер логирования
    /// </summary>
    /// <typeparam name="TContext">Тип контекста логирования</typeparam>
    public abstract class LogManager<TContext>
        where TContext : ILogingContext
    {
        /// <summary>
        /// Проверка на пропуск логирования для текущего сообщения
        /// </summary>
        /// <param name="context">Контекст логирования</param>
        /// <returns>ВОзвращает флаг необходимости пропуска сообщения</returns>
        public abstract bool ShoudSkip(TContext context);
    }
}
