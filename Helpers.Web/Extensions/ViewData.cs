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
                => viewDataDictionary
                    .ReallyGetValueOrDefault("Title", obj => obj as String)
                    .EvaluateSanity()
                    .Do(eval =>  String.Format(eval.IsSane? titleFormatIfTrue: titleFormatIfFalse, prefix, eval.Value));
        }
    }
}
