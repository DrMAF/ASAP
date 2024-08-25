using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using StockMarket.Interfaces;

namespace BLL
{
    public class PolygonNewsService : BaseService<PolygonNews, string>, IPolygonNewsService
    {
        readonly IPolygonBaseService _polygonService;
        readonly ILogger<PolygonNewsService> _logger;

        public PolygonNewsService(IBaseRepository<PolygonNews, string> repository, 
        IPolygonBaseService polygonBaseService,
        ILogger<PolygonNewsService> logger) : base(repository)
        {
            _polygonService = polygonBaseService;
            _logger = logger;
        }

        public async Task<List<PolygonNews>> SyncPolygonNews()
        {
            try
            {
                List<PolygonNews> newsUpdate = new List<PolygonNews>();

                var result = await _polygonService.GetNews();

                PolygonNews newsObject;

                foreach (var item in result.results)
                {
                    newsObject = null;

                    newsObject = FindById(item.id);

                    if (newsObject != null)
                    {
                        continue;
                    }

                    newsObject = new PolygonNews()
                    {
                        Id = item.id,
                        Title = item.title,
                        Name = item.title,
                        Description = item.description,
                        Author = item.author,
                        PublisherName = item.publisher.name,
                        Article_url = item.article_url,
                        CreatedAt = item.published_utc,
                        Image_url = item.image_url,
                        Tickers = string.Join(",", item.tickers)
                    };

                    Create(newsObject);

                    newsUpdate.Add(newsObject);
                }

                return newsUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error in SyncPolygonNews: {ex} => {ex.Message}");
                return new List<PolygonNews>();
            }
        }

    }
}
