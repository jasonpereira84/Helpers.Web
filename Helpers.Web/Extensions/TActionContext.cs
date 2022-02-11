using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        public static partial class Web
        {
            public static ITempDataDictionaryFactory GetFactory<TActionContext>(this TActionContext actionContext)
                where TActionContext : ActionContext
                => actionContext.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;

            public static Boolean TryGetFactory<TActionContext>(this TActionContext actionContext, out ITempDataDictionaryFactory tempDataDictionaryFactory)
                where TActionContext : ActionContext
            {
                try
                {
                    tempDataDictionaryFactory = GetFactory(actionContext);
                    return tempDataDictionaryFactory != null;
                }
                catch
                {
                    tempDataDictionaryFactory = default(ITempDataDictionaryFactory);
                    return false;
                }
            }

            public static ITempDataDictionary GetTempData<TActionContext>(this TActionContext actionContext, ITempDataDictionaryFactory tempDataDictionaryFactory)
                where TActionContext : ActionContext
                => tempDataDictionaryFactory.GetTempData(actionContext.HttpContext);

            public static Boolean TryGetTempData<TActionContext>(this TActionContext actionContext, ITempDataDictionaryFactory tempDataDictionaryFactory, out ITempDataDictionary tempDataDictionary)
                where TActionContext : ActionContext
            {
                try
                {
                    tempDataDictionary = GetTempData(actionContext, tempDataDictionaryFactory);
                    return tempDataDictionary != null;
                }
                catch
                {
                    tempDataDictionary = default(ITempDataDictionary);
                    return false;
                }
            }

            public static ITempDataDictionary GetTempData<TActionContext>(this TActionContext actionContext)
                where TActionContext : ActionContext
                => GetTempData(actionContext, GetFactory(actionContext));

            public static Boolean TryGetTempData<TActionContext>(this TActionContext actionContext, out ITempDataDictionary tempDataDictionary)
                where TActionContext : ActionContext
            {
                try
                {
                    tempDataDictionary = GetTempData(actionContext);
                    return tempDataDictionary != null;
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
