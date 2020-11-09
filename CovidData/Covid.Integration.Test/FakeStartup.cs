using System;
using Covid.Api;
using Covid.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Covid.Integration.Test
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            
            using var serviceScope = serviceScopeFactory.CreateScope();
            
            var dbContext = serviceScope.ServiceProvider.GetService<IOptions<CovidDatabaseSettings>>();

            if (dbContext.Value.ConnectionString.Contains("database.windows.net"))
            {
                throw new Exception("LIVE SETTINGS IN TESTS!");
            }
        }
    }
}