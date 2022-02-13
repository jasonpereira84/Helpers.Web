using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json.Linq;
    using Misc = Extensions.Misc;

    public class HealthCheck : IHealthCheck
    {
        public Func<HealthCheckContext, Task<HealthCheckResult>> CheckHealthFunc { get; protected set; }

        public HealthCheck(Func<HealthCheckContext, Task<HealthCheckResult>> checkHealthFunc)
        {
            CheckHealthFunc = checkHealthFunc ?? throw new ArgumentNullException(nameof(checkHealthFunc));
        }

        public HealthCheck(Func<HealthCheckRegistration, Task<HealthCheckResult>> checkHealthFunc)
        {
            checkHealthFunc = checkHealthFunc ?? throw new ArgumentNullException(nameof(checkHealthFunc));

            CheckHealthFunc = healthCheckContext => checkHealthFunc.Invoke(healthCheckContext.Registration);
        }

        public HealthCheck(Task<HealthCheckResult> checkHealthTask)
        {
            checkHealthTask = checkHealthTask ?? throw new ArgumentNullException(nameof(checkHealthTask));

            CheckHealthFunc = HealthCheckContext => checkHealthTask;
        }

        public HealthCheck(HealthCheckResult healthCheckResult)
            : this(Task.FromResult(healthCheckResult))
        { }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext healthCheckContext, CancellationToken cancellationToken = default(CancellationToken))
            => CheckHealthFunc.Invoke(healthCheckContext);

        public static class Options
        {
            public static HealthCheckOptions From(Func<HealthCheckRegistration, Boolean> predicate, Func<HttpContext, HealthReport, Task> responseWriter = null, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
            {
                predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

                Task _writeAsync(HttpContext httpContext, HealthReport healthReport)
                {
                    httpContext.Response.ContentType = MimeTypes.Text.Plain;
                    return HttpResponseWritingExtensions.WriteAsync(httpContext.Response, $"{healthReport.Status}");
                }

                return new HealthCheckOptions
                {
                    Predicate = predicate,
                    ResponseWriter = responseWriter ?? _writeAsync,
                    AllowCachingResponses = allowCachingResponses,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = resultStatusCodes?[HealthStatus.Healthy] ?? StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = resultStatusCodes?[HealthStatus.Degraded] ?? StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = resultStatusCodes?[HealthStatus.Unhealthy] ?? StatusCodes.Status503ServiceUnavailable
                    }
                };
            }

            public static HealthCheckOptions JsonResponse(Func<HealthCheckRegistration, Boolean> predicate, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
            {
                Task _writeAsync(HttpContext httpContext, HealthReport healthReport)
                => JsonResponseWriter.WriteAsync(
                    httpContext.Response,
                    getJProperties(
                        healthReport
                            .Entries
                            .First()
                            .Value));

                return From(
                    predicate: predicate,
                    responseWriter: _writeAsync,
                    allowCachingResponses: allowCachingResponses,
                    resultStatusCodes: resultStatusCodes);
            }

            public static HealthCheckOptions JsonResponse(String name, Boolean allowCachingResponses = false, IDictionary<HealthStatus, Int32> resultStatusCodes = null)
            {
                var key = name ?? throw new ArgumentNullException(nameof(name));

                Boolean _predicate(HealthCheckRegistration healthCheckRegistration)
                    => Misc.Matches(healthCheckRegistration.Name, key);

                Task _writeAsync(HttpContext httpContext, HealthReport healthReport)
                    => JsonResponseWriter.WriteAsync(
                        httpContext.Response,
                        getJProperties(
                            healthReport
                                .Entries
                                .First(pair => Misc.Matches(pair.Key, key))
                                .Value));

                return From(
                    predicate: _predicate,
                    responseWriter: _writeAsync,
                    allowCachingResponses: allowCachingResponses,
                    resultStatusCodes: resultStatusCodes);
            }

            internal static IEnumerable<JProperty> getJProperties(HealthReportEntry healthReportEntry)
            {
                var retVal = new List<JProperty>();
                {
                    retVal.Add(
                        new JProperty("healthStatus", $"{healthReportEntry.Status}"));

                    if (Misc.EvaluateSanity(healthReportEntry.Description, out String description))
                        retVal.Add(
                            new JProperty("description", description));

                    if (healthReportEntry.Data != null && healthReportEntry.Data.Any())
                        retVal.Add(
                            new JProperty("data",
                                new JObject(
                                    healthReportEntry.Data
                                        .Select(pair => new JProperty(pair.Key, pair.Value)))));
                }
                return retVal;
            }

        }

        public static class Result
        {
            public static HealthCheckResult From(HealthStatus healthStatus, IDictionary<String, Object> data, String description = default(String), Exception exception = default(Exception))
            {
                data = data ?? throw new ArgumentNullException(nameof(data));

                return new HealthCheckResult(healthStatus, description, exception, new ReadOnlyDictionary<String, Object>(data));
            }

            public static HealthCheckResult From(IDictionary<String, Object> data, String description = default(String), Exception exception = default(Exception))
                => From(HealthStatus.Healthy, data, description, exception);
        }
    }

}
