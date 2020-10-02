using System;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Http;
        using Microsoft.AspNetCore.Diagnostics;
        using Microsoft.Extensions.Diagnostics.HealthChecks;

        using Newtonsoft.Json;
        using Newtonsoft.Json.Linq;

        public static partial class Web
        {
            public static Boolean TryGet<TFeature>(this HttpContext httpContext, Func<TFeature, Boolean> predicate, out TFeature feature, TFeature defaultValue = default(TFeature))
                => TryGet(httpContext?.Features, predicate, out feature, defaultValue);
            
            public static Boolean TryGetException(this HttpContext httpContext, out Exception exception, Exception defaultValue)
                => TryGetException(httpContext?.Features, out exception, defaultValue);

            public static Boolean TryGetExceptionHandlerPathFeature(this HttpContext httpContext, out (Exception Exception, String Path) feature, (Exception Exception, String Path) defaultValue)
                => TryGetExceptionAndPath(httpContext?.Features, out feature, defaultValue);

            public static Boolean TryGetException(this HttpContext httpContext, out Exception exception)
                => TryGetException(httpContext?.Features, out exception);

            public static Boolean TryGetExceptionHandlerPathFeature(this HttpContext httpContext, out (Exception Exception, String Path) feature)
                => TryGetExceptionAndPath(httpContext?.Features, out feature);

            public static Task JsonResponseWriter(this HttpContext httpContext, JObject jObject, Formatting formatting)
                => WriteAsync(httpContext.Response, jObject, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, JObject jObject)
                => JsonResponseWriter(httpContext, jObject, Formatting.Indented);

            public static Task JsonResponseWriter(this HttpContext httpContext, IEnumerable<JProperty> jProperties, Formatting formatting)
                => WriteAsync(httpContext.Response, jProperties, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, IEnumerable<JProperty> jProperties)
                => JsonResponseWriter(httpContext, jProperties, Formatting.Indented);

            public static Task JsonResponseWriter(this HttpContext httpContext, Formatting formatting, params JProperty[] jProperties)
                => WriteAsync(httpContext.Response, formatting, jProperties);
            public static Task JsonResponseWriter(this HttpContext httpContext, params JProperty[] jProperties)
                => JsonResponseWriter(httpContext, Formatting.Indented, jProperties);

            public static Task JsonResponseWriter(this HttpContext httpContext, HealthStatus status, String description, IReadOnlyDictionary<String, Object> data, Formatting formatting)
                => WriteAsync(httpContext.Response, status, description, data, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, HealthStatus status, String description, IReadOnlyDictionary<String, Object> data)
                => JsonResponseWriter(httpContext, status, description, data, Formatting.Indented);

            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReportEntry healthReportEntry, Formatting formatting)
                => WriteAsync(httpContext.Response, healthReportEntry, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReportEntry healthReportEntry)
                => JsonResponseWriter(httpContext, healthReportEntry, Formatting.Indented);

            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter, Formatting formatting)
                => WriteAsync(httpContext.Response, healthReport, healthReportEntryGetter, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter)
                => JsonResponseWriter(httpContext, healthReport, healthReportEntryGetter, Formatting.Indented);

            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReport healthReport, String key, Formatting formatting)
                => WriteAsync(httpContext.Response, healthReport, key, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReport healthReport, String key)
                => JsonResponseWriter(httpContext, healthReport, key, Formatting.Indented);

            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReport healthReport, Formatting formatting)
                => WriteAsync(httpContext.Response, healthReport, formatting);
            public static Task JsonResponseWriter(this HttpContext httpContext, HealthReport healthReport)
                => JsonResponseWriter(httpContext, healthReport, Formatting.Indented);
        }
    }
}