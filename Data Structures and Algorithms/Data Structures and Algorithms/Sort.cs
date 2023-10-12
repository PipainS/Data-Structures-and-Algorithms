using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    class Sort
    {
        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        // сортировка пузырьком
        public static void BubbleSort<T>(T[] list) where T : IComparable
        {
            int n = list.Length;
            bool swapped;

            do
            {
                swapped = false;

                for (int i = 0; i < n - 1; i++)
                {
                    // Сравниваем текущий элемент с соседним
                    if (list[i].CompareTo(list[i + 1]) > 0)
                    {
                        Swap(ref list[i], ref list[i + 1]);
                        swapped = true;
                    }
                }

                n--;
            }
            while (swapped);
        }

        // Алгоритм сортировки выбором
        public static void MaxSort<T>(T[] list) where T : IComparable
        {
            int n = list.Length;

            for (int i = 0; i < n; i++)
            {
                int maxIndex = 0;

                for (int j = 1; j < n - i; j++)
                {
                    // Ищем максимальный элемент
                    if (list[j].CompareTo(list[maxIndex]) > 0)
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != n - i - 1)
                {
                    Swap(ref list[maxIndex], ref list[n - i - 1]);
                }
            }
        }
        // Алгоритм сортировки вставками
        public static void InsertionSort<T>(T[] list) where T : IComparable
        {
            int n = list.Length;

            for (int i = 1; i < n; i++)
            {
                T key = list[i];
                int j = i - 1;

                // Сдвигаем элементы вправо, чтобы вставить элемент в правильную позицию
                while (j >= 0 && list[j].CompareTo(key) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = key;
            }
        }
        // Алгоритм сортировки Шелла
        public static void ShellSort<T>(T[] list) where T : IComparable
        {
            int n = list.Length;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = list[i];
                    int j = i;

                    // Сдвигаем элементы вправо, чтобы вставить элемент в правильную позицию в подсписке
                    while (j >= gap && list[j - gap].CompareTo(temp) > 0)
                    {
                        list[j] = list[j - gap];
                        j -= gap;
                    }

                    list[j] = temp;
                }

                gap /= 2;
            }
        }
        // Алгоритм сортировки подсчетом
        public static void CountingSort(int[] list, int maxValue)
        {
            int size = list.Length;
            int[] counts = new int[maxValue + 1];

            // Подсчитываем количество вхождений каждого элемента
            for (int i = 0; i < size; i++)
            {
                counts[list[i]]++;
            }

            int index = 0;

            // Восстанавливаем отсортированный массив
            for (int i = 0; i <= maxValue; i++)
            {
                while (counts[i] > 0)
                {
                    list[index] = i;
                    index++;
                    counts[i]--;
                }
            }
        }
        //Быстрая сортировка с рекурсией
        public static void QuickSortRecursive<T>(T[] list) where T : IComparable
        {
            QuickSortRecursive(list, 0, list.Length - 1);
        }

        private static void QuickSortRecursive<T>(T[] list, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                int pivotIndex = Partition(list, left, right);
                QuickSortRecursive(list, left, pivotIndex - 1);
                QuickSortRecursive(list, pivotIndex + 1, right);
            }
        }

        //Быстрая сортировка без рекурсии(используя стек)
        public static void QuickSortIterative<T>(T[] list) where T : IComparable
        {
            if (list.Length < 2)
                return;

            Stack<int> stack = new Stack<int>(list.Length);
            stack.Push(0);
            stack.Push(list.Length - 1);

            while (!stack.IsEmpty())
            {
                int end = stack.Pop();
                int start = stack.Pop();

                int pivotIndex = Partition(list, start, end);

                if (pivotIndex - 1 > start)
                {
                    stack.Push(start);
                    stack.Push(pivotIndex - 1);
                }

                if (pivotIndex + 1 < end)
                {
                    stack.Push(pivotIndex + 1);
                    stack.Push(end);
                }
            }
        }

        private static int Partition<T>(T[] list, int left, int right) where T : IComparable
        {
            T pivot = list[right];
            int i = left - 1;

            for (int j = left; j <= right - 1; j++)
            {
                if (list[j].CompareTo(pivot) < 0)
                {
                    i++;
                    Swap(ref list[i], ref list[j]);
                }
            }

            Swap(ref list[i + 1], ref list[right]);
            return i + 1;
        }
        // Сортировка слиянием с рекурсией
        public static void MergeSortRecursive<T>(T[] list) where T : IComparable
        {
            MergeSortRecursive(list, 0, list.Length - 1);
        }

        private static void MergeSortRecursive<T>(T[] list, int left, int right) where T : IComparable
        {

            if (left < right)
            {
                int n = list.Length;
                T[] tempArray = new T[n];
                int middle = (left + right) / 2;
                MergeSortRecursive(list, left, middle);
                MergeSortRecursive(list, middle + 1, right);
                Merge(list, tempArray, left, middle, right);
            }
        }

        // Сортировка слиянием без рекурсии
        public static void MergeSortIterative<T>(T[] list) where T : IComparable
        {
            int n = list.Length;
            T[] tempArray = new T[n];
            int currentSize;

            for (currentSize = 1; currentSize < n; currentSize *= 2)
            {
                for (int leftStart = 0; leftStart < n - 1; leftStart += 2 * currentSize)
                {
                    int middle = Math.Min(leftStart + currentSize - 1, n - 1);
                    int rightEnd = Math.Min(leftStart + 2 * currentSize - 1, n - 1);
                    Merge(list, tempArray, leftStart, middle, rightEnd);
                }
            }
        }

        private static void Merge<T>(T[] list, T[] tempArray, int left, int middle, int right) where T : IComparable
        {
            int lengthLeft = middle - left + 1;
            int lengthRight = right - middle;

            Array.Copy(list, left, tempArray, 0, lengthLeft);
            Array.Copy(list, middle + 1, tempArray, lengthLeft, lengthRight);

            int i = 0, j = lengthLeft;
            int k = left;

            while (i < lengthLeft && j < lengthLeft + lengthRight)
            {
                if (tempArray[i].CompareTo(tempArray[j]) <= 0)
                {
                    list[k] = tempArray[i];
                    i++;
                }
                else
                {
                    list[k] = tempArray[j];
                    j++;
                }
                k++;
            }

            while (i < lengthLeft)
            {
                list[k] = tempArray[i];
                i++;
                k++;
            }

            while (j < lengthLeft + lengthRight)
            {
                list[k] = tempArray[j];
                j++;
                k++;
            }
        }

    }
}
