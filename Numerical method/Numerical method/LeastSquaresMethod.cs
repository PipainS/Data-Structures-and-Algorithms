using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_method
{
    class LeastSquaresMethod
    {
        private readonly Vector x;
        private readonly Vector y;
        public Vector p;
        public delegate double FuncPsi(double func);
        public FuncPsi[] func;

        public int n, m;

        public LeastSquaresMethod(Vector x, Vector y, FuncPsi[] func)
        {
            this.x = x.Copy();
            this.y = y.Copy();
            this.func = func;
            n = x.Size;
            m = func.Length;

            Solve();
        }

        private void Solve()
        {
            double[,] A = new double[m, m];
            double[] b = new double[m];
            double[] c = new double[m];
            double[] d = new double[m];

            for (int i = 0; i < n; i++)
            {
                double xi = x[i];
                double yi = y[i];

                for (int j = 0; j < m; j++)
                {
                    double psi = func[j](xi);
                    b[j] += yi * psi;

                    for (int k = j; k < m; k++)
                    {
                        double psiK = func[k](xi);
                        A[j, k] += psi * psiK;
                    }
                }
            }

            for (int j = 0; j < m; j++)
            {
                c[j] = b[j];
                for (int i = 0; i < j; i++)
                {
                    c[j] -= A[i, j] * d[i];
                }
                d[j] = c[j] / A[j, j];
            }

            p = new Vector(m);
            for (int i = 0; i < m; i++)
            {
                p[i] = d[i];
            }
        }
        private Vector GetFunc(double xValue)
        {
            Vector result = new Vector(m);
            for (int i = 0; i < m; i++)
                result[i] = func[i](xValue);
            return result;
        }

        public double GetCriteria()
        {
            Vector residuals = new Vector(n);
            for (int i = 0; i < n; i++)
                residuals[i] = y[i] - p * GetFunc(x[i]);
            return residuals.Norma1();
        }
    }
    class LeastSquares
    {
        private readonly Vector x;
        private readonly Vector y;
        public Vector p;
        public delegate double FuncPsi(double func);
        public FuncPsi[] func;

        public int n, m;

        public LeastSquares(Vector x, Vector y, FuncPsi[] func)
        {
            if (x.Size != y.Size)
                throw new Exception("Количество аргументов не совпадает с количеством значений функции");

            this.x = x.Copy();
            this.y = y.Copy();
            this.n = x.Size;
            this.func = func;
            this.m = func.Length;

            CalculateParameters();
        }

        private void CalculateParameters()
        {
            Matrix H = new Matrix(n, m);
            for (int i = 0; i < n; i++)
                H.SetRow(i, GetFunc(x[i]));

            Matrix HTransposed = H.Trans();
            Matrix HTHInverse = Matrix.Inverse(HTransposed * H);
            Matrix HTHInverseHT = HTHInverse * HTransposed;
            p = HTHInverseHT.Multiply(y);
        }

        private Vector GetFunc(double xValue)
        {
            Vector result = new Vector(m);
            for (int i = 0; i < m; i++)
                result[i] = func[i](xValue);
            return result;
        }

        public double GetCriteria()
        {
            Vector residuals = new Vector(n);
            for (int i = 0; i < n; i++)
                residuals[i] = y[i] - p * GetFunc(x[i]);
            return residuals.Norma1();
        }
    }
    
}
