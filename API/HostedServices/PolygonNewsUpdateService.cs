using Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using StockMarket;

namespace API.HostedServices
{
    public class PolygonNewsUpdateService : BackgroundService
    {
        readonly ILogger<PolygonNewsUpdateService> _logger;
        readonly PolygonSettings _polygonSettings;

        IServiceScopeFactory _serviceScopeFactory;

        readonly TimeSpan _interval = TimeSpan.FromMinutes(2);

        public PolygonNewsUpdateService(ILogger<PolygonNewsUpdateService> logger, 
        IServiceScopeFactory serviceScopeFactory, IOptions<PolygonSettings> polygonSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _polygonSettings = polygonSettings.Value;

            int minutes = 2;

            int.TryParse(_polygonSettings.IntervalInMinutes, out minutes);

            _interval = TimeSpan.FromMinutes(minutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_interval);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    _logger.LogInformation("service started");

                    using IServiceScope scope = _serviceScopeFactory.CreateScope();
                    {
                        var polygonNewsService = scope.ServiceProvider.GetRequiredService<IPolygonNewsService>();

                        var newsUpdate = await polygonNewsService.SyncPolygonNews();

                        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                        await notificationService.SendEmailsToUsersAsync(newsUpdate);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Error PolygonNewsUpdateService: {ex}");
                }
            }
        }
    }
}
