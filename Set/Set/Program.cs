using Set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №1: Множества");
            Set<int> set1 = new Set<int>(5);
            set1.Add(1);
            set1.Add(2);
            set1.Add(3);

            Set<int> set2 = new Set<int>(5);
            set2.Add(3);
            set2.Add(4);
            set2.Add(5);

            Console.WriteLine("Set1: " + set1); // Выводит: Set1: {1, 2, 3}
            Console.WriteLine("Set2: " + set2); // Выводит: Set2: {3, 4, 5}

            Set<int> unionSet = set1 + set2;
            Console.WriteLine("Union set: " + unionSet); // Выводит: Union set: {1, 2, 3, 4, 5}

            Set<int> intersectionSet = set1 * set2;
            Console.WriteLine("Intersection set: " + intersectionSet); // Выводит: Intersection set: {3}

            Set<int> differenceSet = set1 - set2;
            Console.WriteLine("Difference set: " + differenceSet); // Выводит: Difference set: {1, 2}

            List<Set<int>> subsets = set1.Subsets();
            Console.WriteLine("Subsets of set1:");
            foreach (Set<int> subset in subsets)
            {
                Console.WriteLine(subset);
            }

            List<Set<int>> combinations = set1.Combination(2);
            Console.WriteLine("Combinations of set1 (k = 2) without repetitions:");
            foreach (Set<int> combination in combinations)
            {
                Console.WriteLine(combination.ToString());
            }
        }
    }
}