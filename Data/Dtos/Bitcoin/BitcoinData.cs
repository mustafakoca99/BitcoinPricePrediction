using System.Collections.Generic;

namespace Data.Dtos.Bitcoin
{
    public class BitcoinData
    {
        public bool Aggregated { get; set; }
        public long TimeFrom { get; set; }
        public long TimeTo { get; set; }
        public List<HistoricalData> Data { get; set; }
    }
}
