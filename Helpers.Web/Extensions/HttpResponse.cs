using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;
        using Microsoft.Extensions.Diagnostics.HealthChecks;

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

            //public static Task WriteAsync(this HttpResponse httpResponse, HealthStatus healthStatus, String description, IReadOnlyDictionary<String, Object> data, Formatting formatting = Formatting.Indented)
            //    => JsonResponseWriter.WriteAsync(httpResponse, healthStatus, description, data, formatting);

            //public static Task WriteAsync(this HttpResponse httpResponse, HealthReportEntry healthReportEntry, Formatting formatting = Formatting.Indented)
            //    => JsonResponseWriter.WriteAsync(httpResponse, healthReportEntry, formatting);

            //public static Task WriteAsync(this HttpResponse httpResponse, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter, Formatting formatting = Formatting.Indented)
            //    => JsonResponseWriter.WriteAsync(httpResponse, healthReport, healthReportEntryGetter);

            //public static Task WriteAsync(this HttpResponse httpResponse, HealthReport healthReport, String key, Formatting formatting = Formatting.Indented)
            //    => JsonResponseWriter.WriteAsync(httpResponse, healthReport, key, formatting);

            //public static Task WriteAsync(this HttpResponse httpResponse, HealthReport healthReport, Formatting formatting = Formatting.Indented)
            //    => JsonResponseWriter.WriteAsync(httpResponse, healthReport, formatting);
        }
    }
}
