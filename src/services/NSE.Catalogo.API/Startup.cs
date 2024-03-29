﻿using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Configuration;
using NSE.WebAPI.Core.Identidade;
using IStartup = NSE.Catalogo.API.Configuration.IStartup;

namespace NSE.Catalogo.API
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void Configure(WebApplication app)
        {
            app.UseSwaggerConfiguration();
            app.UseApiConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddApiConfiguration(Configuration);
            services.AddJwtConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.RegisterServices();
        }
    }
}