
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IPolygonNewsService : IBaseService<PolygonNews, string>
    {
        Task<List<PolygonNews>> SyncPolygonNews();
    }
}
