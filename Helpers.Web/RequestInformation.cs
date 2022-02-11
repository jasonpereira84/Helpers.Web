using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using Misc = Extensions.Misc;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public abstract class _RequestInformation
    {
        public String Id { get; set; }

        public String ContentType { get; set; }

        public String ContentLength { get; set; }

        public String Protocol { get; set; }

        public String Scheme { get; set; }

        public String Method { get; set; }

        public String Path { get; set; }

        public String QueryString { get; set; }

        protected _RequestInformation(HttpRequest httpRequest)
        {
            Id = httpRequest?.HttpContext?.TraceIdentifier;
            ContentType = httpRequest?.ContentType;
            ContentLength = httpRequest?.ContentLength?.ToString();
            Protocol = httpRequest?.Protocol;
            Scheme = httpRequest?.ContentType;
            Method = httpRequest?.Method;
            Path = httpRequest?.Path;
            QueryString = httpRequest?.QueryString.ToString();
        }
    }

    public sealed class RequestInformation : _RequestInformation
    {
        public RequestInformation(HttpRequest httpRequest)
            : base(httpRequest)
        { }

        public override String ToString()
            => JsonConvert.SerializeObject(this);
    }

    public sealed class ControllerActionRequestInformation : _RequestInformation
    {
        public String Controller { get; set; }

        public String Action { get; set; }

        public ControllerActionRequestInformation(ControllerContext controllerContext)
            : base(controllerContext?.HttpContext?.Request)
        {
            var routeValueDictionary = controllerContext?.RouteData?.Values ?? default(IDictionary<String, Object>);
            if (routeValueDictionary != null && routeValueDictionary.Any()) 
            {
                String _getValue(String key)
                {
                    if (Misc.ReallyTryGetValueOrDefault(routeValueDictionary, key, out Object obj))
                    {
                        return Misc.IfNotNullOrEmptyOrWhiteSpace(
                            obj?.ToString() ?? default(String),
                            value => value.Trim());
                    }

                    return default(String);
                }

                Controller = _getValue("Controller");
                Action = _getValue("Action");
            }
        }

        public ControllerActionRequestInformation(ControllerBase controllerBase)
            : this(controllerBase.ControllerContext) { }

        public override String ToString()
            => JsonConvert.SerializeObject(this);
    }
}
