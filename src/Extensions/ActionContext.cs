using System;
using System.Linq;
using System.Collections.Generic;

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

            public static Boolean GetFactory<TActionContext>(this TActionContext actionContext, out ITempDataDictionaryFactory tempDataDictionaryFactory)
                where TActionContext : ActionContext
                => Do.Action(tempDataDictionaryFactory = GetFactory(actionContext), factory => factory.IsNotNull());

            public static ITempDataDictionary GetTempData<TActionContext>(this TActionContext actionContext, ITempDataDictionaryFactory tempDataDictionaryFactory)
                where TActionContext : ActionContext
                => tempDataDictionaryFactory.GetTempData(actionContext.HttpContext);

            public static ITempDataDictionary GetTempData<TActionContext>(this TActionContext actionContext)
                where TActionContext : ActionContext
                => GetTempData(actionContext, GetFactory(actionContext));

            public static Boolean GetTempData<TActionContext>(this TActionContext actionContext, out ITempDataDictionary tempDataDictionary)
                where TActionContext : ActionContext
                => Do.Action(tempDataDictionary = GetTempData(actionContext, GetFactory(actionContext)), tempData => tempData.IsNotNull());
        }
    }
}
