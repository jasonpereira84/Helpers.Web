using System;
using System.Reflection;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;

        public static partial class Web
        {
            public static IWebHostBuilder UseDefaultServiceProvider(this IWebHostBuilder webHostBuilder)
                => webHostBuilder.UseDefaultServiceProvider(
                    (webHostBuilderContext, serviceProviderOptions) =>
                    {
                        serviceProviderOptions.ValidateScopes = webHostBuilderContext.HostingEnvironment.IsDevelopment();
                    });

            public static (IWebHost WebHost, WebHostBuilderContext WebHostBuilderContext) BuildWithContext(this IWebHostBuilder webHostBuilder)
            {
                var thisValueWillOnlyBeSetAfterBuildIsInvoked = default(WebHostBuilderContext);
                webHostBuilder.ConfigureServices(
                    (context, services) => { thisValueWillOnlyBeSetAfterBuildIsInvoked = context; });
                return (webHostBuilder.Build(), thisValueWillOnlyBeSetAfterBuildIsInvoked);
            }

            public static Assembly GetAssembly(this WebHostBuilderContext webHostBuilderContext)
                => Assembly.Load(
                    new AssemblyName(
                        webHostBuilderContext.HostingEnvironment.ApplicationName));
        }
    }
}
