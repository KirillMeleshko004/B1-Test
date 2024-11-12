using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelServer.Controllers
{
    public static class PresentationServiceExtension
    {
        public static IServiceCollection ConfigurePresentation(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ViewModels.TurnoverDocumentUploadViewModel).Assembly);
            services.ConfigureControllers();

            return services;
        }

        private static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                //return http 406 Not Acceptable when Accept header contains unsupported format
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;
            })
            .AddApplicationPart(typeof(TurnoverDocumentsController).Assembly);

            //Supressing default 400 bad request response on invalid model
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}