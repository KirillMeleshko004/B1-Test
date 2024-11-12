using ExcelServer.Infrastructure.DB.Contexts;
using ExcelServer.Infrastructure.DB.Repositories;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelServer.Infrastructure
{
    public static class InfrastructureServiceExtension
    {
        public static IServiceCollection ConfigureInfrastructureServices(
           this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDB(configuration)
                .ConfigureRepositorties();

            return services;
        }

        private static IServiceCollection ConfigureRepositorties(
           this IServiceCollection services)
        {
            return services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection ConfigureDB(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ExcelWorkerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultDb"));
            });

            return services;
        }
    }
}