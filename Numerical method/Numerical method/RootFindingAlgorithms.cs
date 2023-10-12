using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Numerical_method
{
    public delegate double Func(double x);
    class RootFindingAlgorithms
    {
        public static double BisectionMethod(double left_board, double right_board, double eps, Func func)
        {
            /*
             * Function Bisection Method
             */
            double funcA = func(left_board);
            double funcB = func(right_board);

            /// If hasn't root
            if (funcA * funcB > 0) { return double.NaN; }

            
            while (right_board - left_board > eps)
            {
                
                double middle = (left_board + right_board) * 0.5;
                double funcMiddle = func(middle);
                if (funcA * funcMiddle < 0) /// check on root
                {
                    /// move right board
                    right_board = middle;
                }
                else
                {
                    /// move left board
                    left_board = middle;
                    funcA = funcMiddle;
                }
            }
            return (left_board + right_board) / 2;
        }
        public static double NewtonsMethod(double currentPoint, double eps, Func func)
        {
            double delta = 0;
            double old_delta = double.MaxValue;

            do
            {
                /// текущая точка
                double funcPoint = func(currentPoint);
                /// Производная
                double derivative = (func(currentPoint + eps) - func(currentPoint)) / eps;
                /// Новое значение
                double newPoint = currentPoint - (funcPoint / derivative);

                /// Абсолютная разность 
                delta = Math.Abs(newPoint - currentPoint);
                currentPoint = newPoint;


                /// Проверка на расходимость
                if (delta > old_delta) { return double.NaN; }

                old_delta = delta;
            } while (delta > eps);

            return currentPoint;
        }
        public static double FixedPointIteration(double point, double eps, Func f)
        {
            double old_delta = double.MaxValue;
            double delta;
            do
            {
                double phiPoint = f(point);
                delta = Math.Abs(phiPoint - point);
                point = phiPoint;

                if (delta > old_delta) { return double.NaN; }
                old_delta = delta;
            } while (delta > eps);
            return point;
        }
    }
}
