using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;

namespace BLL
{
    public class NotificationService : INotificationService
    {
        readonly UserManager<User> _userMangaer;
        readonly IPolygonNewsService _stockMarketService;
        readonly ILogger<NotificationService> _logger;

        public NotificationService(UserManager<User> userMangaer,
        IPolygonNewsService stockMarketService,
        ILogger<NotificationService> logger)
        {
            _userMangaer = userMangaer;
            _stockMarketService = stockMarketService;
            _logger = logger;
        }

        public async Task SendEmailsToUsers(List<PolygonNews> newsList)
        {
            try
            {
                var users = _userMangaer.Users.Select(usr => new { usr.FirstName, usr.Email }).ToList();
                
                string emailHtml =  FormatEmail(newsList);

                foreach (var user in users)
                {
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: {ex} => {ex.Message}");
            }
        }

        private static string FormatEmail(List<PolygonNews> newsList)
        {
            string itemFilePath = Path.Combine(Directory.GetCurrentDirectory(), "./HTMLTemplates/Polygon/News", "NewsItem.html");

            string newsItemTemplate = File.ReadAllText(itemFilePath);

            StringBuilder itemStringResult = new StringBuilder();

            foreach (var newsItem in newsList)
            {
                itemStringResult = itemStringResult.Append(newsItemTemplate);

                itemStringResult = itemStringResult.Replace("@NewsTitle", newsItem.Title);
                itemStringResult = itemStringResult.Replace("@NewsDescription", newsItem.Description);
                itemStringResult = itemStringResult.Replace("@NewsImageLink", newsItem.Image_url);
                itemStringResult = itemStringResult.Replace("@NewsLink", newsItem.Article_url);
                itemStringResult = itemStringResult.Replace("@NewsAuther", newsItem.Author);
                itemStringResult = itemStringResult.Replace("@NewsPublisher", newsItem.PublisherName);
                itemStringResult = itemStringResult.Replace("@NewsPublishedAt", newsItem.Published_utc.ToString("yyyy-MM-dd HH:mm"));
            }

            string newsEmailFilePath = Path.Combine(Directory.GetCurrentDirectory(), "./HTMLTemplates/Polygon/News", "NewsEmail.html");

            string newsEmailTemplat = File.ReadAllText(newsEmailFilePath);

            newsEmailTemplat = newsEmailTemplat.Replace("@NewsItemsTemplates", itemStringResult.ToString());

            return newsEmailTemplat;
        }

        private void FormatCommonEmail(List<PolygonNews> newsList)
        {

        }

        private void FormatEmailForUser(string firstName)
        {

        }
    }
}
