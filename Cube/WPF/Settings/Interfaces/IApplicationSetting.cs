using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.WPF.Settings
{
    /// <summary>
    /// Настройка Приложения
    /// </summary>
    /// <typeparam name="T">Тип значения</typeparam>
    public interface IApplicationSetting<TKey, TValue>
    {
        /// <summary>
        /// Внутренний уникальный идентификатор (уникален в рамках AppDomain)
        /// </summary>
        int InternalID { get; }

        /// <summary>
        /// Ключ Настройки (уникален в рамках контекста)
        /// </summary>
        TKey Key { get; }

        /// <summary>
        /// Значение
        /// </summary>
        TValue Value { get; set; }
    }
}
