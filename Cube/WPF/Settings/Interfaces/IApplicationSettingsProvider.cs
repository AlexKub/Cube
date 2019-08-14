using System;
using System.Collections.Generic;

namespace Cube.WPF.Settings
{
    /// <summary>
    /// Набор настроек
    /// </summary>
    /// <typeparam name="TSetting">Тип настройки</typeparam>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    interface IApplicationSettingsProvider<TSetting, TKey, TValue> : IDictionary<TKey, TSetting>, IDisposable
        where TSetting : IApplicationSetting<TKey, TValue>
    {
        /// <summary>
        /// Обновить настройки
        /// </summary>
        void UpdateSettings();

        /// <summary>
        /// Обновить конкретную настройку
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        void Update(TKey key);
    }
}
