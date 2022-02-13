using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;

        public static partial class Web
        {
            public static RequestInformation GetInformation(this HttpRequest httpRequest)
                => new RequestInformation
                {
                    Id = httpRequest.HttpContext?.TraceIdentifier,
                    ContentType = httpRequest.ContentType,
                    ContentLength = httpRequest.ContentLength?.ToString(),
                    Protocol = httpRequest.Protocol,
                    Scheme = httpRequest.ContentType,
                    Method = httpRequest.Method,
                    Path = httpRequest.Path.Value,
                    QueryString = httpRequest.QueryString.Value,
                };

            public static Boolean TryGetInformation(this HttpRequest httpRequest, out RequestInformation requestInformation)
            {
                try
                {
                    requestInformation = GetInformation(httpRequest);
                    return true;
                }
                catch
                {
                    requestInformation = default(RequestInformation);
                    return false;
                }
            }

        }
    }
}
