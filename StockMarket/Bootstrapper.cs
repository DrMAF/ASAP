using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Serilog.HttpClient.Extensions;
using StockMarket.Interfaces;
using StockMarket.Services;

namespace StockMarket.Bootstrapper
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddPolygonProviderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPolygonBaseService, PolygonBaseService>();

            services.Configure<PolygonSettings>(configuration.GetSection("PolygonSettings"));

            var polygonConfig = configuration.GetSection("PolygonSettings").Get<PolygonSettings>();

            services.AddRefitClient<IPolygonProvider>(new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            }).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(polygonConfig.Url);
            }).LogRequestResponse();


            services.AddOptions();

            return services;
        }
    }
}
