using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record StockDataDto(string ticker, List<StockAggregate> results, string request_id);

    /*
        t => timestamp
        o => open
        c => close
        h => high
        l => low
        v => volume
    */
    public record StockAggregate(double t, decimal o, decimal c, decimal h, decimal l, double v);

    public record CreatePolygonDto(string request_id, string results);
    public record GetPolygonApiDataDto(string request_id, string[] results);
}
