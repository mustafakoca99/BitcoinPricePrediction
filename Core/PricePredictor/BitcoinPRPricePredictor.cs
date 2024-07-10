using Core.PolynomialRegression;
using System.Linq;

namespace Core.PricePredictor
{
    public class BitcoinPRPricePredictor
    {
        // Modeli optimize etmek için polinom regresyonunu kullan
        public static double Predict(decimal[] prices)
        {
            // Geçmiş verilerin tarihlerine ihtiyacımız olmadığından sadece fiyatlarla çalışacağız
            double[] x = Enumerable.Range(1, prices.Length).Select(i => (double)i).ToArray();

            // Polinom regresyon modeli oluşturma
            var model = MasterPolynomialRegression.Train(x, prices.Select(p => (double)p).ToArray(), degree: 2); // İkinci dereceden polinom

            // Bir sonraki değeri tahmin etmek için x'i (geçmiş veri noktalarının sayısını bir artırarak) artırın
            double nextX = x.Max() + 1;

            // Tahmini fiyatı al
            double predictedPrice = model.Predict(nextX);

            return predictedPrice;
        }
    }
}
