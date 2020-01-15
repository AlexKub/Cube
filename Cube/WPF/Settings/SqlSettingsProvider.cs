using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.WPF.Settings
{
    /// <summary>
    /// Поставщик настроек из SQL
    /// </summary>
    public abstract class SqlSettingsProvider<TSetting, TKey, TValue> : IApplicationSettingsProvider<TSetting, TKey, TValue>
        where TSetting : IApplicationSetting<TKey, TValue>
    {
        IDictionary<TKey, TValue> m_cashe = new ConcurrentDictionary<TKey, TValue>();

        public string ConnectionString { get; private set; }

        public SqlSettingsProvider()
        {
            
        }

        #region IDictionary

        public TSetting this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TSetting> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TSetting value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TSetting> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TSetting> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TSetting>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TSetting>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TSetting> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TSetting value)
        {
            throw new NotImplementedException();
        }

        public void Update(TKey key)
        {
            throw new NotImplementedException();
        }

        public void UpdateSettings()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
