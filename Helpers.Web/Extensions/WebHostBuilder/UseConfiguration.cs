using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;
        using Microsoft.Extensions.Configuration;

        public static partial class Web
        {
            internal static Boolean getBoolean(IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate)
            {
                var predicateResult = default(Boolean);
                webHostBuilder.ConfigureAppConfiguration(
                    (context, builder) => { predicateResult = predicate.Invoke(context, builder); });
                return predicateResult;
            }

            private static Boolean getBoolean(IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate)
                => getBoolean(webHostBuilder, (context, builder) => predicate.Invoke(builder.Build()));

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, IConfiguration configuration)
                => predicate.IsFalse() ? webHostBuilder : HostingAbstractionsWebHostBuilderExtensions.UseConfiguration(webHostBuilder, configuration);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, IConfiguration configuration)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configuration);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, IConfiguration configuration)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configuration);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfiguration> configureDelegate)
                => HostingAbstractionsWebHostBuilderExtensions.UseConfiguration(webHostBuilder, configureDelegate());

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, Func<IConfiguration> configureDelegate)
                => predicate.IsFalse() ? webHostBuilder : UseConfiguration(webHostBuilder, configureDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Func<IConfiguration> configureDelegate)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configureDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Func<IConfiguration> configureDelegate)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configureDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Action<WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => webHostBuilder.ConfigureAppConfiguration(configurationDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, Action<WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => predicate.IsFalse() ? webHostBuilder : UseConfiguration(webHostBuilder, configurationDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Action<WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configurationDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Action<WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configurationDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Action<IWebHostBuilder, WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => webHostBuilder.ConfigureAppConfiguration(
                        (context, builder) => configurationDelegate.Invoke(webHostBuilder, context, builder));

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, Action<IWebHostBuilder, WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => predicate.IsFalse() ? webHostBuilder : UseConfiguration(webHostBuilder, configurationDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Action<IWebHostBuilder, WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configurationDelegate);

            public static IWebHostBuilder UseConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Action<IWebHostBuilder, WebHostBuilderContext, IConfigurationBuilder> configurationDelegate)
                => UseConfiguration(webHostBuilder, getBoolean(webHostBuilder, predicate), configurationDelegate);
        }
    }
}
