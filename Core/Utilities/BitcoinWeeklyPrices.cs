using Data.Dtos.Bitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public static class BitcoinWeeklyPrices
    {
        public async static Task<IEnumerable<decimal>> Execute(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            var weeklyPrices = new List<decimal>();
            var client = httpClientFactory.CreateClient();

            for (int i = 0; i < 7; i++)
            {
                // Önceki günün saat 23:59'unda Bitcoin fiyatını al
                var date = DateTime.Today.AddDays(-i).AddHours(23).AddMinutes(59);
                var path = "/v2/histohour";
                var queryParams = $"?fsym=BTC&tsym=USD&toTs={(int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds}";
                var url = string.Concat(baseUrl, path, queryParams);
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<BitcoinApiResponse>(responseStream);

                if (result.Data != null && result.Data.Data != null && result.Data.Data.Any())
                {
                    var price = result.Data.Data[0].close;
                    weeklyPrices.Add(price);
                }
                else
                {
                    throw new Exception("gelen bilgiler arasında 'data' propertysi BOŞ!");
                }
            }

            return weeklyPrices;
        }
    }
}
