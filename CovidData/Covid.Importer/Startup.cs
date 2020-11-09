using AutoMapper;
using Covid.Business.Configuration;
using Covid.Business.MappingProfiles;
using Covid.Business.Services;
using Covid.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Covid.Importer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddConfiguration(configuration)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<CovidDatabaseSettings>(Configuration.GetSection(nameof(CovidDatabaseSettings)));
            services.Configure<ImportOptions>(options => Configuration.GetSection("ImportOptions").Bind(options));

            services.AddSingleton<ICovidDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CovidDatabaseSettings>>().Value)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ICovidImportService, QuestionaireImportService>()
                .AddScoped<IImportStatisticsService, ImportStatisticsService>()
                .AddHostedService<ImportService>();

            var mappingConfig = new MapperConfiguration(conf =>
            {
                conf.AddProfile(new EntityToDtoProfiles());
                conf.AddProfile(new DtoToEntityProfiles());
            });

            mappingConfig.AssertConfigurationIsValid();
            mappingConfig.CompileMappings();

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}