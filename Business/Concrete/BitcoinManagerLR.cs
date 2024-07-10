using Business.Abstract;
using Core.PricePredictor;
using Core.Utilities;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BitcoinManagerLR : IBitcoinService
    {
        public bool CheckAlgorithm(string algorithmName)
        {
            return algorithmName == "LR";
        }

        public async Task<double> GetBitcoinWeeklyPrices(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            var weeklyPrices = await BitcoinWeeklyPrices.Execute(httpClientFactory, baseUrl);

            return BitcoinLRPricePredictor.Predict(weeklyPrices.ToArray());
        }
    }
}
