using System;

namespace Core
{
    public interface ICacheEviction
    {
        void AddKeyToEvict(string key, TimeSpan? timer = null);
    }
}
