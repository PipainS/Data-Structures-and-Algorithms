using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkedList
{
    public class Item<K, T>
    {
        protected K key; // Ключ
        protected T value; // Значение
        public T Value // Свойство
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public K Key // Свойство
        {
            get { return this.key; }
            set { this.key = value; }
        }
        public override string ToString()
        {
            return string.Format("(key={0},Value={1})", Key, Value);
        }
        // Конструкторы
        public Item() { this.key = default(K); this.value = default(T); }
        public Item(K key, T value) { this.key = key; this.value = value; }
    }
    class SingleNode<K, T> : Item<K, T>
    {
        // Класс основан на классе Item<K,T>, который является информационной частью
        // узла односвязного списка 
        private SingleNode<K, T> next; // Ссылка на следующий узел
        // Конструкторы узла
        public SingleNode(K key, T value) : base(key, value)
        {
            this.next = null;
        }
        public SingleNode() : base()
        {
            this.next = null;
        }
        public SingleNode<K, T> Next // Свойство
        {
            get { return this.next; }
            set { this.next = value; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    

    class SingleLinkedList<K, T> where K : IComparable where T : IComparable
    {
        private SingleNode<K, T>? first = null;
        private int count = 0;

        public SingleNode<K, T> First { get { return first; } }
        public int Count { get { return count; } }

        public SingleLinkedList()
        {
            first = null;
            count = 0;
        }

        public int AddBegin(K key, T value)
        {
            SingleNode<K, T> newNode = new SingleNode<K, T>(key, value);
            newNode.Next = first;
            first = newNode;
            return ++count;
        }

        public int AddEnd(K key, T value)
        {
            SingleNode<K, T> newNode = new SingleNode<K, T>(key, value);

            if (first == null)
            {
                first = newNode;
            }
            else
            {
                SingleNode<K, T> lastNode = first;
                while (lastNode.Next != null)
                {
                    lastNode = lastNode.Next;
                }
                lastNode.Next = newNode;
            }

            return ++count;
        }

        public void Clear()
        {
            first = null;
            count = 0;
        }

        public bool ContainsValue(T value)
        {
            SingleNode<K, T> currentNode = first;
            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(value) == 0)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public bool ContainsKey(K key)
        {
            SingleNode<K, T> currentNode = first;
            while (currentNode != null)
            {
                if (currentNode.Key.CompareTo(key) == 0)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public SingleNode<K, T> GetNode(int index)
        {
            if (first == null || index >= count)
            {
                return null;
            }

            SingleNode<K, T> currentNode = first;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode;
        }

        public int InsertAfterNode(K key, K newKey, T value)
        {
            if (ContainsKey(key))
            {
                SingleNode<K, T> newNode = new SingleNode<K, T>(newKey, value);
                SingleNode<K, T> currentNode = first;

                while (currentNode.Key.CompareTo(key) != 0)
                {
                    currentNode = currentNode.Next;
                }

                newNode.Next = currentNode.Next;
                currentNode.Next = newNode;

                return ++count;
            }

            return count;
        }

        public int InsertBeforeNode(K key, K newKey, T value)
        {
            if (ContainsKey(key))
            {
                SingleNode<K, T> newNode = new SingleNode<K, T>(newKey, value);

                if (first.Key.CompareTo(key) == 0)
                {
                    newNode.Next = first;
                    first = newNode;
                }
                else
                {
                    SingleNode<K, T> currentNode = first;
                    SingleNode<K, T> previousNode = null;

                    while (currentNode.Key.CompareTo(key) != 0)
                    {
                        previousNode = currentNode;
                        currentNode = currentNode.Next;
                    }

                    newNode.Next = currentNode;
                    previousNode.Next = newNode;
                }

                return ++count;
            }
            else
            {
                return AddEnd(newKey, value);
            }
        }

        public void RemoveFirstNode()
        {
            if (first != null)
            {
                first = first.Next;
                count--;
            }
        }

        public void RemoveLastNode()
        {
            if (first == null)
            {
                return;
            }
            if (first.Next == null)
            {
                first = null;
            }
            else
            {
                SingleNode<K, T> currentNode = first;
                while (currentNode.Next.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = null;
            }

            count--;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                return;
            }

            if (index == 0)
            {
                RemoveFirstNode();
                return;
            }

            SingleNode<K, T> currentNode = first;
            for (int i = 0; i < index - 1; i++)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = currentNode.Next.Next;
            count--;
        }

        public void Remove(K key)
        {
            if (first == null)
            {
                return;
            }

            if (first.Key.Equals(key))
            {
                RemoveFirstNode();
                return;
            }

            SingleNode<K, T> currentNode = first;
            while (currentNode.Next != null)
            {
                if (currentNode.Next.Key.Equals(key))
                {
                    if (currentNode.Next.Next != null)
                    {
                        currentNode.Next = currentNode.Next.Next;
                    }
                    else
                    {
                        RemoveLastNode();
                    }

                    count--;
                    break;
                }
                currentNode = currentNode.Next;
            }
        }

        public void WriteList()
        {
            SingleNode<K, T> currentNode = first;
            while (currentNode != null)
            {
                Console.WriteLine("Key: {0}, Value: {1}", currentNode.Key, currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public void Reverse()
        {
            SingleNode<K, T> previousNode = null;
            SingleNode<K, T> currentNode = first;
            SingleNode<K, T> nextNode = null;

            while (currentNode != null)
            {
                nextNode = currentNode.Next;
                currentNode.Next = previousNode;
                previousNode = currentNode;
                currentNode = nextNode;
            }

            first = previousNode;
        }
    }
}
