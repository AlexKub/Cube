using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.Log
{
    /// <summary>
    /// Построитель сообщения
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    public interface IMeassageBuilder<TMessage, TData>
    {
        /// <summary>
        /// Построение сообщения
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TMessage Build(TData data);
    }
}
