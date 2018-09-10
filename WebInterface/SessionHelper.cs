using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInterface
{
    using System.Web.SessionState;

    using Microsoft.ApplicationInsights.Web;

    public static class SessionHelper
    {

        public enum SessionKey
        {
            key1,
            key2,
            key3
        }

        public static void Set(HttpSessionState session, SessionKey key, object value)
        {
            var name = Enum.GetName(typeof(SessionKey), key);
            session[name] = value;
        }

        public static T Get<T>(HttpSessionState session, SessionKey key)
        {
            var name = Enum.GetName(typeof(SessionKey), key);
            var value = session[name];
            if (value is T)
            {
                return (T)value;
            }

            return default(T);
        }
    }
}