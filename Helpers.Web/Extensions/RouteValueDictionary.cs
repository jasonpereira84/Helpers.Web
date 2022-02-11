using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Routing;

        public static partial class Web
        {
            public static String GetValue(this RouteValueDictionary routeValueDictionary, String key, String defaultValue = default(String))
            {
                if (routeValueDictionary.None())
                    return defaultValue;

                if (routeValueDictionary.ReallyTryGetValueOrDefault(key, out Object obj))
                    return (obj?.ToString() ?? default(String)).SanitizeTo(defaultValue);

                return defaultValue;
            }

            public static String GetController(this RouteValueDictionary routeValueDictionary, String defaultValue = default(String))
                => GetValue(routeValueDictionary, "Controller", defaultValue);

            public static String GetAction(this RouteValueDictionary routeValueDictionary, String defaultValue = default(String))
                => GetValue(routeValueDictionary, "Action", defaultValue);
        }
    }
}
