using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace MatrixWeb
{
    public static class SessionExtensions
    {

        public static bool? GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToBoolean(data, 0);
        }

        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static T GetObject<T> (this ISession session, string key)where T:class
        {
            var json = session.GetString(key);
            if (json == null)            
                return null;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetOject(this ISession session, string key, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            session.SetString(key, json);
        }
    }
}
