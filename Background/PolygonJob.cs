using Application.Services.EmailQueue;
using Application.Services.Polygon;
using Background.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background
{
    public class PolygonJob
    {
        private readonly IPolygonJobService _polygonJobService;
        private readonly IEmailQueueService _emailQueueService;
        private readonly IPolygonService _polygonService;

        public PolygonJob(IPolygonJobService polygonJobService, IEmailQueueService emailQueueService, IPolygonService polygonService)
        {
            _polygonJobService = polygonJobService;
            _emailQueueService = emailQueueService;
            _polygonService = polygonService;
        }

        public async Task FetchStockDataAsync(string ticker)
        {
            var stockData = await _polygonJobService.GetStockDataAsync(ticker);

            if (stockData == null) return;

            await _polygonService.AddAsync(stockData);

            var emails = await _emailQueueService.GetEmailsAsync();


            //await _emailQueueService.AddBluckAsync(new EmailQueue
            //{
            //    Id = Guid.NewGuid(),
            //    Attempts = 0,
            //    UserId = 0
            //});
        }
    }
}
