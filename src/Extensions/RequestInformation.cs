using System;
using System.Linq;
using System.Collections.Generic;

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

            public static MvcRequestInformation GetInformation(this ControllerContext controllerContext, out MvcRequestInformation requestInformation)
                => requestInformation = new MvcRequestInformation(controllerContext);

            public static MvcRequestInformation GetInformation(this ControllerBase controllerBase, out MvcRequestInformation requestInformation)
                => requestInformation = new MvcRequestInformation(controllerBase);

            public static RequestInformation GetInformation(this PageModel pageModel, out RequestInformation requestInformation)
                => requestInformation = new RequestInformation(pageModel.Request);

            public static String IfSane<TRequestInformation>(this TRequestInformation requestInformation, Func<String, String> messageGenerator)
                where TRequestInformation : _RequestInformation
                => (requestInformation?.Id)
                        .EvaluateSanity(out String requestId)
                        .IsFalse() ? String.Empty
                            : messageGenerator?.Invoke(requestId) ?? requestId;
        }
    }
}
