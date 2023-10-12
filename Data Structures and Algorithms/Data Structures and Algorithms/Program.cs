using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Structures_and_Algorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
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

            List<Set<int>> subsets = set1.subSets();
            Console.WriteLine("Subsets of set1:");
            foreach (Set<int> subset in subsets)
            {
                Console.WriteLine(subset);
            }

            List<Set<int>> combinations = set1.GetCombinations(2);
            Console.WriteLine("Combinations of set1 (k = 2) without repetitions:");
            foreach (Set<int> combination in combinations)
            {
                Console.WriteLine(combination.ToString());
            }
            Console.WriteLine("----------------------------------");
            */
            Console.WriteLine("Лабораторная работа №2-3: Сортировки");

            Console.WriteLine("Bubble Sort");

            int[] testArray = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.BubbleSort<int>(testArray);

            foreach (var item in testArray) 
            {
                Console.Write("{0} \t", item);
            }
            
            Console.WriteLine();
            Console.WriteLine("Max Sort");
            
            int[] testMaxSort = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.MaxSort<int>(testMaxSort);

            foreach (var item in testMaxSort) 
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("Insertion Sort");

            int[] testInsertionSort = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.InsertionSort<int>(testInsertionSort);

            foreach (var item in testInsertionSort)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("Shell Sort");

            int[] testShellSort = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.ShellSort<int>(testShellSort);

            foreach (var el in testShellSort)
            {
                Console.Write("{0} \t", el);
            }

            Console.WriteLine();
            Console.WriteLine("Counting Sort");

            int[] testCountingSort = { 4, 43, 18, 5, 8, 3, 1, 33 };
            Sort.CountingSort(testCountingSort, 43);

            foreach (var item in testCountingSort)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("QuickSort with recursion");

            int[] testQuickSortRec = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.QuickSortRecursive(testQuickSortRec);

            foreach (var item in testQuickSortRec)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("QuickSort without recursion");
            int[] testQuickSortIter = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.QuickSortIterative(testQuickSortIter);

            foreach (var item in testQuickSortIter)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("MergeSort with recursion");
            int[] testMergeSortRec = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.MergeSortRecursive(testMergeSortRec);

            foreach (var item in testMergeSortRec)
            {
                Console.Write("{0} \t", item);
            }

            Console.WriteLine();
            Console.WriteLine("MergeSort without recursion");
            int[] testMergeSortIter = { 4, -5, 18, 0, 8, 3, -9, -14 };
            Sort.MergeSortIterative(testMergeSortIter);

            foreach (var item in testMergeSortIter)
            {
                Console.Write("{0} \t", item);
            }


            Console.WriteLine();
            Console.WriteLine("_____________________________________________________________________________");
            Console.WriteLine();

            Console.WriteLine("Лабораторная работа - Односвязный список");
            Console.WriteLine();
            Console.WriteLine("Test Single linked list");
            Console.WriteLine();

            Item<int, string>[] sns = { new Item<int, string>(1, "fff"), new Item<int, string>(5, "fi"), new Item<int, string>(10, "tttt") };
            var sLL = new SingleLinkedList<int, string>();
            sLL.AddBegin(2, "Second");
            int CountSL = sLL.Count;
            for (int i = 0; i < sns.Length; i++) sLL.AddBegin(sns[i].Key, sns[i].Value);
            sLL.AddEnd(7, "Seven"); sLL.AddEnd(1345, "Sevкпen"); sLL.AddEnd(21, "Sуцамуeven");
            // Получение по индексу
            for (int k = 0; k < sLL.Count; k++)
            {
                Console.WriteLine("Элемент под индексом {0}: {1}", k, sLL.getNode(k));
            }
            Console.WriteLine("Метод Reverse");
            sLL.Reverse();

            for (int k = 0; k < sLL.Count; k++)
            {
                Console.WriteLine("Элемент под индексом {0}: {1}", k, sLL.getNode(k));
            }
            Console.WriteLine("----------------------------------");
            // Добавить в начало
            Console.WriteLine("Добавить в начало:");
            sLL.AddBegin(1, "First");
            sLL.WriteList();


            Console.WriteLine("----------------------------------");
            // Добавить в конец
            Console.WriteLine("Добавить в конец:");
            sLL.AddEnd(444, "Last");
            sLL.WriteList();

            
            Console.WriteLine("----------------------------------");
            // добавить после заданного ключа
            Console.WriteLine("Вставка после:");
            sLL.InsertAfterNode(5, 45, "After 5");
            sLL.WriteList();


            Console.WriteLine("----------------------------------");
            // Добавить до заданного ключа
            Console.WriteLine("Вставка до:");
            sLL.InsertBeforeNode(45, 910, "Before 45");
            sLL.WriteList();


            Console.WriteLine("----------------------------------");
            // Удаление первого узла
            Console.WriteLine("Удаление первого узла");
            sLL.RemoveFirstNode();
            sLL.WriteList();


            Console.WriteLine("----------------------------------");
            // Удаление последнего узла
            Console.WriteLine("Удаление последнего узла");
            sLL.RemoveLastNode();
            sLL.WriteList();


            Console.WriteLine("----------------------------------");
            // Удаление узла по индексу 
            Console.WriteLine("Удаление узла по индексу(2)");
            sLL.RemoveAt(2);
            sLL.WriteList();

            Console.WriteLine("----------------------------------");
            // Удаление узла по ключу
            Console.WriteLine("Удаление узла по ключу(21)");
            sLL.Remove(21);
            sLL.WriteList();

            Console.WriteLine("_____________________________________________________________");
            
           Stack<int> stack = new Stack<int>(5);

           stack.Push(1);
           stack.Push(2);
           stack.Push(3);
           stack.Push(4);
           stack.Push(5);
           stack.Push(6);
           Console.WriteLine("Stack");
           Console.WriteLine("(stack: 1, 2, 3, 4, 5)");
           Console.WriteLine("Метод Peek");
           Console.WriteLine(stack.Peek());
           Console.WriteLine("Метод Pop");
           Console.WriteLine(stack.Pop());
           Console.WriteLine(stack.Pop());

           Console.WriteLine(stack.Pop());
           Console.WriteLine(stack.Pop());

           Console.WriteLine(stack.Pop());
           Console.WriteLine("Если нет, то возвращает default(T)");
           Console.WriteLine(stack.Pop());
           Console.WriteLine("--------------------------------------------------------------------");
           Console.WriteLine("Проверка HashTable Открытое хэширование");

           HashTableOpen<string, string> car = new HashTableOpen<string, string>(10);
           Console.WriteLine("Добавление");
           car.Insert("lada", "Kamil");
           car.Insert("4matic", "aka");
           car.Insert("cerato", "volkswagen");
           car.Insert("solaris", "hyndai");
           car.Insert("polo", "kia");
           car.View();
           Console.WriteLine("Удаление по ключу 'lada' ");
           car.Delete("lada");
           car.View();
           Console.WriteLine("Поиск по ключу polo");
           Console.WriteLine(car.Search("polo"));

           Console.WriteLine();
           Console.WriteLine();

           Console.WriteLine("Проверка HashTable Открытое адресация ");

           HashTableClose<string, string> hashTableClose = new HashTableClose<string, string>(10);

           Console.WriteLine("Добавление");
           hashTableClose.Insert("AISD", "excellent");
           hashTableClose.Insert("CHM", "not good");
           hashTableClose.Insert("BD", "bad");
           hashTableClose.Insert("Voenka", "good");
           hashTableClose.Insert("MATSTAT", "god");
           hashTableClose.View();
           Console.WriteLine();

           Console.WriteLine("Удалить  BD");
           hashTableClose.Delete("BD");
           hashTableClose.View();


           Console.WriteLine("Поиск по ключу AISD");
           Console.WriteLine(hashTableClose.Search("AISD")); 
            
           Console.WriteLine("____________________________________________________________________");
           Console.WriteLine("БИНАРНОЕ ДЕРЕВО");

           BinaryTree<string> tree = new BinaryTree<string>();
           tree.Insert("b", 4);
           tree.Insert("a", 1);
           tree.Insert("d", 2);
           tree.Insert("c", 3);
           tree.Insert("e", 6);
           tree.Insert("l", 5);

           tree.Insert("j", 0);

           Console.WriteLine("ДЕРЕВО:");
           Console.WriteLine("Корень дерева: {0}", tree.root);
           tree.ViewTree();
           Console.WriteLine();
           Console.WriteLine("проверка баланса");
            Console.WriteLine(tree.IsBalanced()); 
            Console.WriteLine();
           Console.WriteLine("Поиск минимального значения");
           Console.WriteLine(tree.FindMin(tree.root));
           Console.WriteLine("Поиск максимального значения");
           Console.WriteLine(tree.FindMax(tree.root));
           Console.WriteLine("Удаление узла (1) с двумя дочерними узлами");
           tree.Delete(1);
           tree.ViewTree();
           Console.WriteLine("Просмотр дерева с минимального");
           tree.ViewFromMin();
           Console.WriteLine("Просмотр дерева с максимального");
           tree.ViewFromMax();
           Console.WriteLine("_________________________________________________________");

           var graph = new Graph();
           var v1 = new Vertex("v1");
           var v2 = new Vertex("v2");
           var v3 = new Vertex("v3");
           var v4 = new Vertex("v4");
           var v5 = new Vertex("v5");
           var v6 = new Vertex("v6");
           var v7 = new Vertex("v7");
           var v8 = new Vertex("v8");
           var v9 = new Vertex("v9");

           // добавляем вершины
           graph.allVertexs.Add(v1);
           graph.allVertexs.Add(v2);
           graph.allVertexs.Add(v3);
           graph.allVertexs.Add(v4);
           graph.allVertexs.Add(v5);
           graph.allVertexs.Add(v6);
           graph.allVertexs.Add(v7);
           graph.allVertexs.Add(v8);
           graph.allVertexs.Add(v9);


           // добавляем ребра
           Console.WriteLine("Добавляем ребра");
           graph.AddEdge(v1, v2, 1);
           graph.AddEdge(v2, v1, 1);

           graph.AddEdge(v2, v3, 1);
           graph.AddEdge(v3, v2, 1);

           graph.AddEdge(v3, v4, 1);
           graph.AddEdge(v4, v3, 1);

           graph.AddEdge(v4, v5, 1);
           graph.AddEdge(v5, v4, 1);

           graph.AddEdge(v5, v6, 1);
           graph.AddEdge(v6, v5, 1);

           graph.AddEdge(v6, v7, 1);
           graph.AddEdge(v7, v6, 1);

           graph.AddEdge(v6, v9, 1);
           graph.AddEdge(v9, v6, 1);

           graph.AddEdge(v7, v8, 1);
           graph.AddEdge(v8, v7, 1);

           graph.AddEdge(v8, v1, 1);
           graph.AddEdge(v1, v8, 1);

           graph.AddEdge(v8, v9, 1);
           graph.AddEdge(v9, v8, 1);


           Console.WriteLine("Проверка BFS");
           graph.BFS(v1);
           foreach (Vertex v in graph.allVertexs)
           {
               Console.WriteLine("Vertex {0}: distance = {1}", v.label, v.sumdistance);
           }
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("Проверка DFS");
            graph.DFS(v1);
            foreach (Vertex v in graph.allVertexs)
            {
                Console.WriteLine("Vertex {0}: distance = {1}", v.label, v.sumdistance);
            }
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine("__________________________");
            Console.WriteLine("Проверка алгоритма Дейкстры");



            graph.Dijkstra(v1);

           Console.WriteLine("Кратчайший путь от " + v1.label + " до " + v4.label + " равен " + v4.sumdistance);


           Console.WriteLine("---------------------------------------------------------");


           Console.WriteLine();

            
          
            Console.ReadKey();

            
        }
    }
}
