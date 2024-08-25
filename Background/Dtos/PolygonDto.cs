using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Dtos
{
    public record StockDataDto(string Ticker, List<StockAggregate> Results);

    public record StockAggregate(long Timestamp, decimal Open, decimal Close, decimal High, decimal Low, long Volume);
}
