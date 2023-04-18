using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Primitives;

        public static partial class Web
        {
            public static IEnumerable<String> AsStrings(this StringValues stringValues)
                => stringValues
                    .ToArray()//Do not change
                    .SelectWhen(
                        predicate: s => s.IsNotNullOrEmptyOrWhiteSpace(),
                        selector: s => s.Trim());

            public static String AsString(this StringValues stringValues)
                => Misc.AsString(stringValues.AsStrings());

            public static String AsString(this StringValues stringValues, String separator)
                => Misc.AsString(stringValues.AsStrings(), separator);

            public static String AsString(this StringValues stringValues, Char separator)
                => Misc.AsString(stringValues.AsStrings(), separator);

        }
    }
}
