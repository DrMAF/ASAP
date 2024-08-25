using Microsoft.Extensions.Options;
using StockMarket.Configurations;
using StockMarket.Interfaces;
using StockMarket.ViewModels;

namespace StockMarket.Services
{
    internal class PolygonBaseService : IPolygonBaseService
    {
        string _APIKey;

        readonly PolygonSettings _polygonConfiguration;
        readonly IPolygonProvider _polygonProvider;

        public PolygonBaseService(IOptions<PolygonSettings> polygonConfiguration, IPolygonProvider polygonProvider)
        {
            _APIKey = string.Empty;

            _polygonConfiguration = polygonConfiguration.Value;
            _polygonProvider = polygonProvider;
        }

        private void Authorize()
        {
            _APIKey = _polygonConfiguration.APIKey;
        }

        public async Task<PolygonNewsResponse> GetNews()
        {
            Authorize();

            var result = await _polygonProvider.GetNews(_APIKey);

            //var result = await _polygonProvider.GetNews(_APIKey);

            //var mapedResult = JsonConvert.DeserializeObject<PolygonNewsResponse>(result);

            return result;
        }
    }
}
