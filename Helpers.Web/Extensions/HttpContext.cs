using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        public static partial class Web
        {
            public static ITempDataDictionaryFactory GetTempDataDictionaryFactory(this HttpContext httpContext)
                => httpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;

            public static Boolean TryGetTempDataDictionaryFactory(this HttpContext httpContext, out ITempDataDictionaryFactory tempDataDictionaryFactory)
            {
                try
                {
                    tempDataDictionaryFactory = GetTempDataDictionaryFactory(httpContext);
                    return tempDataDictionaryFactory != null;
                }
                catch
                {
                    tempDataDictionaryFactory = default(ITempDataDictionaryFactory);
                    return false;
                }
            }

            public static ITempDataDictionary GetTempData(this HttpContext httpContext)
                => GetTempDataDictionaryFactory(httpContext).GetTempData(httpContext);

            public static Boolean TryGetTempData(this HttpContext httpContext, out ITempDataDictionary tempDataDictionary)
            {
                try
                {
                    //Note: ITempDataDictionaryFactory.GetTempData
                    //      Gets or creates an Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary
                    //      instance for the request associated with the given context.
                    //      so only possible failure is ITempDataDictionaryFactory being null
                    tempDataDictionary = GetTempData(httpContext);
                    return true;
                }
                catch
                {
                    tempDataDictionary = default(ITempDataDictionary);
                    return false;
                }
            }

        }
    }
}
