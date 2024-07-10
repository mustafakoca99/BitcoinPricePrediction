namespace Data.Dtos.Bitcoin
{
    public class HistoricalData
    {
        public long time { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal open { get; set; }
        public decimal volumefrom { get; set; }
        public decimal volumeto { get; set; }
        public decimal close { get; set; }
        public string conversionType { get; set; }
        public string conversionSymbol { get; set; }
    }

}
