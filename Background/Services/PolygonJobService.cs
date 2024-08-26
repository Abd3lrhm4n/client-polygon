using Application.Dtos;
using Application.Services.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Background.Services
{
    public class PolygonJobService : IPolygonJobService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IPolygonService _polygonService;

        public PolygonJobService(HttpClient httpClient, IConfiguration configuration, IPolygonService polygonService)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Polygon:ApiKey"]!;
            _polygonService = polygonService;
        }

        public async Task<StockDataDto?> GetStockDataAsync(string ticker)
        {
            var requestUrl = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?apiKey={_apiKey}";
            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve stock data from Polygon.io.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var stockData = JsonSerializer.Deserialize<StockDataDto>(content);
            
            if (stockData == null) return null;

            await _polygonService.AddAsync(new CreatePolygonDto(stockData.request_id, string.Join(",", stockData.results)));

            return stockData;
        }

    }
}
