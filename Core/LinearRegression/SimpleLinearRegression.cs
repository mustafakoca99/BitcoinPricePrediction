using System;
using System.Linq;

namespace Core.LinearRegression
{
    public class SimpleLinearRegression
    {
        private readonly double slope;
        private readonly double intercept;

        public SimpleLinearRegression(double slope, double intercept)
        {
            this.slope = slope;
            this.intercept = intercept;
        }

        public static SimpleLinearRegression Train(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("The number of elements in X and Y must be equal.");
            }

            int n = x.Length;

            double sumX = x.Sum();
            double sumY = y.Sum();

            double sumXSquared = x.Select(xi => xi * xi).Sum();
            double sumXY = x.Zip(y, (xi, yi) => xi * yi).Sum();

            double slope = (n * sumXY - sumX * sumY) / (n * sumXSquared - sumX * sumX);
            double intercept = (sumY - slope * sumX) / n;

            return new SimpleLinearRegression(slope, intercept);
        }

        public double Predict(double x)
        {
            return slope * x + intercept;
        }
    }
}
