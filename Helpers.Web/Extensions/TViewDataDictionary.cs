using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        public static partial class Web
        {
            public static String GetTitle<TViewDataDictionary>(this TViewDataDictionary viewDataDictionary, String prefix, String titleFormatIfTrue = "{0} - {1}", String titleFormatIfFalse = "{0}")
                where TViewDataDictionary : ViewDataDictionary
            {
                if (!Misc.ReallyTryGetValueOrDefault(viewDataDictionary, "Title", out Object obj))
                    return String.Format(titleFormatIfFalse, prefix, "NULL");

                if(!Misc.EvaluateSanity(obj as String ?? default(String), out String saneValue))
                    return String.Format(titleFormatIfFalse, prefix, saneValue);

                return String.Format(titleFormatIfTrue, prefix, saneValue);
            }
        }
    }
}
