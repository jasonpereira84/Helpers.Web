using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers.Web.Tests
{
    using Microsoft.AspNetCore.Mvc;

    [TestClass]
    public class Test_Notification_settings
    {
        [TestMethod]
        public void Sanitize()
        {
            var settings = new Notification.settings
            {
                Delay = 0
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => settings.Sanitize());
        }

    }

    [TestClass]
    public class Test_Notification_DecoratorResult
    {
        [TestMethod]
        public void ctor()
        {
            {
                var result = new ViewResult();
                var options = new Notification.options();
                var notification = new Notification(options);

                var decoratorResult = new Notification.DecoratorResult<ViewResult>(result, notification);
                Assert.IsNotNull(decoratorResult);
                Assert.AreSame(
                    expected: result,
                    actual: decoratorResult.Result);
                Assert.AreSame(
                    expected: notification,
                    actual: decoratorResult.Notification);
            }

            {
                var options = new Notification.options();
                var notification = new Notification(options);

                Assert.ThrowsException<ArgumentNullException>(
                    () => new Notification.DecoratorResult<ViewResult>(default(ViewResult), notification));
            }

            {
                var result = new ViewResult();

                Assert.ThrowsException<ArgumentNullException>(
                    () => new Notification.DecoratorResult<ViewResult>(result, default(Notification)));
            }

        }

        [TestMethod]
        public async Task ExecuteResultAsync()
        {
            {
                var result = new ViewResult();
                var options = new Notification.options();
                var notification = new Notification(options);
                var decoratorResult = new Notification.DecoratorResult<ViewResult>(result, notification);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => decoratorResult.ExecuteResultAsync(default(ActionContext)));
            }

            {
                var result = new ViewResult();
                var options = new Notification.options();
                var notification = new Notification(options);
                var decoratorResult = new Notification.DecoratorResult<ViewResult>(result, notification);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                    () => decoratorResult.ExecuteResultAsync(new ActionContext()));
            }

        }

    }

}
