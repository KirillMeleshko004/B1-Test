using ExcelServer.UseCases.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelServer.UseCases
{
    public static class UseCasesServiceCollectionExtension
    {
        public static IServiceCollection ConfigureApplicationServices(
            this IServiceCollection services)
        {
            //pass assembly with mapping profiles
            services.AddAutoMapper(typeof(AccountProfile).Assembly);

            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(typeof(AccountProfile).Assembly));

            return services;
        }
    }
}