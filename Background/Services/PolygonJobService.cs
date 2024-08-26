using Application.Dtos;
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

        public PolygonJobService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Polygon:ApiKey"]!;
        }

        public async Task<CreatePolygonDto?> GetStockDataAsync(string ticker)
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

            return new CreatePolygonDto(stockData.request_id, string.Join(",", stockData.results));
        }
    }
}
