using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;

        public static partial class Web
        {
            public static Dictionary<String, IEnumerable<String>> AsDictionary(this IHeaderDictionary headerDictionary)
                => headerDictionary
                    .ToDictionary(
                        keySelector: kvp => kvp.Key,
                        elementSelector: kvp => kvp.Value.AsStrings());

        }
    }
}
