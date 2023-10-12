using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Numerical_method
{
    class Programm
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №1 - Поиск корня уравнения скалярной функции");
            Console.WriteLine();

            Console.WriteLine("BisectionMethod");

            double firstRoot = RootFindingAlgorithms.BisectionMethod(1, 4, 0.00001, Math.Sin);
            Console.WriteLine("sin(x) = 0;  Answer = {0}", firstRoot);

            double secondRoot = RootFindingAlgorithms.BisectionMethod(1, 4, 0.0001, x => x - 2);
            Console.WriteLine("x - 2 = 0; Answer = {0}", secondRoot);

            Console.WriteLine();

            Console.WriteLine("NewtonMethod");

            firstRoot = RootFindingAlgorithms.NewtonsMethod(4, 0.00001, Math.Sin);
            Console.WriteLine("sin(x) = 0;  Answer = {0}", firstRoot);

            secondRoot = RootFindingAlgorithms.NewtonsMethod(4, 0.0001, x => x - 2);
            Console.WriteLine("x - 2 = 0; Answer = {0}", secondRoot);

            double thirdRoot = RootFindingAlgorithms.NewtonsMethod(5, 0.0001, x => x * x - 1);
            Console.WriteLine("x^2 - 1 = 0; Answer = {0}", thirdRoot);

            Console.WriteLine();

            Console.WriteLine("Fixed Point Iteration Method");
            firstRoot = RootFindingAlgorithms.FixedPointIteration(1.1, 0.0001, x => x - 0.5 * (x * x - 1));
            Console.WriteLine("x - 0.5 * (x * x - 1) = 0;  Answer = {0}", firstRoot);

            secondRoot = RootFindingAlgorithms.FixedPointIteration(1.1, 0.0001, x => x - 2 * (x * x - 1));
            Console.WriteLine("x - 2 * (x * x - 1); Answer = {0}", secondRoot);


            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("Лабораторная работа №2-3 - Матричные методы");
            Console.WriteLine();

            Console.WriteLine("Test Multiply method Dot - Matrix.Dot(Matrix)");

            double[,] firstArrayTestMultiply = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            double[,] secondArrayTestMultiply = { { 10, 11, 12, 9 }, { 13, 14, 15, 32 }, { 16, 17, 18, 42 } };
            
            Matrix firstTestMatrixMultiply = new(firstArrayTestMultiply);
            Matrix secondTestMatrixMultiply = new(secondArrayTestMultiply);
            Matrix resultMatrixMultiply = firstTestMatrixMultiply *secondTestMatrixMultiply;

            
            for (int i = 0; i < resultMatrixMultiply.Rows; i++)
            {
                for (int j = 0; j < resultMatrixMultiply.Columns; j++)
                {
                    Console.Write(resultMatrixMultiply[i, j] + " ");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine("Test Multiply operator *");
            Matrix result = firstTestMatrixMultiply * secondTestMatrixMultiply;
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            
            Console.WriteLine("Test Muliply Matrix on Vector");
            double[] vectorTestArray = { 10, 13, 16 };
            double[,] arrayTestMultiplyVector = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Vector testVectorMultiplyV = new(vectorTestArray);
            Matrix testMatrixMultiplyM = new(arrayTestMultiplyVector);
            var resultMul = testMatrixMultiplyM.Multiply(testVectorMultiplyV);

            for (int i = 0; i < resultMul.Size; i++)
            {
                Console.WriteLine(resultMul[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Test solveTriangleUp");
            double[,] testArraySystemMatrix = { { 1, 2, 3, 4 }, { 0, 5, 6, 7 }, { 0, 0, 8, 9 }, { 0, 0, 0, 10 } };
            double[] testArrayAnswerVector = { 4, 2, 5, 7 };

            Matrix testSolveTriangleUp = new(testArraySystemMatrix);
            Vector testSolveTriangleUpV= new(testArrayAnswerVector);

            var resultSolveTriangle = Matrix.SolveLUUpTriangle(testSolveTriangleUp, testSolveTriangleUpV);
            for (int i = 0; i < resultSolveTriangle.Size; i++)
            {
                Console.WriteLine(resultSolveTriangle[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Test solveTriangleDown");
            double[,] testArraySystemMatrixDown = { { 1, 0, 0 }, { 2, 3, 0 }, { 6, 5, 4 } };
            double[] testArrayAnswerVectorDown = { 10, 11, 12 };

            Matrix testSolveTriangleDown = new(testArraySystemMatrixDown);
            Vector testSolveTriangleDownV= new(testArrayAnswerVectorDown);

            var resultSolveTriangleDown =
                Matrix.SolveLUDownTriangle(testSolveTriangleDown, testSolveTriangleDownV);
            for (int i = 0; i < resultSolveTriangleDown.Size; i++)
            {
                Console.WriteLine(resultSolveTriangleDown[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Test Gauss method");
            double[,] arrayTestGaussMethod = { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 8, 9 } };
            double[] vectorTestGaussMethod = { 14, 11, 12 };

            Matrix testGaussMethod = new(arrayTestGaussMethod);
            Vector testGaussMethodV = new(vectorTestGaussMethod);

            var resultGauss = Matrix.GaussianElimination(testGaussMethod, testGaussMethodV);
            for (int i = 0; i < resultGauss.Size; i++)
            {
                Console.WriteLine(resultGauss[i]);
            }
            var res = testGaussMethod.Multiply(resultGauss);
            Console.WriteLine(res); 

            double[,] arrayTestDot = { { 1, 2, 3 }, { 4, 5, 6 }, { 8, 8, 9 } };
            Matrix matrixTestDot = new(arrayTestDot);
            var resDot = matrixTestDot * 3;
            for (int i = 0; i < resDot.Rows; i++)
            {
                for (int j = 0; j < resDot.Columns; j++)
                {
                    Console.Write(resDot[i, j] + " ");
                }
                Console.WriteLine();
            }



            double[,] a = { { 4, 2, 0, 0 }, { 2, 4, 1, 0 }, { 0, 1, 5, -1 }, { 0, 0, -1, 5 } };
            double[] b = { 6, 7, 8, 9 };

            Matrix matrix= new(a);
            Vector d = new(b);

            var example = Matrix.GaussianElimination(matrix, d);
            Console.WriteLine("Gauss Method");
            Console.WriteLine(example);

            double[] firstVector = { 2, 1, -1, 0 };
            double[] secondVector = { 4, 4, 5, 5 };
            double[] thirdVector = {0, 2, 1, -1};
            double[] answerVector = {6, 7, 8, 9};

            Vector firstVectorV = new(firstVector);
            Vector secondVectorV = new(secondVector);
            Vector thirdVectorV = new(thirdVector);
            Vector answerVectorV = new(answerVector);

            Console.WriteLine("Progon Method");
            var resProgon = Matrix.RunThroughMethod(firstVectorV, secondVectorV, thirdVectorV, answerVectorV);
            Console.WriteLine(resProgon);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Метод Гивенса (Вращения)");

            double[,] a_ = { { 4, 2, 0, 0 }, { 2, 4, 1, 0 }, { 0, 1, 5, -1 }, { 0, 0, -1, 5 } };
            double[] b_ = { 6, 7, 8, 9 };

            Matrix matrix_ = new(a);
            Vector d_ = new(b);

            var givenMatrix = Matrix.SolveUsingGivens(matrix_, d_);
            Console.WriteLine(givenMatrix);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Метод последовательного приближения");

            double[,] test = { { 2, -1, 1 }, { 3, 5, -2 }, { 1, -4, 10 } };
            double[] test2 = { -3, 1, 0 };

            Matrix testM = new(test);
            Vector testV = new(test2);
            var iterationMatrix = Matrix.FixedPointIterationMatrix(matrix_, d_);
            Console.WriteLine(iterationMatrix);
           
            /// метод вращения гивенса
            /// 

            Console.WriteLine("Сплайн интерполяции");
            Vector x = new Vector(new double[] { 0, 1, 2, 3, 4 });
            Vector y = new Vector(new double[] { 0, 1, 4, 9, 16 });

            CubicSpline spline = new CubicSpline(x, y);

            double interpolatedValue = spline.Interpolate(2.5);

            Console.WriteLine(interpolatedValue);

            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Метод наименьших квадратов");
            /*

            Vector x_ = new Vector(new double[] { 1, 2, 3, 4, 5 });
            Vector y_ = new Vector(new double[] { 4, 7, 12, 19, 28 });

            // Создаем массив функций
            LeastSquares.FuncPsi[] func = new LeastSquares.FuncPsi[] { x => 2 * x + 1, x => 3 * x * x + 2 * x + 1 };
            LeastSquares ls = new LeastSquares(x_, y_, func);
            // Получаем коэффициенты p
            Vector p = ls.p;
            Console.WriteLine("Коэффициенты p: " + p);

            double criteria = ls.GetCriteria();
            Console.WriteLine("Критерий: " + criteria);
            Console.WriteLine("PROVERKA");
            // Создаем объект LeastSquares
            
            LeastSquaresMethod.FuncPsi[] funcMehod = new LeastSquaresMethod.FuncPsi[] { x => 2 * x + 1, x => 3 * x * x + 2 * x + 1 };
            LeastSquaresMethod ls2 = new LeastSquaresMethod(x_, y_, funcMehod);
            
            Vector coefficients = ls2.p;

            // Print the coefficients
            Console.WriteLine("Coefficients:");
            for (int i = 0; i < coefficients.Size; i++)
            {
                Console.WriteLine($"p{i} = {coefficients[i]}");
            }
            double criteria__ = ls2.GetCriteria();
            Console.WriteLine("Критерий: " + criteria__);
            */

            Vector x_ = new Vector(new double[] { 0, 5, 10, 15, 20, 25 });
            Vector y_ = new Vector(new double[] { 21, 39, 51, 63, 70, 90 });
            LeastSquaresMethod.FuncPsi[] psiArray = new LeastSquaresMethod.FuncPsi[] { x_ => x_, x_ => x_ * x_ };

            Console.WriteLine($"X: {x_}");
            Console.WriteLine($"Y: {y_}");
            Console.WriteLine("Function 1 = x");
            Console.WriteLine("Function 2 = x*x");

            LeastSquaresMethod func = new LeastSquaresMethod(x_, y_, psiArray);
            Console.WriteLine("Параметры: {0}", func.p);
            Console.WriteLine("Критерий: {0}", func.GetCriteria());

            Console.WriteLine("ИНТЕГРАЛ");

            Console.WriteLine();


            

            double eps = 1e-6;

            double result1 = IntegralCalculator.RectangleMethod(0, Math.PI, eps, x => Math.Cos(x));
            double result2 = IntegralCalculator.TrapezoidalMethod(0, Math.PI, eps, x => Math.Cos(x));
            double result3 = IntegralCalculator.SimpsonMethod(0, Math.PI, eps, x => Math.Cos(x));


            Console.WriteLine($"Rectangle method: {result1}");
            Console.WriteLine($"Trapezoidal method: {result2}");
            Console.WriteLine($"Simpson method: {result3}");


            double resultDouble = IntegralCalculator.SimpsonMethod(0, Math.PI, 0, Math.PI, eps, (x, y) => Math.Sin(x) * Math.Cos(y));

            Console.WriteLine($"Double integral: {resultDouble}");

            
            Console.WriteLine("Дифференциальные уравнения");

           
            // Пример использования метода Эйлера
            double t0 = 0.0;
            double tEnd = 1.0;
            int steps = 10;
            Vector x0 = new Vector(2);
            x0[0] = 0.0;
            x0[1] = 1.0;

            Matrix eulerResult = DifferentialEquations.EulerMethod(t0, tEnd, x0, steps, DifferentialEquations.PravDU);

            Console.WriteLine("Метод Эйлера:");
            Matrix.PrintMatrix(eulerResult);

            Console.WriteLine();

            // Пример использования метода Адамса
            Matrix adamsResult = DifferentialEquations.AdamsMethod(t0, tEnd, x0, steps, DifferentialEquations.PravDU);

            Console.WriteLine("Метод Адамса:");
            Matrix.PrintMatrix(adamsResult);


            Matrix rk2Result = DifferentialEquations.RungeKutta2(t0, tEnd, x0, steps, DifferentialEquations.PravDU);

            Console.WriteLine("Метод Рунге-Кутты второго порядка (RK2):");
            Matrix.PrintMatrix(rk2Result);

            Console.WriteLine();

            // Пример использования метода Рунге-Кутты четвертого порядка (RK4)
            Matrix rk4Result = DifferentialEquations.RungeKutta4(t0, tEnd, x0, steps, DifferentialEquations.PravDU);

            Console.WriteLine("Метод Рунге-Кутты четвертого порядка (RK4):");
            Matrix.PrintMatrix(rk4Result);


            Console.ReadLine();


        }
    }
}