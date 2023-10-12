using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_method
{
    class DifferentialEquations
    {
        public delegate Vector Der(double t, Vector x);
        public static Vector PravDU(double t, Vector x)
        {
            Vector f = new Vector(2);
            f[0] = x[1];
            f[1] = -x[0];
            return f;
        }
        // Метод Эйлера
        public static Matrix EulerMethod(double t0, double tEnd, Vector x0, int steps, Der f)
        {
            int n = x0.Size;
            Matrix result = new Matrix(n + 1, steps + 1);
            double dt = (tEnd - t0) / steps;
            Vector column = new Vector(n + 1);
            column[0] = t0;

            for (int i = 0; i < n; i++)
                column[i + 1] = x0[i];

            result.SetColumn(0, column);
            Vector xt = x0.Copy();
            double t = t0;
            Vector derivative;

            for (int k = 1; k <= steps; k++)
            {
                derivative = f(t, xt);
                xt = xt + derivative * dt;
                t += dt;
                column[0] = t;

                for (int i = 0; i < n; i++)
                    column[i + 1] = xt[i];

                result.SetColumn(k, column);
            }

            return result;
        }

        // Метод Рунге-Кутты второго порядка (RK2)
        public static Matrix RungeKutta2(double t0, double tEnd, Vector x0, int steps, Der f)
        {
            int n = x0.Size;
            Matrix result = new Matrix(n + 1, steps + 1);
            double dt = (tEnd - t0) / steps;
            Vector column = new Vector(n + 1);
            column[0] = t0;

            for (int i = 0; i < n; i++)
                column[i + 1] = x0[i];

            result.SetColumn(0, column);
            Vector xt = x0.Copy();
            double t = t0;

            for (int k = 1; k <= steps; k++)
            {
                Vector k1 = f(t, xt);
                Vector k2 = f(t + dt, xt + k1 * dt);
                xt = xt + (k1 + k2) * (dt / 2.0);
                t += dt;
                column[0] = t;

                for (int i = 0; i < n; i++)
                    column[i + 1] = xt[i];

                result.SetColumn(k, column);
            }

            return result;
        }

        // Метод Рунге-Кутты четвертого порядка (RK4)
        public static Matrix RungeKutta4(double t0, double tEnd, Vector x0, int steps, Der f)
        {
            int n = x0.Size;
            Matrix result = new Matrix(n + 1, steps + 1);
            double dt = (tEnd - t0) / steps;
            Vector column = new Vector(n + 1);
            column[0] = t0;

            for (int i = 0; i < n; i++)
                column[i + 1] = x0[i];

            result.SetColumn(0, column);
            Vector xt = x0.Copy();
            double t = t0;

            for (int k = 1; k <= steps; k++)
            {
                Vector k1 = f(t, xt);
                Vector k2 = f(t + dt / 2.0, xt + k1 * (dt / 2.0));
                Vector k3 = f(t + dt / 2.0, xt + k2 * (dt / 2.0));
                Vector k4 = f(t + dt, xt + k3 * dt);
                xt = xt + (k1 + 2 * k2 + 2 * k3 + k4) * (dt / 6.0);
                t += dt;
                column[0] = t;

                for (int i = 0; i < n; i++)
                    column[i + 1] = xt[i];

                result.SetColumn(k, column);
            }

            return result;
        }

        public static Matrix AdamsMethod(double t0, double tEnd, Vector x0, int steps, Der f)
        {
            int n = x0.Size;
            Matrix result = new Matrix(n + 1, steps + 1); // Создаем матрицу для хранения результатов
            double dt = (tEnd - t0) / steps; // Вычисляем размер шага по времени
            Vector column = new Vector(n + 1); // Создаем вектор для хранения значений для каждого временного шага
            column[0] = t0; // Устанавливаем начальное значение времени

            for (int i = 0; i < n; i++)
                column[i + 1] = x0[i]; // Устанавливаем начальные значения переменных

            result.SetColumn(0, column); // Устанавливаем начальный столбец матрицы результатов
            Vector[] xt = new Vector[steps + 1]; // Создаем массив для хранения значений x на каждом временном шаге
            double[] t = new double[steps + 1]; // Создаем массив для хранения значений времени
            Vector[] derivatives = new Vector[steps + 1]; // Создаем массив для хранения производных


            t[0] = t0;
            xt[0] = x0.Copy();
            derivatives[0] = f(t[0], xt[0]);

            // Метод Эйлера для получения начальных значений
            for (int k = 1; k <= Math.Min(3, steps); k++)
            {
                t[k] = t[k - 1] + dt;
                derivatives[k] = f(t[k], xt[k - 1]);
                xt[k] = xt[k - 1] + derivatives[k] * dt;
                column[0] = t[k];

                for (int i = 0; i < n; i++)
                    column[i + 1] = xt[k][i];

                result.SetColumn(k, column);
            }

            // Метод Адамса для последующих шагов
            for (int k = 3; k < steps; k++)
            {
                t[k + 1] = t[k] + dt;
                xt[k + 1] = xt[k] + (dt / 24) * (55 * derivatives[k] - 59 * derivatives[k - 1] + 37 * derivatives[k - 2] - 9 * derivatives[k - 3]);
                derivatives[k + 1] = f(t[k + 1], xt[k + 1]);
                column[0] = t[k + 1];

                for (int i = 0; i < n; i++)
                    column[i + 1] = xt[k + 1][i];

                result.SetColumn(k + 1, column);
            }

            return result;
        }
    }
}
