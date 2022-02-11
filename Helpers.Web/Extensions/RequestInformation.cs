using System;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;

        public static partial class Web
        {
            public static RequestInformation GetInformation(this HttpRequest httpRequest, out RequestInformation requestInformation)
                => requestInformation = new RequestInformation(httpRequest);

            public static ControllerActionRequestInformation GetInformation(this ControllerContext controllerContext, out ControllerActionRequestInformation requestInformation)
                => requestInformation = new ControllerActionRequestInformation(controllerContext);

            public static ControllerActionRequestInformation GetInformation(this ControllerBase controllerBase, out ControllerActionRequestInformation requestInformation)
                => requestInformation = new ControllerActionRequestInformation(controllerBase);

            public static RequestInformation GetInformation(this PageModel pageModel, out RequestInformation requestInformation)
                => requestInformation = new RequestInformation(pageModel.Request);

            public static String IfSane<TRequestInformation>(this TRequestInformation requestInformation, Func<String, String> messageGenerator)
                where TRequestInformation : _RequestInformation
                => Misc.EvaluateSanity(requestInformation?.Id, out String requestId)
                    ? messageGenerator?.Invoke(requestId) ?? requestId
                    : String.Empty;
        }
    }
}
