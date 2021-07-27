using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static Boolean ReallyTryGetValueOrDefault<TValue>(this IDictionary<String, Object> dictionary, String key, out TValue value, Func<Object, TValue> valuefier, TValue defaultValue = default(TValue))
        {
            if (Extensions.Misc.ReallyTryGetValueOrDefault(dictionary, key, default, out Object obj))
            {
                value = valuefier.Invoke(obj);
                return true;
            }

            value = defaultValue;
            return false;
        }

        public static TValue ReallyGetValueOrDefault<TValue>(this IDictionary<String, Object> dictionary, String key, out TValue value, Func<Object, TValue> valuefier, TValue defaultValue = default(TValue))
        {
            ReallyTryGetValueOrDefault(dictionary, key, out value, valuefier, defaultValue);
            return value;
        }

        public static TValue ReallyGetValueOrDefault<TValue>(this IDictionary<String, Object> dictionary, String key, Func<Object, TValue> valuefier, TValue defaultValue = default(TValue))
            => ReallyGetValueOrDefault(dictionary, key, out TValue value, valuefier, defaultValue);
    }
}
