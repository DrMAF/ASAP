using Core.Interfaces.Services;

namespace API.HostedServices
{
    public class PolygonNewsUpdateService : BackgroundService
    {
        readonly ILogger<PolygonNewsUpdateService> _logger;
        IServiceScopeFactory _serviceScopeFactory;

        readonly TimeSpan _interval = TimeSpan.FromSeconds(40);

        public PolygonNewsUpdateService(ILogger<PolygonNewsUpdateService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

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
                        //var polygonNewsService = scope.ServiceProvider.GetRequiredService<IPolygonNewsService>();

                        //var newsUpdate = await polygonNewsService.SyncPolygonNews();

                        //var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                        //await notificationService.SendEmailsToUsers(newsUpdate);
                    }
                }
                catch (Exception ex)
                {
                
                }
            }
        }
    }
}
