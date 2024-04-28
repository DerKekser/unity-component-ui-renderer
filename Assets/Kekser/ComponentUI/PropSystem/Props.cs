using System;
using System.Collections;
using System.Collections.Generic;

namespace Kekser.ComponentUI.PropSystem
{
    public sealed class Props: IDictionary<string, object>
    {
        private Dictionary<string, object> _props = new Dictionary<string, object>();
        private bool _isDirty = true;

        public int Count => _props.Count;
        public bool IsReadOnly => false;

        public bool IsDirty
        {
            get => _isDirty;
            set => _isDirty = value;
        }
        
        public T Get<T>(string prop, T defaultValue = default)
        {
            if (TryGetValue(prop, out object p))
                return (T) p;
            return defaultValue;
        }
        
        public object Get(string prop, object defaultValue = null)
        {
            if (TryGetValue(prop, out object p))
                return p;
            return defaultValue;
        }
        
        public void Set<T>(string prop, T p)
        {
            if (TryGetValue(prop, out object current) && current.Equals(p))
                return;
            _props[prop] = p;
            _isDirty = true;
        }
        
        public void Set(string prop, object p)
        {
            Set<object>(prop, p);
        }
        
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _props.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            _props.Add(item.Key, item.Value);
            _isDirty = true;
        }

        public void Clear()
        {
            _props.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return ContainsKey(item.Key) && _props[item.Key].Equals(item.Value);
        }
        
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            _isDirty = true;
            return _props.Remove(item.Key);
        }

        public void Add(string key, object value)
        {
            _props.Add(key, value);
            _isDirty = true;
        }

        public bool ContainsKey(string key)
        {
            return _props.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            _isDirty = true;
            return _props.Remove(key);
        }
        
        public bool TryGetValue<T>(string key, out T value)
        {
            if (TryGetValue(key, out object v))
            {
                value = (T) v;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            return _props.TryGetValue(key, out value) && value != null;
        }

        public object this[string key]
        {
            get => _props[key];
            set => Set(key, value);
        }

        public ICollection<string> Keys => _props.Keys;
        public ICollection<object> Values => _props.Values;
    }
}