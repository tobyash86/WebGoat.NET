using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace WebGoatCore
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            if(session.TryGet<T>(key, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException(key);
        }

        public static bool TryGet<T>(this ISession session, string key, out T value)
        {
            var str = session.GetString(key);
            if(str == null)
            {
                value = default;
                return false;
            }

            value = JsonSerializer.Deserialize<T>(str);
            return true;
        }
    }
}
