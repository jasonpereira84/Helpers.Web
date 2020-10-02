using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            public static Task WriteAsync(this HttpResponse httpResponse, AjaxResult jsonResult, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
            {
                httpResponse.ContentType = jsonResult.ContentType;
                return HttpResponseWritingExtensions.WriteAsync(httpResponse, jsonResult.Data, encoding, cancellationToken);
            }

            public static Task WriteAsync(this HttpResponse httpResponse, AjaxResult jsonResult, CancellationToken cancellationToken = default(CancellationToken))
            {
                httpResponse.ContentType = jsonResult.ContentType;
                return HttpResponseWritingExtensions.WriteAsync(httpResponse, jsonResult.Data, cancellationToken);
            }

            public static Task WriteAsync(this HttpResponse httpResponse, AjaxResult jsonResult)
            {
                httpResponse.ContentType = jsonResult.ContentType;
                return HttpResponseWritingExtensions.WriteAsync(httpResponse, jsonResult.Data);
            }

            public static Task WriteAsync(this HttpResponse httpResponse, JObject jObject, Formatting formatting = Formatting.Indented)
            {
                httpResponse.ContentType = MimeTypes.Application.Json;
                return HttpResponseWritingExtensions.WriteAsync(httpResponse, jObject.ToString(formatting));
            }

            public static Task WriteAsync(this HttpResponse httpResponse, IEnumerable<JProperty> jProperties, Formatting formatting = Formatting.Indented)
                => WriteAsync(httpResponse, new JObject(content: jProperties.ToArray()), formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, Formatting formatting = Formatting.Indented, params JProperty[] jProperties)
                => WriteAsync(httpResponse, new JObject(content: jProperties), formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, HealthStatus status, String description, IReadOnlyDictionary<String, Object> data, Formatting formatting = Formatting.Indented)
            {
                IEnumerable<JProperty> _getJProperties()
                {
                    var jProperties = new List<JProperty> { new JProperty(nameof(status), $"{status}") };
                    if (description.EvaluateSanity(out description)) { jProperties.Add(new JProperty(nameof(description), description)); }
                    if (data.IsNotNullOrNone()) { jProperties.Add(new JProperty(nameof(data), new JObject(data.Select(pair => new JProperty(pair.Key, pair.Value))))); }
                    return jProperties;
                }
                return WriteAsync(httpResponse, _getJProperties(), formatting);
            }

            public static Task WriteAsync(this HttpResponse httpResponse, HealthReportEntry healthReportEntry, Formatting formatting = Formatting.Indented)
                => WriteAsync(httpResponse, healthReportEntry.Status, healthReportEntry.Description, healthReportEntry.Data, formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter, Formatting formatting = Formatting.Indented)
                => WriteAsync(httpResponse, healthReportEntryGetter.Invoke(healthReport), formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, HealthReport healthReport, String key, Formatting formatting = Formatting.Indented)
                => WriteAsync(httpResponse, healthReport, hr => hr.Entries.First(pair => pair.Key.Matches(key)).Value, formatting);

            public static Task WriteAsync(this HttpResponse httpResponse, HealthReport healthReport, Formatting formatting = Formatting.Indented)
                => WriteAsync(httpResponse, healthReport, hr => hr.Entries.First().Value, formatting);
        }
    }
}
