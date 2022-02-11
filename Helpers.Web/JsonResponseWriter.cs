using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Misc = Extensions.Misc;


    public static class JsonResponseWriter
    {
        public static Task WriteAsync(HttpResponse httpResponse, JObject jObject, Formatting formatting = Formatting.Indented)
        {
            httpResponse.ContentType = MimeTypes.Application.Json;
            return HttpResponseWritingExtensions.WriteAsync(httpResponse, jObject.ToString(formatting));
        }

        public static Task WriteAsync(HttpResponse httpResponse, IEnumerable<JProperty> jProperties, Formatting formatting = Formatting.Indented)
            => WriteAsync(httpResponse, new JObject(content: jProperties.ToArray()), formatting);

        public static Task WriteAsync(HttpResponse httpResponse, Formatting formatting = Formatting.Indented, params JProperty[] jProperties)
            => WriteAsync(httpResponse, new JObject(content: jProperties), formatting);

        //public static Task WriteAsync(HttpResponse httpResponse, HealthStatus healthStatus, String description, IReadOnlyDictionary<String, Object> data, Formatting formatting = Formatting.Indented)
        //{
        //    IEnumerable<JProperty> _getJProperties()
        //    {
        //        var jProperties = new List<JProperty> { new JProperty(nameof(healthStatus), $"{healthStatus}") };
        //        if (Misc.EvaluateSanity(description, out String saneDescription))
        //            jProperties.Add(new JProperty(nameof(description), saneDescription));
        //        if (data != null && data.Any())
        //            jProperties.Add(new JProperty(nameof(data), new JObject(data.Select(pair => new JProperty(pair.Key, pair.Value)))));
        //        return jProperties;
        //    }

        //    return WriteAsync(httpResponse, _getJProperties(), formatting);
        //}

        //public static Task WriteAsync(HttpResponse httpResponse, HealthReportEntry healthReportEntry, Formatting formatting = Formatting.Indented)
        //    => WriteAsync(httpResponse, healthReportEntry.Status, healthReportEntry.Description, healthReportEntry.Data, formatting);

        //public static Task WriteAsync(HttpResponse httpResponse, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter, Formatting formatting = Formatting.Indented)
        //    => WriteAsync(httpResponse, healthReportEntryGetter.Invoke(healthReport), formatting);

        //public static Task WriteAsync(HttpResponse httpResponse, HealthReport healthReport, String key, Formatting formatting = Formatting.Indented)
        //    => WriteAsync(httpResponse, healthReport, hr => hr.Entries.First(pair => Misc.Matches(pair.Key, key)).Value, formatting);

        //public static Task WriteAsync(HttpResponse httpResponse, HealthReport healthReport, Formatting formatting = Formatting.Indented)
        //    => WriteAsync(httpResponse, healthReport, hr => hr.Entries.First().Value, formatting);
    }
}
