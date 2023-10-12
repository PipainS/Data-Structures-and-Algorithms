using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_method
{
    class CubicSpline
    {
        private Vector x; // Вектор узловых точек x
        private Vector y; // Вектор узловых точек y
        private Vector h; // Вектор расстояний между соседними узловыми точками
        private Vector a; // Коэффициенты a для сплайнов
        private Vector b; // Коэффициенты b для сплайнов
        private Vector c; // Коэффициенты c для сплайнов
        private Vector d; // Коэффициенты d для сплайнов

        public CubicSpline(Vector x, Vector y)
        {
            if (x.Size != y.Size)
                throw new ArgumentException("Размеры векторов x и y должны совпадать.");

            this.x = x.Copy(); // Создаем копию вектора x
            this.y = y.Copy(); // Создаем копию вектора y
            int n = x.Size;

            // Вычисляем расстояния между соседними узловыми точками
            h = new Vector(n - 1);
            for (int i = 0; i < n - 1; i++)
                h[i] = x[i + 1] - x[i];

            // Вычисляем коэффициенты c для сплайнов
            c = CalculateC();

            // Вычисляем коэффициенты a, b и d для сплайнов
            a = new Vector(n - 1);
            b = new Vector(n - 1);
            d = new Vector(n - 1);
            for (int i = 0; i < n - 1; i++)
            {
                a[i] = y[i];
                b[i] = (y[i + 1] - y[i]) / h[i] - h[i] * (c[i + 1] + 2 * c[i]) / 3;
                d[i] = (c[i + 1] - c[i]) / (3 * h[i]);
            }
        }

        private Vector CalculateC()
        {
            int n = x.Size;
            Matrix A = new Matrix(n, n);
            Vector B = new Vector(n);

            // Задаем граничные условия
            A[0, 0] = 1;
            A[n - 1, n - 1] = 1;

            // Заполняем матрицу A и вектор B
            for (int i = 1; i < n - 1; i++)
            {
                A[i, i - 1] = h[i - 1];
                A[i, i] = 2 * (h[i - 1] + h[i]);
                A[i, i + 1] = h[i];

                B[i] = 3 * ((y[i + 1] - y[i]) / h[i] - (y[i] * y[i - 1]) / h[i - 1]);

            }
            Vector c = Matrix.GaussianElimination(A, B);
            return c;
        }

        public double Interpolate(double xi)
        {
            int index = FindIndex(xi);
            double dx = xi - x[index];
            double result = a[index] + b[index] * dx + c[index] * dx * dx + d[index] * dx * dx * dx;

            return result;

        }
        private int FindIndex(double xi)
        {
            int n = x.Size;
            // Проверка граничных условий
            if (xi <= x[0])
                return 0;
            if (xi >= x[n - 1])
                return n - 2;

            // Бинарный поиск индекса
            int left = 0;
            int right = n - 1;
            while (left + 1 < right)
            {
                int mid = (left + right) / 2;
                if (xi < x[mid])
                    right = mid;
                else
                    left = mid;
            }

            return left;
        }
    }
}

