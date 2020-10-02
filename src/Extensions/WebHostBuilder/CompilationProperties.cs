using System;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.AspNetCore.Hosting;
        using Microsoft.Extensions.Configuration;

        using Pair = KeyValuePair<String, String>;

        public class CompilationPropertiesConfigurationProvider : ConfigurationProvider, IEnumerable<Pair>
        {
            private readonly CompilationProperties _source;

            public CompilationPropertiesConfigurationProvider(CompilationProperties source)
            {
                _source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public IEnumerator<Pair> GetEnumerator() => Data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public override void Load()
            {
                foreach (var pair in _source)
                    Data.Add(pair.Key, pair.Value);

                base.Load();
            }
        }

        public class CompilationPropertiesConfigurationSource : IConfigurationSource
        {
            public CompilationProperties InitialData { get; private set; }

            public CompilationPropertiesConfigurationSource(CompilationProperties initialData)
            {
                InitialData = initialData ?? throw new ArgumentNullException(nameof(initialData));
            }

            public IConfigurationProvider Build(IConfigurationBuilder builder)
                => new CompilationPropertiesConfigurationProvider(InitialData);
        }

        public static partial class Web
        {
            public static IConfigurationBuilder AddCompilationProperties(this IConfigurationBuilder builder, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => builder.Add(compilationPropertiesConfigurationSource);

            public static IConfigurationBuilder AddCompilationProperties(this IConfigurationBuilder builder, CompilationProperties initialData)
                => AddCompilationProperties(builder, new CompilationPropertiesConfigurationSource(initialData));

            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration((context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));
            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, Boolean predicate, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));
            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));
            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(compilationPropertiesConfigurationSource));

            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration((context, builder) => builder.AddCompilationProperties(initialData));
            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, Boolean predicate, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(initialData));
            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, Func<WebHostBuilderContext, IConfigurationBuilder, Boolean> predicate, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(initialData));
            public static IWebHostBuilder UseCompilationProperties(this IWebHostBuilder webHostBuilder, Func<IConfigurationRoot, Boolean> predicate, CompilationProperties initialData)
                => webHostBuilder.UseConfiguration(predicate, (context, builder) => builder.AddCompilationProperties(initialData));

        }
    }
}
