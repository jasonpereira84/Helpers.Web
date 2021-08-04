using System;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public sealed class EmptyStartup
    {
        public EmptyStartup() { }

        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app, IHostEnvironment env) { }
    }
}
