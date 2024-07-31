using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.APIs.Errors;
using Store.APIs.Helpers;
using Store.Core;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Repository;
using Store.Service;

namespace Store.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers().
            AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                
            });

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGoodService), typeof(GoodService));
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage).ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                });
            });
            return services;
        }
    }
}
