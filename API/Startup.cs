using BLL;
using Core.Entities;
using Core.Interfaces;
using Core.Settings;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ASAP
{
    public static class Startup
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppDbContext>();
            
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;

                options.User.RequireUniqueEmail = true;
            });

            string secrt = configuration.GetSection("AuthSettings:TokenSecret").Value.ToString();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrt)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
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
