using NSE.WebAPI.Core.Identidade;

namespace NSE.Pagamento.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddDbContext<PedidosContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                if (Debugger.IsAttached)
                    options.EnableSensitiveDataLogging();
            });*/

            //services.AddAutoMapper(typeof(VoucherMapper).Assembly);

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Total");
            app.UseAuthConfiguration();
            app.MapControllers();

            return app;
        }
    }
}