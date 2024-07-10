using System;

namespace Core.PolynomialRegression
{
    public class MasterPolynomialRegression
    {
        private readonly double[] coefficients;

        private MasterPolynomialRegression(double[] coefficients)
        {
            this.coefficients = coefficients;
        }

        public static MasterPolynomialRegression Train(double[] x, double[] y, int degree)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("The number of elements in X and Y must be equal.");
            }

            int n = x.Length;
            int numberOfCoefficients = degree + 1;

            // X matrisini oluştur
            double[,] X = new double[n, numberOfCoefficients];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    X[i, j] = Math.Pow(x[i], j);
                }
            }

            // Y vektörünü oluştur
            double[,] Y = new double[n, 1];
            for (int i = 0; i < n; i++)
            {
                Y[i, 0] = y[i];
            }

            // Katsayıları hesapla (en küçük kareler yöntemi kullanarak)
            var XTranspose = MatrixTranspose(X);
            var XTX = MatrixMultiply(XTranspose, X);
            var inverseXTX = MatrixInverse(XTX);
            var XTY = MatrixMultiply(XTranspose, Y);
            var coefficientsMatrix = MatrixMultiply(inverseXTX, XTY);

            // Katsayıları düz bir diziye dönüştür
            double[] coefficients = new double[numberOfCoefficients];
            for (int i = 0; i < numberOfCoefficients; i++)
            {
                coefficients[i] = coefficientsMatrix[i, 0];
            }

            return new MasterPolynomialRegression(coefficients);
        }

        public double Predict(double x)
        {
            double y = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                y += coefficients[i] * Math.Pow(x, i);
            }
            return y;
        }

        // Yardımcı matris işlemleri
        private static double[,] MatrixTranspose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double[,] result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        private static double[,] MatrixMultiply(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int cols2 = matrix2.GetLength(1);

            double[,] result = new double[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        private static double[,] MatrixInverse(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] result = new double[n, n];

            double determinant = MatrixDeterminant(matrix);
            if (determinant == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double[,] adjoint = MatrixAdjoint(matrix);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = adjoint[i, j] / determinant;
                }
            }

            return result;
        }

        private static double MatrixDeterminant(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Matrix must be square.");
            }

            int n = matrix.GetLength(0);

            if (n == 1)
            {
                return matrix[0, 0];
            }

            double determinant = 0;

            for (int j = 0; j < n; j++)
            {
                determinant += matrix[0, j] * MatrixCofactor(matrix, 0, j);
            }

            return determinant;
        }

        private static double MatrixCofactor(double[,] matrix, int row, int col)
        {
            return Math.Pow(-1, row + col) * MatrixDeterminant(MatrixMinor(matrix, row, col));
        }


        private static double[,] MatrixMinor(double[,] matrix, int row, int col)
        {
            int n = matrix.GetLength(0);
            double[,] minor = new double[n - 1, n - 1];

            for (int i = 0, k = 0; i < n; i++)
            {
                if (i == row)
                {
                    continue;
                }
                for (int j = 0, l = 0; j < n; j++)
                {
                    if (j == col)
                    {
                        continue;
                    }
                    minor[k, l] = matrix[i, j];
                    l++;
                }
                k++;
            }

            return minor;
        }

        private static double[,] MatrixAdjoint(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] adjoint = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    adjoint[i, j] = MatrixCofactor(matrix, j, i); // Transpose
                }
            }

            return adjoint;
        }
    }
}
