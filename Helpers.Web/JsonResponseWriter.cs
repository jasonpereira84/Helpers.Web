using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;


    public static class JsonResponseWriter
    {
        public static Task WriteAsync(HttpResponse httpResponse, String jsonString)
        {
            httpResponse.ContentType = MimeTypes.Application.Json;
            return HttpResponseWritingExtensions.WriteAsync(httpResponse, jsonString);
        }

        public static Task WriteAsync(HttpResponse httpResponse, JObject jObject, Formatting formatting = Formatting.Indented)
            => WriteAsync(httpResponse, jObject.ToString(formatting));

        public static Task WriteAsync(HttpResponse httpResponse, IEnumerable<JProperty> jProperties, Formatting formatting = Formatting.Indented)
            => WriteAsync(httpResponse, new JObject(content: jProperties.ToArray()), formatting);

        public static Task WriteAsync(HttpResponse httpResponse, Formatting formatting = Formatting.Indented, params JProperty[] jProperties)
            => WriteAsync(httpResponse, new JObject(content: jProperties), formatting);
    }
}
