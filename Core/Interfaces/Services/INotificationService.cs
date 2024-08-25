using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendEmailsToUsersAsync(List<PolygonNews> newsList);
    }
}