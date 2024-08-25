using Background.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Background.Services
{
    public class PolygonService : IPolygonService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public PolygonService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Polygon:ApiKey"]!;
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

            return stockData;
        }
    }
}
