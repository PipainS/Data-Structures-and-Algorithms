using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Single linked list");
            SingleLinkedList<int, string> list = new SingleLinkedList<int, string>();

            list.AddEnd(1, "One");
            list.AddEnd(2, "Two");
            list.AddEnd(3, "Three");
            list.AddEnd(4, "Four");

            Console.WriteLine("Linked List:");
            list.WriteList();
            Console.WriteLine("Count: " + list.Count);

            Console.WriteLine();

            Console.WriteLine("Inserting node after key 2:");
            list.InsertAfterNode(2, 5, "Five");

            Console.WriteLine("Linked List:");
            list.WriteList();
            Console.WriteLine("Count: " + list.Count);

            Console.WriteLine();

            Console.WriteLine("Removing node with key 3:");
            list.Remove(3);

            Console.WriteLine("Linked List:");
            list.WriteList();
            Console.WriteLine("Count: " + list.Count);

            Console.WriteLine();

            Console.WriteLine("Reversing the linked list:");
            list.Reverse();

            Console.WriteLine("Linked List:");
            list.WriteList();
            Console.WriteLine("Count: " + list.Count);

            Console.ReadLine();

        }
    }
} 