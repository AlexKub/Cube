using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.Log
{
    /// <summary>
    /// Построение строкового сообщения для типа
    /// </summary>
    public abstract class TypeStringMessageBuilder<T> : IMeassageBuilder<string, T>
    {
        public abstract string Build(T data);
    }
}
