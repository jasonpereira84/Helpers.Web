using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;
        using Microsoft.Extensions.Configuration;

        public static partial class Web
        {
            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(
                    (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, Boolean predicate, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(
                    predicate, 
                    (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration((context, builder) => builder.AddCompilationProperties(initialData));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, Boolean predicate, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(initialData));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(initialData));

            public static IWebHostBuilder Use(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(initialData));
        }
    }
}
