using Serilog;

namespace ExcelServer.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            services.AddSerilog(configuration =>
            {
                configuration.WriteTo.Console();
            });

            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", builder =>
                    {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            return services;
        }

    }
}