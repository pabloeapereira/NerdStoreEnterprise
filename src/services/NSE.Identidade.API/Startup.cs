using NSE.Identidade.API.Configuration;
using IStartup = NSE.Identidade.API.Extensions.IStartup;

namespace NSE.Identidade.API
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


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration);
            services.AddApiConfiguration();
            services.AddSwaggerConfiguration();
            services.AddMessageBusConfiguration(Configuration);
        }

        public void Configure(WebApplication app)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration();
        }
    }
}