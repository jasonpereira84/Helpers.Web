using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;

        using Newtonsoft.Json;
        using Newtonsoft.Json.Linq;

        public static partial class Web
        {
            public static Task WriteAsync(this HttpResponse httpResponse, JObject jObject, Formatting formatting = Formatting.Indented)
                => JsonResponseWriter.WriteAsync(httpResponse, jObject, formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, IEnumerable<JProperty> jProperties, Formatting formatting = Formatting.Indented)
                => JsonResponseWriter.WriteAsync(httpResponse, jProperties, formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, Formatting formatting = Formatting.Indented, params JProperty[] jProperties)
                => JsonResponseWriter.WriteAsync(httpResponse, formatting, jProperties);
        }
    }
}
