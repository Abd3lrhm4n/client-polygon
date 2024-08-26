using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Services
{
    public interface IPolygonJobService
    {
        Task<StockDataDto?> GetStockDataAsync(string ticker);
    }
}
