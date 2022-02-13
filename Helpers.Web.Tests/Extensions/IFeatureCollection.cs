using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JasonPereira84.Helpers.Web.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        using Microsoft.AspNetCore.Diagnostics;
        using Microsoft.AspNetCore.Http.Features;

        [TestClass]
        public class Test_IFeatureCollection
        {
            [TestMethod]
            public void TryGet()
            {
                {
                    Assert.IsFalse(
                        Web.TryGet(default(IFeatureCollection), out IExceptionHandlerFeature feature));
                }

                {
                    Assert.ThrowsException<ArgumentNullException>(
                        () => Web.TryGet(default(IFeatureCollection), default(Func<IExceptionHandlerFeature, Exception>), out Exception exception));
                }

            }

        }
    }
}
