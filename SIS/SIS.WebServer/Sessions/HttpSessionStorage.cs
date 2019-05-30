﻿namespace SIS.MvcFramework.Sessions
{
    using System.Collections.Concurrent;
    using HTTP.Sessions;

    public class HttpSessionStorage
    {
        public const string SessionCookieKey = "SIS_ID";

        private static readonly ConcurrentDictionary<string, IHttpSession> httpSessions =
            new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession GetSession(string id)
        {
            return httpSessions.GetOrAdd(id, _ => new HttpSession(id));
        }

        public static bool ContainsSession(string id)
        {
            return httpSessions.ContainsKey(id);
        }
    }
}
