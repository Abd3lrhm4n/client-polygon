using Application.Services.Client;
using Application.Services.EmailQueue;
using Application.Services.Polygon;
using Background.Services;
using Domain.Entities;
using Domain.IRepositories;
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
        private readonly IClientService _clientService;
        private readonly SendEmailService _sendEmailService;

        public PolygonJob(IPolygonJobService polygonJobService,
            IEmailQueueService emailQueueService,
            IPolygonService polygonService,
            IClientService clientService,
            SendEmailService sendEmailService)
        {
            _polygonJobService = polygonJobService;
            _emailQueueService = emailQueueService;
            _polygonService = polygonService;
            _clientService = clientService;
            _sendEmailService = sendEmailService;
        }

        public async Task FetchStockDataAsync(string ticker)
        {
            var stockData = await _polygonJobService.GetStockDataAsync(ticker);

            if (stockData == null) return;


            var users = await _clientService.GetAllClientsAsync();

            string path = Path.Combine(AppContext.BaseDirectory, "assests\\EmailTemplate.html");
            string htmlBody = await File.ReadAllTextAsync(path);

            StringBuilder tableBody = new StringBuilder();
            var ss = stockData.results.ToList();

            foreach (var result in ss)
            {
                tableBody.Append(@$"<tr>
                                        <td><strong>Ticker Symbol (T):</strong></td>
                                        <td>{stockData.ticker}</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Volume (v):</strong></td>
                                        <td>{result.v}</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Opening Price (o):</strong></td>
                                        <td>{result.o}</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Closing Price (c):</strong></td>
                                        <td>{result.c}</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Highest Price (h):</strong></td>
                                        <td>{result.h}</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Lowest Price (l):</strong></td>
                                        <td>{result.l}</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Timestamp (t):</strong></td>
                                        <td>{DateTimeOffset.FromUnixTimeMilliseconds(result.t).UtcDateTime.ToString("dd/MM/yyyy HH:mm:ss")}</td>
                                    </tr>"
                );
            }
            string htmlfinalBody = htmlBody.Replace("{{TableBody}}", tableBody.ToString());

            await _sendEmailService.SendEmailsWithBccAsync(users.Select(x => x.Email), $"News for Ticker: {ticker}", htmlfinalBody);

        }
    }
}
