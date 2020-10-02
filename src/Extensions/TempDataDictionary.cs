using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        public static partial class Web
        {
            public static TTempDataDictionary AddIfNew<TTempDataDictionary>(this TTempDataDictionary dictionary, String key, Object value)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNew(dictionary, key, value);

            public static TTempDataDictionary AddIfNew<TTempDataDictionary>(this TTempDataDictionary dictionary, params KeyValuePair<String, Object>[] pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNew(dictionary, pairs);

            public static TTempDataDictionary AddIfNew<TTempDataDictionary>(this TTempDataDictionary dictionary, IEnumerable<KeyValuePair<String, Object>> pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNew(dictionary, pairs);

            public static TTempDataDictionary AddIfNew<TTempDataDictionary>(this TTempDataDictionary dictionary, params (String Key, Object Value)[] pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNew(dictionary, pairs);

            public static TTempDataDictionary AddIfNew<TTempDataDictionary>(this TTempDataDictionary dictionary, IEnumerable<(String Key, Object Value)> pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNew(dictionary, pairs);


            public static TTempDataDictionary AddIfNewOrUpdate<TTempDataDictionary>(this TTempDataDictionary dictionary, String key, Object value)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNewOrUpdate(dictionary, key, value);

            public static TTempDataDictionary AddIfNewOrUpdate<TTempDataDictionary>(this TTempDataDictionary dictionary, params KeyValuePair<String, Object>[] pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNewOrUpdate(dictionary, pairs);

            public static TTempDataDictionary AddIfNewOrUpdate<TTempDataDictionary>(this TTempDataDictionary dictionary, IEnumerable<KeyValuePair<String, Object>> pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNewOrUpdate(dictionary, pairs);

            public static TTempDataDictionary AddIfNewOrUpdate<TTempDataDictionary>(this TTempDataDictionary dictionary, params (String Key, Object Value)[] pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNewOrUpdate(dictionary, pairs);

            public static TTempDataDictionary AddIfNewOrUpdate<TTempDataDictionary>(this TTempDataDictionary dictionary, IEnumerable<(String Key, Object Value)> pairs)
                where TTempDataDictionary : ITempDataDictionary
                => Misc.AddIfNewOrUpdate(dictionary, pairs);
        }
    }
}
