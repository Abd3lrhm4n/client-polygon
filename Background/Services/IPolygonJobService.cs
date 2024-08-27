using Application.Dtos;

namespace Background.Services
{
    public interface IPolygonJobService
    {
        Task<StockDataDto?> GetStockDataAsync(string ticker);
    }
}
