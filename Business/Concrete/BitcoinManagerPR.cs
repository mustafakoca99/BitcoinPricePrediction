using Business.Abstract;
using Core.PricePredictor;
using Core.Utilities;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BitcoinManagerPR : IBitcoinService
    {
        public bool CheckAlgorithm(string algorithmName)
        {
            return algorithmName == "PR";
        }

        public async Task<double> GetBitcoinWeeklyPrices(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            var weeklyPrices = await BitcoinWeeklyPrices.Execute(httpClientFactory, baseUrl);

            return BitcoinPRPricePredictor.Predict(weeklyPrices.ToArray());
        }
    }
}
