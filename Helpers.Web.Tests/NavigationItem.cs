using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JasonPereira84.Helpers.Web.Tests
{
    using Microsoft.AspNetCore.Http;

    [TestClass]
    public class Test_NavigationItem
    {
        [TestMethod]
        public void ctor()
        {
            {
                var text = "1";
                var href = new PathString("/1");

                var navigationItem = new NavigationItem(text, href);
                Assert.IsNotNull(navigationItem);
                Assert.AreEqual(
                    expected: text,
                    actual: navigationItem.Text);
                Assert.AreEqual(
                    expected: href.Value,
                    actual: navigationItem.Href.Value);
            }

            {
                var href = new PathString("/1");

                Assert.ThrowsException<ArgumentNullException>(
                    () => new NavigationItem(default(String), href));
            }

        }

        [TestMethod]
        public void SetIsActiveIfMatches()
        {
            {
                var text = "1";
                var href = new PathString("/1");

                var navigationItem = new NavigationItem(text, href);
                Assert.IsFalse(navigationItem.IsActive);

                navigationItem.SetIsActiveIfMatches(href);
                Assert.IsTrue(navigationItem.IsActive);
            }

            {
                var text = "1";
                var href = new PathString("/1");

                var navigationItem = new NavigationItem(text, href);
                Assert.IsFalse(navigationItem.IsActive);

                navigationItem.SetIsActiveIfMatches("1");
                Assert.IsFalse(navigationItem.IsActive);
            }

        }

    }

}
