using System.Collections.Generic;

namespace SenseHome.Agent.Services.Caching
{
    public interface ICacheService
    {
        void Add(string key, string value);
        void Clear();
        IEnumerable<string> GetKeys();
        string GetValueOrDefault(string key);
        bool IsExist(string key);
        void Set(string key, string value);
    }
}
