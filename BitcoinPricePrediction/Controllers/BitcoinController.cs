using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitcoinPricePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitcoinController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<IBitcoinService> _bitcoinServices;
        private readonly string baseUrl = "";

        public BitcoinController(IHttpClientFactory clientFactory, IConfiguration configuration, IEnumerable<IBitcoinService> bitcoinServices)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _bitcoinServices = bitcoinServices;
            baseUrl = _configuration["Bitcoin:url"];
        }

        [HttpGet("getBitcoinWeeklyPrices")]
        public async Task<IActionResult> GetBitcoinWeeklyPricesLR([FromQuery] string algorithmName)
        {
            foreach (var service in _bitcoinServices)
            {
                if (service.CheckAlgorithm(algorithmName))
                {
                    return Ok(await service.GetBitcoinWeeklyPrices(_clientFactory, baseUrl));
                }
            }

            return NotFound();
        }
    }
}
