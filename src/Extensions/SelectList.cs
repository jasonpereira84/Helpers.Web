using System;
using System.Net;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc.Rendering;

        public static partial class Web
        {
            public static SelectList AsSelectList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
                => new SelectList(pairs, "Key", "Value");
            public static SelectList AsSelectList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs, TKey selectedValue)
                => new SelectList(pairs, "Key", "Value", selectedValue);

            public static SelectList AsSelectList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs, TKey selectedValue, String dataGroupField)
                => new SelectList(pairs, "Key", "Value", selectedValue, dataGroupField);

        }
    }
}
