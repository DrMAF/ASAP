using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendEmailsToUsers(List<PolygonNews> newsList);
    }
}