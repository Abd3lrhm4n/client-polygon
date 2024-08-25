using Background.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background
{
    public class PolygonJob
    {
        private readonly IPolygonService _polygonService;

        public PolygonJob(IPolygonService polygonService)
        {
            _polygonService = polygonService;
        }

        public async Task FetchStockDataAsync(string ticker)
        {
            var stockData = await _polygonService.GetStockDataAsync(ticker);
            // Process or save the stock data
        }
    }
}
