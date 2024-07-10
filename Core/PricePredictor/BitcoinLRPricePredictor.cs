using Core.LinearRegression;
using System.Linq;

namespace Core.PricePredictor
{
    public static class BitcoinLRPricePredictor
    {
        public static double Predict(decimal[] prices)
        {
            // Geçmiş verilerin tarihlerine ihtiyacımız olmadığından sadece fiyatlarla çalışacağız
            double[] x = Enumerable.Range(1, prices.Length).Select(i => (double)i).ToArray();

            // Lineer regresyon modeli oluşturma
            var model = SimpleLinearRegression.Train(x, prices.Select(p => (double)p).ToArray());

            // Bir sonraki değeri tahmin etmek için x'i (geçmiş veri noktalarının sayısını bir artırarak) artırın
            double nextX = x.Max() + 1;

            // Tahmini fiyatı al
            double predictedPrice = model.Predict(nextX);

            return predictedPrice;
        }
    }
}
