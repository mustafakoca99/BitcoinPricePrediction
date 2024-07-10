namespace Data.Dtos.Bitcoin
{
    public class BitcoinApiResponse
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public bool HasWarning { get; set; }
        public int Type { get; set; }
        public RateLimit RateLimit { get; set; }
        public BitcoinData Data { get; set; }
    }
}
