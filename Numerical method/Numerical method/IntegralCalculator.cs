using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_method
{
   
    public delegate double Integral(double x);
    public delegate double DoubleIntegral(double x, double y);
    class IntegralCalculator
    {

        public static double RectangleMethod(double a, double b, double eps, Integral function)
        {
            /*
             * 
             *  a: левый конец интервала [a,b].
                b: правый конец интервала [a,b].
                eps: максимальная погрешность, допустимая в численном приближении.
            */
            int n = 1; // количество интервалов
            double result1, result2 = 0.0; // результаты интегрирования на предыдущей и текущей итерациях

            do
            {
                result1 = result2; // сохраняем результат предыдущей итерации
                result2 = 0.0; // обнуляем результат текущей итерации

                double h = (b - a) / n; // вычисляем длину интервала

                // вычисляем значение функции в средней точке каждого интервала и суммируем
                for (int i = 0; i < n; i++)
                {
                    double x = a + i * h;
                    result2 += function(x + h / 2);
                }

                result2 *= h; // умножаем сумму на длину интервала

                n *= 2; // удваиваем количество интервалов для следующей итерации
            } while (Math.Abs(result2 - result1) > eps); // продолжаем, пока не достигнем заданной точности

            return result2; // возвращаем результат интегрирования
        }

        public static double TrapezoidalMethod(double a, double b, double eps, Integral function)
        {
            int n = 1; // количество интервалов
            double result1, result2 = 0.0; // результаты интегрирования на предыдущей и текущей итерациях

            do
            {
                result1 = result2; // сохраняем результат предыдущей итерации
                result2 = 0.5 * (function(a) + function(b)); // вычисляем значение на концах интервала

                double h = (b - a) / n; // вычисляем длину интервала

                // вычисляем значение функции на каждом интервале и суммируем
                for (int i = 1; i < n; i++)
                {
                    double x = a + i * h;
                    result2 += function(x);
                }

                result2 *= h; // умножаем сумму на длину интервала

                n *= 2; // удваиваем количество интервалов для следующей итерации
            } while (Math.Abs(result2 - result1) > eps); // продолжаем, пока не достигнем заданной точности

            return result2; // возвращаем результат интегрирования
        }
        // Функция для вычисления интеграла методом Симпсона
        public static double SimpsonMethod(double a, double b, double eps, Integral function)
        {
            int n = 1; // Начальное количество отрезков разбиения
            double h = (b - a) / n; // Шаг разбиения
            double sum1 = function(a) + function(b); // Сумма функций на концах интервала
            double sum2 = 0.0; // Сумма функций внутри интервала

            double integralApproximation = h * (sum1 + 2 * sum2) / 2; // Приближенное значение интеграла
            double integralError = double.MaxValue; // Ошибка приближения интеграла

            while (integralError > eps)
            {
                double sum2Temp = 0.0;

                for (int i = 1; i <= n; i++)
                {
                    double x = a + i * h;
                    sum2Temp += function(x - h / 2);
                }

                sum2 = sum2Temp;
                n *= 2;
                h /= 2;

                double integralApproximationPrev = integralApproximation;
                integralApproximation = h * (sum1 + 2 * sum2) / 2;
                integralError = Math.Abs(integralApproximation - integralApproximationPrev) / 15; // Формула оценки погрешности
            }

            return integralApproximation;
        }

        /* 
            Этот метод Симпсона вычисляет двойной интеграл функции f(x,y) на прямоугольнике [ax, bx] x [ay, by] с заданной точностью eps.
        */
        public static double SimpsonMethod(double ax, double bx, double ay, double by, double eps, DoubleIntegral functionDouble)
        {
            // Инициализируем значения счетчиков шагов для x и y
            int nx = 2;
            int ny = 2;

            // Инициализируем результаты предыдущего и текущего вычислений
            double prevResult = 0.0;
            double result = 0.0;

            // Запускаем цикл для вычисления методом Симпсона по сетке
            while (true)
            {
                // Вычисляем шаги для x и y
                double hx = (bx - ax) / nx;
                double hy = (by - ay) / ny;

                // Инициализируем сумму, которую будем считать
                double sum = 0.0;

                // Запускаем вложенный цикл для суммирования значений функции по всей сетке
                for (int i = 0; i <= nx; i++)
                {
                    for (int j = 0; j <= ny; j++)
                    {
                        // Вычисляем координаты x и y для текущей ячейки сетки
                        double x = ax + i * hx;
                        double y = ay + j * hy;

                        // Вычисляем веса для текущей ячейки сетки
                        double w = (i == 0 || i == nx) ? 1 : (i % 2 == 0) ? 2 : 4;
                        double h = (j == 0 || j == ny) ? 1 : (j % 2 == 0) ? 2 : 4;

                        // Добавляем значение функции, умноженное на вес, к общей сумме
                        sum += w * h * functionDouble(x, y);
                    }
                }

                // Вычисляем результат для текущей сетки
                result = sum * hx * hy / 9;

                // Если разница между текущим и предыдущим результатами меньше, чем заданная точность, выходим из цикла
                if (Math.Abs(result - prevResult) < eps)
                {
                    break;
                }

                // Обновляем значение предыдущего результата
                prevResult = result;

                // Удваиваем количество шагов для x и y
                nx *= 2;
                ny *= 2;
            }

            // Возвращаем результат
            return result;
        }
    }
}
