using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebGoatCore
{
    public static class SessionExtensions
    {
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
    }
}
