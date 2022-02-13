using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JasonPereira84.Helpers.Web.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        using Microsoft.AspNetCore.Mvc;
        using Microsoft.AspNetCore.Mvc.ViewFeatures;

        [TestClass]
        public class Test_HttpContext
        {
            [TestMethod]
            public void TryGetTempDataDictionaryFactory()
            {
                Assert.IsFalse(
                    Web.TryGetTempDataDictionaryFactory(new ActionContext().HttpContext, out ITempDataDictionaryFactory tempDataDictionaryFactory));
            }

            [TestMethod]
            public void TryGetTempData()
            {
                Assert.IsFalse(
                    Web.TryGetTempData(new ActionContext().HttpContext, out ITempDataDictionary tempDataDictionary));
            }

        }
    }
}
