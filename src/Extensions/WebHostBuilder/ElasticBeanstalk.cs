using System;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;
        using Microsoft.Extensions.Configuration;

        using Dictionary = Dictionary<String, String>;

        public static partial class Web
        {
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, ElasticBeanstalkConfigurationSource elasticBeanstalkConfigurationSource)
                => UseConfiguration(webHostBuilder, (context, builder) => builder.AddElasticBeanstalk(elasticBeanstalkConfigurationSource));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, ElasticBeanstalkConfigurationSource elasticBeanstalkConfigurationSource)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(elasticBeanstalkConfigurationSource));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, ElasticBeanstalkConfigurationSource elasticBeanstalkConfigurationSource)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(elasticBeanstalkConfigurationSource));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, ElasticBeanstalkConfigurationSource elasticBeanstalkConfigurationSource)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(elasticBeanstalkConfigurationSource));

            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Dictionary initialData)
                => UseConfiguration(webHostBuilder, (context, builder) => builder.AddElasticBeanstalk(initialData));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, Dictionary initialData)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(initialData));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Dictionary initialData)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(initialData));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Dictionary initialData)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(initialData));

            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Boolean optional = true, Boolean reloadOnChange = false)
                => UseConfiguration(webHostBuilder, (context, builder) => builder.AddElasticBeanstalk(optional, reloadOnChange));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Boolean predicate, Boolean optional = true, Boolean reloadOnChange = false)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(optional, reloadOnChange));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, Boolean optional = true, Boolean reloadOnChange = false)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(optional, reloadOnChange));
            public static IWebHostBuilder UseElasticBeanstalkConfiguration(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, Boolean optional = true, Boolean reloadOnChange = false)
                => UseConfiguration(webHostBuilder, predicate, (context, builder) => builder.AddElasticBeanstalk(optional, reloadOnChange));
        }
    }
}
