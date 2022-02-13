using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers.Web.Tests
{
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    [TestClass]
    public class Test_HealthCheck
    {
        [TestMethod]
        public void ctor()
        {
            {
                {
                    var checkHealthFunc = new Func<HealthCheckContext, Task<HealthCheckResult>>(
                        healthCheckContext => Task.FromResult(default(HealthCheckResult)));

                    var healthCheck = new HealthCheck(checkHealthFunc);
                    Assert.IsNotNull(healthCheck);
                    Assert.AreSame(
                        expected: checkHealthFunc,
                        actual: healthCheck.CheckHealthFunc);
                }

                {
                    Assert.ThrowsException<ArgumentNullException>(
                        () => new HealthCheck(default(Func<HealthCheckContext, Task<HealthCheckResult>>)));
                }

            }

            {
                {
                    var checkHealthFunc = new Func<HealthCheckRegistration, Task<HealthCheckResult>>(
                        healthCheckRegistration => Task.FromResult(default(HealthCheckResult)));

                    var healthCheck = new HealthCheck(checkHealthFunc);
                    Assert.IsNotNull(healthCheck);
                }

                {
                    Assert.ThrowsException<ArgumentNullException>(
                        () => new HealthCheck(default(Func<HealthCheckRegistration, Task<HealthCheckResult>>)));
                }

            }

            {
                {
                    var checkHealthTask = Task.FromResult(default(HealthCheckResult));

                    var healthCheck = new HealthCheck(checkHealthTask);
                    Assert.IsNotNull(healthCheck);
                }

                {
                    Assert.ThrowsException<ArgumentNullException>(
                        () => new HealthCheck(default(Task<HealthCheckResult>)));
                }

            }

        }

        [TestMethod]
        public async Task CheckHealthAsync()
        {
            var healthCheckResult = new HealthCheckResult(HealthStatus.Degraded);
            var checkHealthFunc = new Func<HealthCheckContext, Task<HealthCheckResult>>(
                healthCheckContext => Task.FromResult(healthCheckResult));

            var healthCheck = new HealthCheck(checkHealthFunc);
            Assert.AreEqual(
                expected: healthCheckResult.Status,
                actual: (await healthCheck.CheckHealthFunc(default)).Status);

        }

    }

    [TestClass]
    public class Test_HealthCheckOptions
    {
        [TestMethod]
        public void From()
        {
            {
                var predicate = new Func<HealthCheckRegistration, Boolean>(
                    healthCheckRegistration => true);
                var responseWriter = new Func<HttpContext, HealthReport, Task>(
                    (httpontext, healthReport) => Task.CompletedTask);
                var allowCachingResponses = true;
                var resultStatusCodes = new Dictionary<HealthStatus, Int32>
                {
                    { HealthStatus.Healthy, StatusCodes.Status200OK },
                    { HealthStatus.Degraded, StatusCodes.Status300MultipleChoices },
                    { HealthStatus.Unhealthy, StatusCodes.Status503ServiceUnavailable }
                };

                var healthCheckOptions = HealthCheck.Options
                    .From(predicate, responseWriter, allowCachingResponses, resultStatusCodes);
                Assert.IsNotNull(healthCheckOptions);
                Assert.IsInstanceOfType(healthCheckOptions, typeof(HealthCheckOptions));
                Assert.AreSame(
                    expected: predicate,
                    actual: healthCheckOptions.Predicate);
                Assert.AreSame(
                    expected: responseWriter,
                    actual: healthCheckOptions.ResponseWriter);
                Assert.AreEqual(
                    expected: allowCachingResponses,
                    actual: healthCheckOptions.AllowCachingResponses);
                foreach (var pair in resultStatusCodes)
                {
                    Assert.IsTrue(healthCheckOptions.ResultStatusCodes.ContainsKey(pair.Key));
                    Assert.AreEqual(
                        expected: pair.Value,
                        actual: healthCheckOptions.ResultStatusCodes[pair.Key]);
                }
            }

            {
                var predicate = new Func<HealthCheckRegistration, Boolean>(
                    healthCheckRegistration => true);
                var resultStatusCodes = new Dictionary<HealthStatus, Int32>
                {
                    { HealthStatus.Healthy, StatusCodes.Status200OK },
                    { HealthStatus.Degraded, StatusCodes.Status200OK },
                    { HealthStatus.Unhealthy, StatusCodes.Status503ServiceUnavailable }
                };

                var healthCheckOptions = HealthCheck.Options
                    .From(predicate);
                Assert.IsNotNull(healthCheckOptions.ResponseWriter);
                Assert.IsFalse(healthCheckOptions.AllowCachingResponses);
                foreach (var pair in resultStatusCodes)
                {
                    Assert.IsTrue(healthCheckOptions.ResultStatusCodes.ContainsKey(pair.Key));
                    Assert.AreEqual(
                        expected: pair.Value,
                        actual: healthCheckOptions.ResultStatusCodes[pair.Key]);
                }
            }

            {

                Assert.ThrowsException<ArgumentNullException>(
                    () => HealthCheck.Options.From(default(Func<HealthCheckRegistration, Boolean>)));
            }

        }

        [TestMethod]
        public void JsonResponse()
        {
            {
                {
                    var predicate = new Func<HealthCheckRegistration, Boolean>(
                        healthCheckRegistration => true);

                    var healthCheckOptions = HealthCheck.Options
                        .JsonResponse(predicate);
                    Assert.IsNotNull(healthCheckOptions.ResponseWriter);
                }

                {

                    Assert.ThrowsException<ArgumentNullException>(
                        () => HealthCheck.Options.JsonResponse(default(Func<HealthCheckRegistration, Boolean>)));
                }

            }

            {
                {
                    var healthCheckOptions = HealthCheck.Options
                        .JsonResponse("1");
                    Assert.IsNotNull(healthCheckOptions.ResponseWriter);
                }

            }

        }

    }

    [TestClass]
    public class Test_HealthCheckResult
    {
        [TestMethod]
        public void From()
        {
            {
                var healthStatus = HealthStatus.Degraded;
                var data = new Dictionary<String, Object>();
                var description = "1";
                var exception = new Exception();

                var healthCheckResult = HealthCheck.Result
                    .From(healthStatus, data, description, exception);
                Assert.IsNotNull(healthCheckResult);
                Assert.IsInstanceOfType(healthCheckResult, typeof(HealthCheckResult));
                Assert.AreEqual(
                    expected: healthStatus,
                    actual: healthCheckResult.Status);
                Assert.IsNotNull(healthCheckResult.Data);
                Assert.AreEqual(
                    expected: description,
                    actual: healthCheckResult.Description);
                Assert.AreSame(
                    expected: exception,
                    actual: healthCheckResult.Exception);
            }

            {

                Assert.ThrowsException<ArgumentNullException>(
                    () => HealthCheck.Result.From(HealthStatus.Degraded, default(IDictionary<String, Object>)));
            }

        }

    }

}
