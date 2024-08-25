using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TelecomLayer
{
    public static class TelecomBootstrapper
    {
        public static IServiceCollection AddTelecomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            services.Configure<TelecomSettings>(configuration.GetSection("TelecomSettings"));

            services.AddOptions();

            return services;
        }
    }
}
