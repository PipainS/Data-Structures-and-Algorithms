using HashTable;
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
            // Test HashTableOpen
            Console.WriteLine("Testing HashTableOpen:");
            HashTableOpen<string, int> hashTableOpen = new HashTableOpen<string, int>(5);

            // Insert elements
            hashTableOpen.Insert("apple", 1);
            hashTableOpen.Insert("banana", 2);
            hashTableOpen.Insert("orange", 3);
            hashTableOpen.Insert("grape", 4);
            hashTableOpen.Insert("kiwi", 5);

            // Search elements
            Console.WriteLine("Search 'apple': " + hashTableOpen.Search("apple"));
            Console.WriteLine("Search 'banana': " + hashTableOpen.Search("banana"));
            Console.WriteLine("Search 'orange': " + hashTableOpen.Search("orange"));
            Console.WriteLine("Search 'grape': " + hashTableOpen.Search("grape"));
            Console.WriteLine("Search 'kiwi': " + hashTableOpen.Search("kiwi"));

            // Delete elements
            Console.WriteLine("Delete 'banana': ");
            hashTableOpen.Delete("banana");
            Console.WriteLine("Delete 'kiwi': ");
            hashTableOpen.Delete("kiwi");
 
            // View the updated hash table
            Console.WriteLine("Updated HashTableOpen:");
            hashTableOpen.View();
            Console.WriteLine();


            // Test HashTableClose
            Console.WriteLine("Testing HashTableClose:");
            HashTableClose<string, int> hashTableClose = new HashTableClose<string, int>(5);

            // Insert elements
            hashTableClose.Insert("apple", 1);
            hashTableClose.Insert("banana", 2);
            hashTableClose.Insert("orange", 3);
            hashTableClose.Insert("grape", 4);
            hashTableClose.Insert("kiwi", 5);

            // Search elements
            Console.WriteLine("Search 'apple': " + hashTableClose.Search("apple"));
            Console.WriteLine("Search 'banana': " + hashTableClose.Search("banana"));
            Console.WriteLine("Search 'orange': " + hashTableClose.Search("orange"));
            Console.WriteLine("Search 'grape': " + hashTableClose.Search("grape"));
            Console.WriteLine("Search 'kiwi': " + hashTableClose.Search("kiwi"));

            // Delete elements
            Console.WriteLine("Delete 'banana': " + hashTableClose.Delete("banana"));
            Console.WriteLine("Delete 'kiwi': " + hashTableClose.Delete("kiwi"));

            // View the updated hash table
            Console.WriteLine("Updated HashTableClose:");
            hashTableClose.View();

        }
    }
}