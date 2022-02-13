using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        public static partial class Web
        {
            public static ITempDataDictionaryFactory GetTempDataDictionaryFactory<TActionContext>(this TActionContext actionContext)
                where TActionContext : ActionContext
                => GetTempDataDictionaryFactory(actionContext.HttpContext);

            public static Boolean TryGetTempDataDictionaryFactory<TActionContext>(this TActionContext actionContext, out ITempDataDictionaryFactory tempDataDictionaryFactory)
                where TActionContext : ActionContext
                => TryGetTempDataDictionaryFactory(actionContext.HttpContext, out tempDataDictionaryFactory);

            public static ITempDataDictionary GetTempData<TActionContext>(this TActionContext actionContext)
                where TActionContext : ActionContext
                => GetTempData(actionContext.HttpContext);

            public static Boolean TryGetTempData<TActionContext>(this TActionContext actionContex, out ITempDataDictionary tempDataDictionary)
                where TActionContext : ActionContext
                => TryGetTempData(actionContex.HttpContext, out tempDataDictionary);
        }
    }
}
