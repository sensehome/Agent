using System;
using System.Collections.Generic;

namespace SenseHome.Agent.Services.Caching
{
    public class CacheService : ICacheService, IDisposable
    {
        private readonly Dictionary<string, string> dictionary;
           
        public CacheService()
        {
            dictionary = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            dictionary.Add(key, value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public IEnumerable<string> GetKeys()
        {
            return dictionary.Keys;
        }

        public string GetValueOrDefault(string key)
        {
            return dictionary.GetValueOrDefault(key);
        }

        public bool IsExist(string key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Set(string key, string value)
        {
            dictionary[key] = value;
        }

        public void Dispose()
        {
            dictionary.Clear();
        }
    }
}
