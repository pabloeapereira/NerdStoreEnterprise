﻿using NSE.Bff.Compras.Configuration;
using NSE.WebAPI.Core.Identidade;
using IStartup = NSE.Bff.Compras.Extensions.IStartup;

namespace NSE.Bff.Compras
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
            services.AddApiConfiguration(Configuration);
            services.AddJwtConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            //services.AddMediatR(typeof(Startup));
            services.RegisterServices();
            services.AddMessageBusConfiguration(Configuration);
        }
    }
}