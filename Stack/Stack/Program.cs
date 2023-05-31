using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание стека с размером 5
            Stack<int> stack = new Stack<int>(5);

            Console.WriteLine($"Is stack empty? {stack.IsEmpty()}"); // Выводит True

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);
            stack.Push(50);
            Console.WriteLine($"Is stack full? {stack.IsFull()}"); // Выводит True

            Console.WriteLine($"Top element: {stack.Peek()}"); // Выводит 50

            while (!stack.IsEmpty())
            {
                Console.WriteLine($"Popped element: {stack.Pop()}"); // Выводит 50, 40, 30, 20, 10
            }

            Console.WriteLine($"Is stack empty? {stack.IsEmpty()}"); // Выводит True

            Console.ReadLine();
        }
    }
}
