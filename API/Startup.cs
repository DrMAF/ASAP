using BLL;
using Core.Entities;
using Core.Interfaces;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace ASAP
{
    public static class Startup
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;

                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));

            var appIServices = typeof(IBaseService<,>).Assembly.GetTypes().Where(s => s.Name.ToLower().EndsWith("service") && s.IsInterface).ToList();
            var appServices = typeof(BaseService<,>).Assembly.GetTypes().Where(s => s.Name.ToLower().EndsWith("service") && s.IsClass).ToList();

            foreach (var appIService in appIServices)
            {
                var implementationType = appServices.FirstOrDefault(srvc => srvc.Name.ToLower() == appIService.Name.ToLower().Substring(1));

                if (implementationType != null)
                {
                    services.Add(new ServiceDescriptor(appIService, implementationType, ServiceLifetime.Scoped));
                }
            }

            return services;
        }
    }
}
