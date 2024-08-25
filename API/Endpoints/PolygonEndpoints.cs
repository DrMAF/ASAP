using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

            var res = notificationService.SendEmailsToUsers(news);

            return Results.Ok(res);
        }
    }
}
