using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №2-3: Сортировки");

            Console.WriteLine("Bubble Sort");

            int[] testArray = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.BubbleSort<int>(testArray);

            foreach (var item in testArray)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("Max Sort");

            int[] testMaxSort = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.MaxSort<int>(testMaxSort);

            foreach (var item in testMaxSort)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("Insertion Sort");

            int[] testInsertionSort = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.InsertionSort<int>(testInsertionSort);

            foreach (var item in testInsertionSort)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("Shell Sort");

            int[] testShellSort = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.ShellSort<int>(testShellSort);

            foreach (var el in testShellSort)
            {
                Console.Write("{0} \t", el);
            }

            Console.WriteLine();
            Console.WriteLine("Counting Sort");

            int[] testCountingSort = { 4, 43, 18, 5, 8, 3, 1, 33 };
            Sorting.CountingSort(testCountingSort, 43);

            foreach (var item in testCountingSort)
            {
                Console.Write("{0} \t", item);
            }
            Console.WriteLine();
            Console.WriteLine("QuickSort with recursion");

            int[] testQuickSortRec = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.QuickSortRecursive(testQuickSortRec);

            foreach (var item in testQuickSortRec)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("QuickSort without recursion");
            int[] testQuickSortIter = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.QuickSortIterative(testQuickSortIter);

            foreach (var item in testQuickSortIter)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("MergeSort with recursion");
            int[] testMergeSortRec = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.MergeSortRecursive(testMergeSortRec);

            foreach (var item in testMergeSortRec)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("MergeSort without recursion");
            int[] testMergeSortIter = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sorting.MergeSortIterative(testMergeSortIter);

            foreach (var item in testMergeSortIter)
            {
                Console.Write("{0} \t", item);
            }


        }
    }
}

