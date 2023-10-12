// Seeusing System;
using AVLTreeCopy;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTreeCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int, string> tree = new AVLTree<int, string>();

            // Вставка элементов
            tree.Insert(5, "Value 5");
            tree.Insert(3, "Value 3");
            tree.Insert(7, "Value 7");
            tree.Insert(2, "Value 2");
            tree.Insert(4, "Value 4");
            tree.Insert(6, "Value 6");
            tree.Insert(8, "Value 8");

            // Получение значения по ключу
            Console.WriteLine("Получение значения по клюуч 4");
            string value1 = tree[4];
            Console.WriteLine(value1); // Выводит "Value 4"

            // Проверка наличия ключа
            Console.WriteLine("Проверка наличия ключа 7");
            bool containsKey = tree.ContainsKey(7);
            Console.WriteLine(containsKey); // Выводит "True"

            Console.WriteLine("удаление по ключу 3");
            // Удаление элемента
            tree.Delete(3);
            Console.WriteLine(tree);
            // Проверка обхода дерева
            foreach (AVLNode<int, string> node in tree)
            {
                Console.WriteLine(node.Value);
            }
            /*
            Вывод:
            Value 5
            Value 2
            Value 4
            Value 7
            Value 6
            Value 8
            */

            // Проверка очистки дерева
            tree.Clear();
            Console.WriteLine("Проверка очистки дерева");
            
            Console.WriteLine(tree);

            AVLTree<int, string> tree1 = new AVLTree<int, string>();
            tree1.Insert(1, "Value 1");
            tree1.Insert(2, "Value 2");
            tree1.Insert(3, "Value 3");

            AVLTree<int, string> tree2 = new AVLTree<int, string>();
            tree2.Insert(1, "Value 1");
            tree2.Insert(2, "Value 2");
            tree2.Insert(3, "Value 3");

            AVLTree<int, string> tree3 = new AVLTree<int, string>();
            tree3.Insert(1, "Value 1");
            tree3.Insert(2, "Value 2");
            tree3.Insert(4, "Value 4");
            tree3.Insert(-1, "Value -1");
            tree3.Insert(-10, "Value -10");
            tree3.Insert(7, "Value 7");


            // Проверка Equals
            Console.WriteLine("Проверка Equals");
            bool areEqual1 = tree1.Equals(tree2);
            bool areEqual2 = tree1.Equals(tree3);

            Console.WriteLine("Tree 1 equals Tree 2: " + areEqual1); // Выводит "True"
            Console.WriteLine("Tree 1 equals Tree 3: " + areEqual2); // Выводит "False"

            

            tree3.ViewTree();

            Console.WriteLine("Delete root");

            Console.WriteLine(tree2);
            tree2.Delete(2);
            Console.WriteLine("After delete root");
            Console.WriteLine(tree2);
            Console.ReadLine();
        }
    }
}
