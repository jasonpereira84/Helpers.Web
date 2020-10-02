using System;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;
        using Microsoft.Extensions.Configuration;

        using IDictionary = IDictionary<String, String>;

        public static partial class Web
        {
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Boolean predicate, String environment)
                => predicate.IsFalse() ? webHostBuilder : HostingAbstractionsWebHostBuilderExtensions.UseEnvironment(webHostBuilder, environment);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, String environment)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), environment);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, String environment)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), environment);

            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<String> environmentDelegate)
                => HostingAbstractionsWebHostBuilderExtensions.UseEnvironment(webHostBuilder, environmentDelegate.Invoke());
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Boolean predicate, Func<String> environmentDelegate)
                => predicate.IsFalse() ? webHostBuilder : UseEnvironment(webHostBuilder, environmentDelegate);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Func<String> environmentDelegate)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), environmentDelegate);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Func<String> environmentDelegate)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), environmentDelegate);

            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, IDictionary dictionary, String defaultValue = "Production")
                => HostingAbstractionsWebHostBuilderExtensions.UseEnvironment(webHostBuilder, dictionary.GetEnvironment(defaultValue));
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Boolean predicate, IDictionary dictionary)
                => predicate.IsFalse() ? webHostBuilder : UseEnvironment(webHostBuilder, dictionary);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, IDictionary dictionary)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), dictionary);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, IDictionary dictionary)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), dictionary);


            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<IDictionary> dictionaryDelegate, String defaultValue = "Production")
                => UseEnvironment(webHostBuilder, dictionaryDelegate.Invoke(), defaultValue);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Boolean predicate, Func<IDictionary> dictionaryDelegate)
                => predicate.IsFalse() ? webHostBuilder : UseEnvironment(webHostBuilder, dictionaryDelegate);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Func<IDictionary> dictionaryDelegate)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), dictionaryDelegate);
            public static IWebHostBuilder UseEnvironment(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Func<IDictionary> dictionaryDelegate)
                => UseEnvironment(webHostBuilder, getBoolean(webHostBuilder, predicate), dictionaryDelegate);

        }
    }
}
