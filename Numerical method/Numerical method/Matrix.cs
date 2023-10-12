using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_method
{
    class Matrix
    {
        protected int rows, columns;
        protected double[,] data;
        public Matrix(int r, int c)
        {
            this.rows = r; this.columns = c;
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++) data[i, j] = 0;
        }
        public Matrix(double[,] mm)
        {
            this.rows = mm.GetLength(0); this.columns = mm.GetLength(1);
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    data[i, j] = mm[i, j];
        }
        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 && j < 0 && i >= rows && j >= columns)
                {
                    // Console.WriteLine(" Индексы вышли за пределы матрицы ");
                    return Double.NaN;
                }
                else
                    return data[i, j];
            }
            set
            {
                if (i < 0 && j < 0 && i >= rows && j >= columns)
                {
                    //Console.WriteLine(" Индексы вышли за пределы матрицы ");
                }
                else
                    data[i, j] = value;
            }
        }
        public Vector? GetRow(int r)
        {
            if (r >= 0 && r < rows)
            {
                Vector row = new Vector(columns);
                for (int j = 0; j < columns; j++) row[j] = data[r, j];
                return row;
            }
            return null;
        }
        public Vector? GetColumn(int c)
        {
            if (c >= 0 && c < columns)
            {
                Vector column = new Vector(rows);
                for (int i = 0; i < rows; i++) column[i] = data[i, c];
                return column;
            }
            return null;
        }
        public bool SetRow(int index, Vector r)
        {
            if (index < 0 || index > rows) return false;
            if (r.Size != columns) return false;
            for (int k = 0; k < columns; k++) data[index, k] = r[k];
            return true;
        }
        public bool SetColumn(int index, Vector c)
        {
            if (index < 0 || index > columns) return false;
            if (c.Size != rows) return false;
            for (int k = 0; k < rows; k++) data[k, index] = c[k];
            return true;
        }
        public void SwapRows(int r1, int r2)
        {
            if (r1 < 0 || r2 < 0 || r1 >= rows || r2 >= rows || (r1 == r2)) return;
            Vector v1 = GetRow(r1);
            Vector v2 = GetRow(r2);
            SetRow(r2, v1);
            SetRow(r1, v2);
        }
        public Matrix Copy()
        {
            Matrix r = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++) r[i, j] = data[i, j];
            return r;
        }
        public Matrix Trans()
        {
            Matrix transposeMatrix = new Matrix(columns, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposeMatrix.data[j, i] = data[i, j];
                }
            }
            return transposeMatrix;
        }
        public static void PrintMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    Console.Write("{0}\t", matrix[i, j]);
                }
                Console.WriteLine("");
            }
        }
        //Сложение матриц
        public static Matrix operator +(Matrix m1, Matrix m2)     //Сложение матриц
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new Exception("Матрицы не совпадают по размерности");
            }
            Matrix result = new Matrix(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return result;
        }
        public Vector Multiply(Vector vector)
        {
            if (columns != vector.Size) return null;
            Vector result = new Vector(rows);
            for (int i = 0; i < rows; i++)
            {
                result[i] = 0;
                for (int j = 0; j < columns; j++)
                    result[i] += data[i, j] * vector[j];

            }
            return result;
        }
        public Matrix Multiply(double value)
        {
            Matrix result = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = data[i, j] * value;
                }
            }
            return result;
        }

        //Матрица на матрицу
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.columns != m2.rows) throw new Exception();

            Matrix result = new Matrix(m1.rows, m2.columns);
            double[] current_row;
            double[] current_column;

            for (int i = 0; i < m1.rows; i++)
            {
                current_row = m1.GetRow(i).GetElements();
                for (int j = 0; j < m2.columns; j++)
                {
                    current_column = m2.GetColumn(j).GetElements();
                    for (int k = 0; k < m1.columns; k++)
                    {
                        result.data[i, j] += current_row[k] * current_column[k];
                        if (Math.Abs(result.data[i, j]) < 0.000001) result.data[i, j] = 0;
                    }
                }
            }
            return result;
        }
        //Матрица на векто
        public static Vector operator *(Matrix matrix, Vector vector) => matrix.Multiply(vector);
        public static Vector operator *(Vector vector, Matrix A) => A.Multiply(vector);
        //Умножение матриц на число
        public static Matrix operator *(Matrix A, double value) => A.Multiply(value);
        
        /// <summary>
        ///  Решение СЛАУ методом нижнетрегольной матрицы
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Vector? SolveLUDownTriangle(Matrix A, Vector B)
        {
            int matrixRows = A.rows;
            int matrixColumns = A.columns;
            int vectorSize = B.Size;

            if (matrixColumns != matrixRows || matrixRows != vectorSize) { return null; };
            ///Проверка
            for (int i = 0; i < matrixRows; i++)
            {
                if (A.data[i, i] == 0) { return null; };
                for (int j = i + 1; j < matrixRows; j++)
                {
                    if (Math.Abs(A.data[i, j]) > 0.000000001) { return null; };
                }
            }

            Vector unknownValueVector = new(A.Rows);
            unknownValueVector[0] = B[0] / A.data[0, 0];
            /// Основной цикл
            for (int i = 0; i < matrixRows; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += A.data[i, j] * unknownValueVector[j];
                }
                unknownValueVector[i] = (B[i] - sum) / A.data[i, i];
            }
            return unknownValueVector;
        }
        /// <summary>
        /// Решение СЛАУ методом верхнетреугольной матрицы
        /// </summary>
        /// <param name="A">Шарипов</param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Vector? SolveLUUpTriangle(Matrix A, Vector B)
        {

            int matrixRows = A.rows;
            int matrixColumns = A.columns;
            int vectorSize = B.Size;
            ///Проверка
            if (matrixColumns != matrixRows || matrixRows != vectorSize) { return null; };

            for (int i = 0; i < matrixRows; i++)
            {
                if (A.data[i, i] == 0) { return null; };
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(A.data[i, j]) > 0.000001) { return null; };
                }
            }

            Vector unknownValueVector = new(matrixRows);
            unknownValueVector[matrixRows - 1] = B[matrixRows - 1] / A.data[matrixRows - 1, matrixRows - 1];
            /// Основной цикл
            for (int i = matrixRows - 2; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < matrixRows; j++)
                {
                    sum += A.data[i, j] * unknownValueVector[j];
                }
                unknownValueVector[i] = (B[i] - sum) / A.data[i, i];
            }
            return unknownValueVector;
        }
        // Получение обратной матрицы методом Гаусса
        public static Matrix Inverse(Matrix A)
        {
            int rows = A.rows;
            int columns = A.columns;
            if (rows != columns) return null;
            Matrix aCopy = A.Copy();

            Matrix result = new Matrix(rows, columns);
            Matrix E = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++) E[i, i] = 1;

            double eps = 0.000001;
            int max;
            for (int j = 0; j < columns; j++)
            {
                max = j;
                for (int i = j + 1; i < rows; i++)
                    if (Math.Abs(aCopy[i, j]) > Math.Abs(aCopy[max, j])) { max = i; };

                if (max != j)
                {
                    Vector temp = aCopy.GetRow(max); aCopy.SetRow(max, aCopy.GetRow(j)); aCopy.SetRow(j, temp);
                    Vector tmp = E.GetRow(max); E.SetRow(max, E.GetRow(j)); E.SetRow(j, tmp);
                }

                if (Math.Abs(aCopy[j, j]) < eps) return null;

                for (int i = j + 1; i < rows; i++)
                {
                    double multiplier = -aCopy[j, j] / aCopy[i, j];
                    for (int k = 0; k < columns; k++)
                    {
                        aCopy[i, k] *= multiplier;
                        aCopy[i, k] += aCopy[j, k];
                        E[i, k] *= multiplier;
                        E[i, k] += E[j, k];
                    }
                }
            }
            for (int i = 0; i < columns; i++)
                result.SetColumn(i, SolveLUUpTriangle(aCopy, E.GetColumn(i)));

            return result;
        }

        /// <summary>
        /// Решение СЛАУ методом Гаусса
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Vector GaussianElimination(Matrix systemMatrix, Vector answerVector)
        {
            /// копирование данных 
            if (systemMatrix.columns != answerVector.Size) { throw new Exception("матрицу невозможно умножить на вектор"); }
            Matrix systemMatrixCopy = systemMatrix.Copy();
            Vector answerVectorCopy = answerVector.Copy();

            int unknownValue = systemMatrixCopy.columns;
            double maxElement;
            int k, index;

            const double eps = 0.00000001;

            k = 0;
            /// Приведение матрицы в верхнетреугольный вид
            while (k < unknownValue)
            {
                /// Строка с макс элементом
                maxElement = Math.Abs(systemMatrixCopy[k, k]);
                index = k;
                for (int i = k + 1; i < unknownValue; i++)
                {
                    if (Math.Abs(systemMatrixCopy[i, k]) > maxElement)
                    {
                        maxElement = Math.Abs(systemMatrixCopy[i, k]);
                        index = i;
                    }
                }
                /// Проверка на ненулевое элемент 
                if (maxElement < eps)
                {
                    throw new Exception("Матрицы невозможно перемножить");
                }
                /// Перестановка
                for (int i = 0; i < unknownValue; i++)
                {
                    /*
                     * Перестановка через временную переменную 
                        double temp = systemMatrixCopy[k, i];
                        systemMatrixCopy[k, i] = systemMatrixCopy[index, i];
                        systemMatrixCopy[index, i] = temp;
                     */
                    /// Перестановка через кортеж
                    (systemMatrixCopy[index, i], systemMatrixCopy[k, i]) = 
                        (systemMatrixCopy[k, i], systemMatrixCopy[index, i]);
                }
                /*
                 * Перестановка через временную переменную 
                    double tmp = answerVectorCopy[k];
                    answerVectorCopy[k] = answerVectorCopy[index];
                    answerVectorCopy[index] = tmp;
                 */
                /// Перестановка через кортеж
                (answerVectorCopy[index], answerVectorCopy[k]) = 
                    (answerVectorCopy[k], answerVectorCopy[index]);

                /// нормализация
                for (int i = 0; i < unknownValue; i++)
                {
                    double temp = systemMatrixCopy[i, k];

                    if (Math.Abs(temp) < eps) { continue; }; ///Пропуск нулевых значений
                    /// деление 
                    for (int j = 0; j < unknownValue; j++)
                    {
                        systemMatrixCopy[i, j] = systemMatrixCopy[i, j] / temp;
                    }

                    answerVectorCopy[i] = answerVectorCopy[i] / temp;
                    /// вычетание
                    if (i == k) { continue; };
                    for (int j = 0; j < unknownValue; j++)
                    {
                        systemMatrixCopy[i, j] = systemMatrixCopy[i, j] - systemMatrixCopy[k, j];
                    }
                    answerVectorCopy[i] = answerVectorCopy[i] - answerVectorCopy[k];
                }
                k++;
            }

            Vector unknownValueVector = SolveLUUpTriangle(systemMatrixCopy, answerVectorCopy) ?? new(unknownValue);
            return unknownValueVector;
        }

        // Метод прогонки
        public static Vector RunThroughMethod(Vector c, Vector d, Vector e, Vector b)
        {
            int n = d.Size;
            double y;
            var alpha = d.Copy();
            var beta = b.Copy();

            for (int i = 1; i < n; i++)
            {
                y = e[i] / alpha[i - 1];
                alpha[i] = alpha[i] - y * c[i - 1];
                beta[i] = beta[i] - y * beta[i-1];
            }

            Vector res = new(n);
            res[n-1] = beta[n-1] / alpha[n-1];
            for (int i = n - 2; i >= 0; i--)
            {
                res[i] = (beta[i] - c[i] * res[i + 1]) / alpha[i];
            }
            return res;
        }

        // метод Гивенса 
        public static Vector SolveUsingGivens(Matrix A, Vector b)
        {
            Matrix CopyA = A.Copy();
            Vector CopyB = b.Copy();
            int n = CopyB.Size;
            double c, s;
            
            // преобразование матрицы A к верхнетреугольному виду с помощью преобразований Гивенса
            for (int j = 0; j < n; j++)
            {
                for (int i = j + 1; i < n; i++)
                {
                    if (CopyA[i, j] != 0)
                    {
                        double r = Math.Sqrt(CopyA[j, j] * CopyA[j, j] + CopyA[i, j] * CopyA[i, j]);
                        c = CopyA[j, j] / r;
                        s = -CopyA[i, j] / r;
                        for (int k = j; k < n; k++)
                        {
                            double t = c * CopyA[j, k] - s * CopyA[i, k];
                            CopyA[i, k] = s * CopyA[j, k] + c * CopyA[i, k];
                            CopyA[j, k] = t;
                        }
                        double t2 = c * CopyB[j] - s * CopyB[i];
                        CopyB[i] = s * CopyB[j] + c * CopyB[i];
                        CopyB[j] = t2;
                    }
                }
            }

            Vector unknownValueVector = SolveLUUpTriangle(CopyA, CopyB) ?? new(CopyB.Size);
            return unknownValueVector;
        }

        private static double Norma(Matrix matrix)
        {
            double tmp = 0.0;

            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    tmp += matrix[i, j] * matrix[i, j];

                }
            }
            return Math.Sqrt(tmp);
        }
        // метод итераций
        public static Vector FixedPointIterationMatrix(Matrix A, Vector B)
        {
            // доделать условие сходимости
            Matrix copyA = A.Copy();
            Vector copyB = B.Copy();
            if (copyA.columns != copyB.Size) { throw new Exception("матрицу невозможно умножить на вектор"); }
            for (int i = 0; i < copyA.rows; i++)
            {
                if (copyA[i, i] == 0) { throw new Exception();}
            }
            Matrix alpha = new(copyA.rows, copyA.columns);
            Vector beta = new(copyB.Size);
            for (int i = 0; i < copyA.rows; i++)
            {
                for (int j = 0; j < copyA.columns; j++)
                {
                    if (i != j) { alpha[i, j] = -copyA[i, j] / copyA[i, i]; }
                    else { alpha[i, i] = 0; }
                }
            }


            //Проверка Norma
            if (Matrix.Norma(alpha) >= 1) return null;

            for (int i = 0; i < copyB.Size; i++)
            {
                beta[i] = copyB[i] / copyA[i, i];
            }
            var n = 100;
            Vector[] delta = new Vector[n];
            delta[0] = beta;
            for (int i = 1; i < n; i++)
            {
                delta[i] = alpha.Multiply( delta[i -1]);
            }
            Vector x = new(copyB.Size);
            
            for (int i = 0; i < B.Size; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    x[i] += delta[j][i];
                }
            }
            
            return x;
        }

    }
}
