using System;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class HealthCheck : IHealthCheck
    {
        public Func<HealthCheckContext, Task<HealthCheckResult>> CheckHealthFunc { get; protected set; }

        public HealthCheck(Func<HealthCheckContext, Task<HealthCheckResult>> func)
         => CheckHealthFunc = func;

        public HealthCheck(Func<HealthCheckRegistration, Task<HealthCheckResult>> func)
            : this(context => func.Invoke(context.Registration)) 
        { }

        public HealthCheck(Task<HealthCheckResult> task)
            : this(new Func<HealthCheckContext, Task<HealthCheckResult>>(context => task))
        { }

        public HealthCheck(HealthCheckResult healthCheckResult)
            : this(Task.FromResult(healthCheckResult))
        { }


        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
            => CheckHealthFunc.Invoke(context);

        #region JsonResponseWriter
        public static Task JsonResponseWriter(HttpContext httpContext, JObject jObject, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, jObject, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, JObject jObject)
            => Extensions.Web.JsonResponseWriter(httpContext, jObject, Formatting.Indented);

        public static Task JsonResponseWriter(HttpContext httpContext, IEnumerable<JProperty> jProperties, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, jProperties, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, IEnumerable<JProperty> jProperties)
            => Extensions.Web.JsonResponseWriter(httpContext, jProperties, Formatting.Indented);

        public static Task JsonResponseWriter(HttpContext httpContext, Formatting formatting, params JProperty[] jProperties)
            => Extensions.Web.JsonResponseWriter(httpContext, formatting, jProperties);
        public static Task JsonResponseWriter(HttpContext httpContext, params JProperty[] jProperties)
            => Extensions.Web.JsonResponseWriter(httpContext, Formatting.Indented, jProperties);

        public static Task JsonResponseWriter(HttpContext httpContext, HealthStatus status, String description, IReadOnlyDictionary<String, Object> data, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, status, description, data, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, HealthStatus status, String description, IReadOnlyDictionary<String, Object> data)
            => Extensions.Web.JsonResponseWriter(httpContext, status, description, data, Formatting.Indented);

        public static Task JsonResponseWriter(HttpContext httpContext, HealthReportEntry healthReportEntry, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReportEntry, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, HealthReportEntry healthReportEntry)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReportEntry, Formatting.Indented);

        public static Task JsonResponseWriter(HttpContext httpContext, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReport, healthReportEntryGetter, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, HealthReport healthReport, Func<HealthReport, HealthReportEntry> healthReportEntryGetter)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReport, healthReportEntryGetter, Formatting.Indented);

        public static Task JsonResponseWriter(HttpContext httpContext, HealthReport healthReport, String key, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReport, key, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, HealthReport healthReport, String key)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReport, key, Formatting.Indented);

        public static Task JsonResponseWriter(HttpContext httpContext, HealthReport healthReport, Formatting formatting)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReport, formatting);
        public static Task JsonResponseWriter(HttpContext httpContext, HealthReport healthReport)
            => Extensions.Web.JsonResponseWriter(httpContext, healthReport, Formatting.Indented);
        #endregion JsonResponseWriter

        public static class Options
        {
            public static HealthCheckOptions From(Func<HealthCheckRegistration, Boolean> predicate, Func<HttpContext, HealthReport, Task> responseWriter = null, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
            {
                Task _writeMinimalPlaintext(HttpContext httpContext, HealthReport healthReport)
                {
                    httpContext.Response.ContentType = MimeTypes.Text.Plain;
                    return HttpResponseWritingExtensions.WriteAsync(httpContext.Response, $"{healthReport.Status}");
                }

                return new HealthCheckOptions
                {
                    Predicate = predicate,
                    AllowCachingResponses = allowCachingResponses,
                    ResponseWriter = responseWriter ?? _writeMinimalPlaintext,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = resultStatusCodes?[HealthStatus.Healthy] ?? StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = resultStatusCodes?[HealthStatus.Degraded] ?? StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = resultStatusCodes?[HealthStatus.Unhealthy] ?? StatusCodes.Status503ServiceUnavailable
                    }
                };
            }

            public static HealthCheckOptions From(Func<HealthStatus, Boolean> predicatedOnStatus, Func<HttpContext, HealthReport, Task> responseWriter = null, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
                => From(new Func<HealthCheckRegistration, Boolean>(check => predicatedOnStatus.Invoke(check.FailureStatus)), responseWriter, allowCachingResponses, resultStatusCodes);

            public static HealthCheckOptions From(Func<String, Boolean> predicatedOnName, Func<HttpContext, HealthReport, Task> responseWriter = null, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
                => From(new Func<HealthCheckRegistration, Boolean>(check => predicatedOnName.Invoke(check.Name)), responseWriter, allowCachingResponses, resultStatusCodes);

            public static HealthCheckOptions From(Func<ISet<String>, Boolean> predicatedOnTags, Func<HttpContext, HealthReport, Task> responseWriter = null, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
                => From(new Func<HealthCheckRegistration, Boolean>(check => predicatedOnTags.Invoke(check.Tags)), responseWriter, allowCachingResponses, resultStatusCodes);

            public static HealthCheckOptions JsonResponse(Func<HealthCheckRegistration, Boolean> predicate, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
                => From(predicate, JsonResponseWriter, allowCachingResponses, resultStatusCodes);

            public static HealthCheckOptions JsonResponse(String name, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
            {
                var key = name ?? throw new ArgumentNullException(nameof(name));
                return From(
                    (n) => Extensions.Misc.Matches(n, key),
                    (httpContext, healthReport) => JsonResponseWriter(httpContext, healthReport, key),
                    allowCachingResponses,
                    resultStatusCodes);
            }
        }
    }

}
