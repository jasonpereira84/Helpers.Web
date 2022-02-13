using System;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Mvc;

    namespace Extensions
    {
        public static partial class Web
        {
            public static Notification ErrorNotification(String message)
                => Notification.Error(message);

            public static Notification WarningNotification(String message)
                => Notification.Warning(message);

            public static Notification SuccessNotification(String message)
                => Notification.Success(message);

            public static Notification.DecoratorResult<TActionResult> WithNotification<TActionResult>(this TActionResult result, Notification notification)
                where TActionResult : IActionResult
                => new Notification.DecoratorResult<TActionResult>(result, notification);

            public static Notification.DecoratorResult<TActionResult> WithNotification<TActionResult>(this TActionResult result, Notification.options options, Notification.settings? settings = null)
                where TActionResult : IActionResult
                => WithNotification(result, new Notification(options, settings));

            public static Notification.DecoratorResult<TActionResult> WithErrorNotification<TActionResult>(this TActionResult result, String message)
                where TActionResult : IActionResult
                => WithNotification(result, ErrorNotification(message));

            public static Notification.DecoratorResult<TActionResult> WithWarningNotification<TActionResult>(this TActionResult result, String message)
                where TActionResult : IActionResult
                => new Notification.DecoratorResult<TActionResult>(result, WarningNotification(message));

            public static Notification.DecoratorResult<TActionResult> WithSuccessNotification<TActionResult>(this TActionResult result, String message)
                where TActionResult : IActionResult
                => new Notification.DecoratorResult<TActionResult>(result, SuccessNotification(message));
        }
    }
}
