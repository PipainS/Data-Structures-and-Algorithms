using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
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
    class SingleLinkedList<K, T>  where K : IComparable where T : IComparable
    {
        private SingleNode<K, T>? first = null;   // Ссылка на начальный узел
        private int pos = 0;
        public SingleNode<K, T> First { get { return first; } } // Свойство
                                                                //Конструктор
        public SingleLinkedList() { first = null; pos = 0; }
        public int Count { get { return pos; } } // Свойство
                                                 // Добавить в начало
        public int AddBegin(K key, T value)
        {
            SingleNode<K, T> s = new SingleNode<K, T>(key, value);
            s.Next = first;
            first = s;
            return this.pos++;
        }

        // Добавить в конец
        public int AddEnd(K key, T value)
        {
            SingleNode<K, T> s = new SingleNode<K, T>(key, value);
            // Если список пустой    
            if (this.first == null) { this.first = s; return this.pos++; }
            // Поиск последнего узла
            SingleNode<K, T> e = this.first;
            while (e.Next != null)
            {
                e = e.Next;
            }
            // Добавление в конец
            e.Next = s;
            return this.pos++;
        }
        // Очистка списка
        public void Clear()
        {
            this.first = null;
            this.pos = 0;
        }
        // Проверка на значение
        public bool ContainsValue(T value)
        {
            if (this.first != null)
            {
                SingleNode<K, T> e = this.first;
                do
                {
                    if (e.Value.CompareTo(value) == 0)
                    {
                        return true;
                    }
                    e = e.Next;
                } while (e != null);
            }
            return false;
        }
        public bool ContainsKey(K key)
        {
            if (this.first != null)
            {
                SingleNode<K, T> e = this.first;
                do
                {
                    if (e.Key.CompareTo(key) == 0)
                    {
                        return true;
                    }
                    e = e.Next;
                } while (e != null);
            }
            return false;
        }
        public SingleNode<K, T> getNode(int k)
        {
            if (this.first == null || k >= Count) { return null; }
            SingleNode<K, T> e = this.first;
            int i = 0;
            while (i < k)
            {
                e = e.Next;
                i++;
            }
            return e;
        }


        // Вставка после заданного узла
        public int InsertAfterNode(K key, K newKey, T Value)
        {
            SingleNode<K, T> e = new() { Key = newKey, Value = Value };
            SingleNode<K, T> currentNode = first;

            if (this.ContainsKey(key))
            {
                while (currentNode.Key.CompareTo(key) != 0)
                    currentNode = currentNode.Next;

                if (currentNode != null)
                {
                    if (this.first == null) { this.first = e; return this.pos++; }
                    else (e.Next, currentNode.Next) = (currentNode.Next, e);
                    return this.pos++;
                }
            }
            return this.pos;
        }
        //вставка до
        public int InsertBeforeNode(K key, K newKey, T Value)
        {
            SingleNode<K, T> e = new() { Key = newKey, Value = Value };
            SingleNode<K, T> currentNode = first;
            SingleNode<K, T> currentNode2 = first;

            if (this.ContainsKey(key))
            {
                if (currentNode.Key.CompareTo(key) == 0)
                {
                    AddBegin(newKey, Value);
                    return this.pos;
                }
                while (currentNode.Key.CompareTo(key) != 0)
                {
                    currentNode2 = currentNode;
                    currentNode = currentNode.Next;
                }
                if (currentNode != null)
                {
                    if (this.first == null) { this.first = e; return this.pos++; }
                    else (e.Next, currentNode2.Next) = (currentNode2.Next, e);
                    return this.pos++;
                }
            }
            else
                AddEnd(newKey, Value);
            return this.pos;
        }
        // Удаление начального узла
        public void RemoveFirstNode()
        {
            if (first == null)
                return;
            first = first.Next;
            this.pos--;
        }
        // Удаление конечного узла
        public void RemoveLastNode()
        {
            if (first == null)
            {
                return;
            }
            if (first.Next == null)
            {
                first = null;
                return;
            }
            SingleNode<K, T> currentNode = first;
            while (currentNode.Next.Next != null)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = null;
            this.pos--;
        }
        //удаление узла по номеру
        public void RemoveAt(int index)
        {
            if (index < 0)
                return;
            SingleNode<K, T> currentNode = first;
            for (int i = 0; i < index - 1; i++)
            {
                if (currentNode.Next == null)
                    return;
                currentNode = currentNode.Next;
            }
            if (currentNode.Next != null)
            {
                currentNode.Next = currentNode.Next.Next;
                this.pos--;
            }
        }
        //удаление узла по значению
        public void Remove(K key)
        {
            SingleNode<K, T> currentNode = first;
            if (currentNode.Key.Equals(key)) RemoveFirstNode();

            while (currentNode.Next != null)
            {
                if (currentNode.Next.Key.Equals(key))
                {
                    if (currentNode.Next.Next != null)
                    {
                        currentNode.Next = currentNode.Next.Next;
                        this.pos--;
                        break;
                    }
                    else
                    {
                        RemoveLastNode();
                        break;
                    }
                }
                currentNode = currentNode.Next;
            }
        }

        // Вывод списка
        public void WriteList()
        {
            var currentNode = first;
            while (currentNode != null)
            {
                Console.WriteLine("Key: {0}, Value: {1}", currentNode.Key, currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public void Reverse()
        {
            SingleNode<K, T> prev = null;
            SingleNode<K, T> current = first;
            SingleNode<K, T> next = null;

            while (current != null)
            {
                next = current.Next; // сохраняем ссылку на следующий узел
                current.Next = prev; // меняем ссылку на следующий узел на ссылку на предыдущий узел
                prev = current; // перемещаем указатель на текущий узел в переменную prev
                current = next; // перемещаем указатель на следующий узел в переменную current
            }

            first = prev; // после прохода по всем узлам, головной узел становится последним, поэтому обновляем ссылку first на новый головной узел
        }
    }   
}
       
    




