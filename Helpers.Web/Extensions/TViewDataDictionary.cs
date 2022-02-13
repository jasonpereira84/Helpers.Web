using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        public static partial class Web
        {
            internal static Boolean reallyTryGetTitle(IDictionary<String, Object> dictionary, out String title)
            {
                if (dictionary == null || dictionary.ReallyTryGetValue("Title", out Object @object).IsFalse())
                {
                    title = "NULL";
                    return false;
                }

                return (@object as String).EvaluateSanity(out title);
            }
            public static Boolean ReallyTryGetTitle<TViewDataDictionary>(this TViewDataDictionary viewDataDictionary, out String title)
                where TViewDataDictionary : ViewDataDictionary
                => reallyTryGetTitle(viewDataDictionary, out title);

            internal static String titleFrom(IDictionary<String, Object> dictionary, String prefix,
                String titleFormatIfTrue, 
                String titleFormatIfFalse)
                => String.Format(
                    reallyTryGetTitle(dictionary, out String title) 
                        ? titleFormatIfTrue ?? "{0} - {1}"
                        : titleFormatIfFalse ?? "{0}",
                    prefix,
                    title);
            public static String TitleFrom<TViewDataDictionary>(this TViewDataDictionary viewDataDictionary, String prefix,
                String titleFormatIfTrue = "{0} - {1}",
                String titleFormatIfFalse = "{0}")
                where TViewDataDictionary : ViewDataDictionary
                => titleFrom(viewDataDictionary, prefix, titleFormatIfTrue, titleFormatIfFalse);
        }
    }
}
