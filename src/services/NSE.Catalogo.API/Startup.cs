using NSE.Catalogo.API.Configuration;

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
        services.AddSwaggerConfiguration();
        services.RegisterServices();
    }
}