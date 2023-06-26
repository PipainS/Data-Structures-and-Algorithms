using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class HashItem<K, T>
    {
        public K Key { get; set; } // Key
        public T Value { get; set; } // Value

        public HashItem(K key, T value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"(key={Key}, Value={Value})";
        }
    }

    class HashTableOpen<K, T> where K : IComparable where T : IComparable
    {
        private int capacity; // Capacity of the hash table
        private int size; // Number of elements in the hash table
        private Dictionary<int, List<HashItem<K, T>>> items;

        public IReadOnlyCollection<KeyValuePair<int, List<HashItem<K, T>>>>? Item => items?.ToList()?.AsReadOnly();

        public HashTableOpen(int capacity)
        {
            this.capacity = capacity;
            size = 0;
            items = new Dictionary<int, List<HashItem<K, T>>>(this.capacity);
        }

        private int GetHash(K key)
        {
            return Math.Abs(key.GetHashCode()) % capacity;
        }

        private void Resize(int newCapacity)
        {
            var newItems = new Dictionary<int, List<HashItem<K, T>>>(newCapacity);

            foreach (var entry in items)
            {
                foreach (var item in entry.Value)
                {
                    int newIndex = Math.Abs(item.Key.GetHashCode() % newCapacity);

                    if (!newItems.ContainsKey(newIndex))
                    {
                        newItems[newIndex] = new List<HashItem<K, T>>();
                    }

                    newItems[newIndex].Add(item);
                }
            }

            items = newItems;
            capacity = newCapacity;
        }

        public void Insert(K key, T value)
        {
            var item = new HashItem<K, T>(key, value);
            var hash = GetHash(key);

            if (items.ContainsKey(hash))
            {
                var hashTableItem = items[hash];

                if (hashTableItem.Any(i => i.Key.CompareTo(item.Key) == 0))
                {
                    Console.WriteLine("Value already exists.");
                    return;
                }

                hashTableItem.Add(item);
            }
            else
            {
                var hashTableItem = new List<HashItem<K, T>> { item };
                items.Add(hash, hashTableItem);
            }

            size++;

            if (size > capacity)
            {
                Resize(capacity * 2);
            }
        }

        public void Delete(K key)
        {
            var hash = GetHash(key);

            if (!items.ContainsKey(hash))
            {
                return;
            }

            var hashTableItem = items[hash];
            var item = hashTableItem.SingleOrDefault(i => i.Key.CompareTo(key) == 0);

            if (item != null)
            {
                hashTableItem.Remove(item);
                size--;

                if (size < capacity / 4)
                {
                    Resize(capacity / 2);
                }
            }
        }

        public HashItem<K, T>? Search(K key)
        {
            var hash = GetHash(key);

            if (!items.ContainsKey(hash))
            {
                return null;
            }

            var hashTableItem = items[hash];

            return hashTableItem.SingleOrDefault(i => i.Key.CompareTo(key) == 0);
        }

        public void View()
        {
            foreach (var item in items)
            {
                Console.WriteLine(item.Key);

                foreach (var value in item.Value)
                {
                    Console.WriteLine($"value: {value.Key}, key: {value.Value}");
                }
            }

            Console.WriteLine();
        }
    }
    class HashTableClose<K, T>
    {
        private int capacity;
        private int size;
        private HashItem<K, T>[] items;

        public int Size => size;
        public int Capacity => capacity;

        public bool IsEmpty() => size == 0;

        public HashTableClose(int capacity)
        {
            this.capacity = capacity;
            size = 0;
            items = new HashItem<K, T>[capacity];
        }

        private int GetHash(K key)
        {
            return Math.Abs(key.GetHashCode()) % capacity;
        }

        public void Insert(K key, T value)
        {
            if (size == capacity)
            {
                Resize(capacity * 2);
            }

            int index = GetHash(key);

            while (items[index] != null && !EqualityComparer<K>.Default.Equals(items[index].Key, key))
            {
                index = (index + 1) % capacity;
            }

            items[index] = new HashItem<K, T>(key, value);
            size++;
        }

        public T Search(K key)
        {
            int index = GetHash(key);

            while (items[index] != null && !EqualityComparer<K>.Default.Equals(items[index].Key, key))
            {
                index = (index + 1) % capacity;
            }

            if (items[index] == null)
            {
                throw new KeyNotFoundException();
            }

            return items[index].Value;
        }

        public void View()
        {
            foreach (var item in items)
            {
                if (item != null)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }
        }

        public bool Delete(K key)
        {
            int index = GetHash(key);

            while (items[index] != null && !EqualityComparer<K>.Default.Equals(items[index].Key, key))
            {
                index = (index + 1) % capacity;
            }

            if (items[index] == null)
            {
                return false;
            }
            else
            {
                items[index] = null;
                size--;
                return true;
            }
        }

        private void Resize(int newCapacity)
        {
            HashItem<K, T>[] newItems = new HashItem<K, T>[newCapacity];

            for (int i = 0; i < capacity; i++)
            {
                if (items[i] != null)
                {
                    K key = items[i].Key;
                    T value = items[i].Value;
                    int newIndex = Math.Abs(key.GetHashCode()) % newCapacity;

                    while (newItems[newIndex] != null && !EqualityComparer<K>.Default.Equals(newItems[newIndex].Key, key))
                    {
                        newIndex = (newIndex + 1) % newCapacity;
                    }

                    newItems[newIndex] = new HashItem<K, T>(key, value);
                }
            }

            capacity = newCapacity;
            items = newItems;
        }
    }
}
