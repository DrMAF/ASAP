using Messaging.Interfaces;
using Messaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddMessagingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddOptions();

            return services;
        }
    }
}
