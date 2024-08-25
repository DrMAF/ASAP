using StockMarket.ViewModels;

namespace StockMarket.Interfaces
{
    public interface IPolygonBaseService
    {
        Task<PolygonNewsResponse> GetNews();
    }
}