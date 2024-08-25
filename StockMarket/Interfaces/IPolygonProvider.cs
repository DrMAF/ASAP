using Refit;
using StockMarket.ViewModels;

namespace StockMarket
{
    public interface IPolygonProvider
    {

        [Get("/reference/news?apiKey={apiKey}")]
        public Task<PolygonNewsResponse> GetNews(string apiKey);
    }
}
