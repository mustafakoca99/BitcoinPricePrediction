using System.Net.Http;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBitcoinService
    {
        bool CheckAlgorithm(string algorithmName);
        Task<double> GetBitcoinWeeklyPrices(IHttpClientFactory httpClientFactory, string baseUrl);
    }
}
