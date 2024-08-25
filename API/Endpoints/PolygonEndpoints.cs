using Core.Interfaces.Services;

namespace API.Endpoints
{
    public static class PolygonEndpoints
    {
        public static IEndpointRouteBuilder MapPolygon(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapPost("sendEmails", SendEmails);

            return endpoint;
        }

        private static async Task<IResult> SendEmails(INotificationService notificationService, IPolygonNewsService polygonNewsService)
        {
            var news = polygonNewsService.GetAll().ToList();

            await notificationService.SendEmailsToUsersAsync(news);

            return Results.Ok();
        }
    }
}
