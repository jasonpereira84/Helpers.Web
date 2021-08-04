using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;
        using Microsoft.Extensions.Configuration;

        using Newtonsoft.Json;

        using IDictionary = IDictionary<String, String>;

        public static partial class Web
        {
            public static IWebHostBuilder UseDefaultServiceProvider(this IWebHostBuilder webHostBuilder)
                => webHostBuilder.UseDefaultServiceProvider(
                    (webHostBuilderContext, serviceProviderOptions) =>
                    {
                        serviceProviderOptions.ValidateScopes = webHostBuilderContext.HostingEnvironment.IsDevelopment();
                    });

            public static T GetValue<T>(this WebHostBuilderContext webHostBuilderContext, String key, out T value)
                => Configuration.GetValue(webHostBuilderContext.Configuration, key, out value);

            public static IConfigurationSection GetSection(this WebHostBuilderContext webHostBuilderContext, String key, out IConfigurationSection configurationSection)
                => Configuration.GetSection(webHostBuilderContext.Configuration, key, out configurationSection);

            public static T GetObject<T>(this WebHostBuilderContext webHostBuilderContext, String key, T defaultValue = default(T))
                where T : class, new()
                => Configuration.GetObject(webHostBuilderContext.Configuration, key, defaultValue);

            public static T GetObject<T>(this WebHostBuilderContext webHostBuilderContext, String key, out T obj, T defaultValue = default(T))
                where T : class, new()
                => Configuration.GetObject(webHostBuilderContext.Configuration, key, out obj, defaultValue);

            public static IWebHost Build(this IWebHostBuilder webHostBuilder, out IWebHost webHost)
                => webHost = webHostBuilder.Build();

            public static IWebHost Build(this IWebHostBuilder webHostBuilder, out IWebHost webHost, out WebHostBuilderContext webHostBuilderContext)
            {
                var thisValueWillOnlyBeSetAfterBuildIsInvoked = default(WebHostBuilderContext);
                webHostBuilder.ConfigureServices(
                    (context, services) => { thisValueWillOnlyBeSetAfterBuildIsInvoked = context; });
                webHost = webHostBuilder.Build();
                webHostBuilderContext = thisValueWillOnlyBeSetAfterBuildIsInvoked;
                return webHost;
            }

            public static String GetValidOrDefault(this WebHostBuilderContext webHostBuilderContext, String key, String defaultValue)
                => ConfigurationBinder.GetValue<String>(webHostBuilderContext.Configuration, key).SanitizeTo(defaultValue);

            public static TEnum GetValidOrDefault<TEnum>(this WebHostBuilderContext webHostBuilderContext, String key, TEnum defaultValue)
                where TEnum : struct, IConvertible
                => Enum.Parse<TEnum>(GetValidOrDefault(webHostBuilderContext, key, Enum.GetName(typeof(TEnum), defaultValue)));

            public static Assembly GetAssembly(this WebHostBuilderContext webHostBuilderContext)
                => Assembly.Load(
                    new AssemblyName(
                        webHostBuilderContext.HostingEnvironment.ApplicationName));

            public static IDictionary GetProperties_UserSecrets(this WebHostBuilderContext webHostBuilderContext, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                => Json.UserSecrets.GetProperties(GetAssembly(webHostBuilderContext), delimiter, dateParseHandling, optional);

            public static WebHostBuilderContext GetWebHostBuilderContext(this IWebHostBuilder webHostBuilder)
            {
                _ = Build(webHostBuilder, out _, out WebHostBuilderContext webHostBuilderContext);
                return webHostBuilderContext;
            }

        }
    }
}
