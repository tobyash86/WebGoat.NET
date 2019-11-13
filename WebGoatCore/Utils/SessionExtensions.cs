using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebGoatCore
{
    public static class SessionExtensions
    {
        private static readonly Dictionary<Guid, object> _cache = new Dictionary<Guid, object>();

        public static void Set<T>(this ISession session, string key, T value)
        {
            var s = JsonConvert.SerializeObject(value);
            session.SetString(key, s);
        }

        public static T Get<T>(this ISession session, string key)
        {
            if (session.TryGet<T>(key, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException(key);
        }

        public static bool TryGet<T>(this ISession session, string key, out T value)
        {
            var str = session.GetString(key);
            if (str == null)
            {
                value = default;
                return false;
            }

            value = JsonConvert.DeserializeObject<T>(str);
            return true;
        }

        public static void SetInMemory(this ISession session, string key, object value)
        {
            var cacheKey = Guid.NewGuid();
            _cache.Add(cacheKey, value);
            session.Set(key, cacheKey.ToByteArray());
        }

        public static bool TryGetInMemory<T>(this ISession session, string key, out T value)
        {
            if (!session.TryGetValue(key, out byte[] cacheKeyBytes))
            {
                value = default;
                return false;
            }

            var cacheKey = new Guid(cacheKeyBytes);
            value = (T)_cache[cacheKey];
            return true;
        }

        public static T GetInMemory<T>(this ISession session, string key)
        {
            if (session.TryGetInMemory<T>(key, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException(key);
        }
    }
}
