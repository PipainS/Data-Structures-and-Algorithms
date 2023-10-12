using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Structures_and_Algorithms
{
    class HashItem<K, T> : Item<K, T>
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
        public HashItem() { this.key = default(K); this.value = default(T); }
        public HashItem(K key, T value) { this.key = key; this.value = value; }
    }
    class HashTableOpen<K, T> where K : IComparable where T : IComparable
    {
        /// <summary>
        /// Метод цепочек (открытое хэширование)
        /// все элементы данных с совпадающем хешем объединяются в список.
        /// </summary>
        private int capacity; // ёмкость хэш-таблицы
        private int size; // количество элементов в хэш-таблице
                                // массив элементов хэш-таблицы

        private Dictionary<int, List<HashItem<K, T>>> items = null;
        public IReadOnlyCollection<KeyValuePair<int, List<HashItem<K, T>>>> Item => items?.ToList()?.AsReadOnly();
        public HashTableOpen(int capacity)
        {
            this.capacity = capacity;
            this.size = 0;
            items = new Dictionary<int, List<HashItem<K, T>>>(this.capacity);
        }
        private int GetHash(K key)
        {
            return Math.Abs(key.GetHashCode()) % capacity;
        }
        private void Resize(int newCapacity)
        {
            Dictionary<int, List<HashItem<K, T>>> newItems = new Dictionary<int, List<HashItem<K, T>>>(newCapacity);
            foreach (KeyValuePair<int, List<HashItem<K, T>>> entry in items)
            {
                foreach (HashItem<K, T> item in entry.Value)
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
            // Создаем новый элемент хеш-таблицы с заданным ключом и значением
            var item = new HashItem<K, T>(key, value);

            // Получаем хеш-значение для ключа
            var hash = GetHash(key);

            // Создаем список элементов, соответствующих данному хеш-значению
            List<HashItem<K, T>> hashTableItem = null;

            // Проверяем, существует ли уже список элементов с данным хеш-значением
            if (items.ContainsKey(hash))
            {
                // Если список уже существует, получаем ссылку на него
                hashTableItem = items[hash];

                // Проверяем, существует ли элемент с таким же ключом в списке элементов
                var oldElement = hashTableItem.SingleOrDefault(i => i.Key.CompareTo(item.Key) == 0);
                if (oldElement != null)
                {
                    // Если элемент уже существует, выводим сообщение об ошибке и выходим из метода
                    Console.WriteLine("Такое значение существует");
                    return;
                }

                // Если элемент с таким ключом не найден, добавляем новый элемент в список
                items[hash].Add(item);
            }
            else
            {
                // Если список элементов с данным хеш-значением не существует, создаем новый список 
                // и добавляем в него новый элемент. Затем добавляем список в хеш-таблицу с заданным хеш-значением.
                hashTableItem = new List<HashItem<K, T>> { item };
                items.Add(hash, hashTableItem);
            }
            
        }

        public void Delete(K key)
        {
            // Получаем хеш-значение для заданного ключа
            var hash = GetHash(key);

            // Если элемент с данным хеш-значением не существует, выходим из метода
            if (!items.ContainsKey(hash))
            {
                return;
            }

            // Получаем список элементов, соответствующих заданному хеш-значению
            var hashTableItem = items[hash];

            // Ищем элемент с заданным ключом в списке элементов
            var item = hashTableItem.SingleOrDefault(i => i.Key.CompareTo(key) == 0);

            // Если элемент с заданным ключом найден, удаляем его из списка элементов
            if (item != null)
            {
                hashTableItem.Remove(item);
            }
        }

        public HashItem<K, T> Search(K key)
        {
            // Получаем хеш-значение для заданного ключа
            var hash = GetHash(key);

            // Если элемент с данным хеш-значением не существует в хеш-таблице, возвращаем null
            if (!items.ContainsKey(hash))
            {
                return null;
            }

            // Получаем список элементов, соответствующих заданному хеш-значению
            var hashTableItem = items[hash];

            // Если список элементов не пустой
            if (hashTableItem != null)
            {
                // Ищем элемент с заданным ключом в списке элементов
                var item = hashTableItem.SingleOrDefault(i => i.Key.CompareTo(key) == 0);

                // Если элемент с заданным ключом найден, возвращаем его
                if (item != null)
                {
                    return item;
                }
            }

            // Если элемент с заданным ключом не найден, возвращаем null
            return null;
        }

        public void View()
        {
            // Перебираем все элементы в хеш-таблице
            foreach (var item in items)
            {
                // Выводим хеш-значение элемента
                Console.WriteLine(item.Key);

                // Перебираем все значения элемента и выводим их вместе с ключами
                foreach (var value in item.Value)
                {
                    Console.WriteLine("value: {0}, key: {1}", value.Key, value.Value);
                }
            }

            // Выводим пустую строку для удобства чтения
            Console.WriteLine();
        }
    }

    class HashTableClose<K, T>
    {
        private int capacity;
        private int size;
        private Item<K, T>[] items;

        // Функция для получения хеш-кода ключа
        private int GetHash(K key)
        {
            return Math.Abs(key.GetHashCode()) % capacity;
        }

        public int Size { get { return size; } }

        public int Cpacity { get { return this.capacity; } }

        // Функция для проверки, пуста ли хэш-таблица
        public bool IsEmpty() { return size == 0; }

        // Конструктор хэш-таблицы
        public HashTableClose(int capacity)
        {
            this.capacity = capacity;
            this.size = 0;
            items = new Item<K, T>[capacity];
        }

        // Функция для вставки элемента в хэш-таблицу
        public void Insert(K key, T value)
        {
            if (size == capacity)
            {
                // Увеличиваем емкость таблицы в два раза
                Resize(capacity * 2);
            }

            int index = GetHash(key);

            // Пока текущий индекс занят и ключи не совпадают,
            // ищем свободный индекс для вставки элемента
            while (items[index] != null && !EqualityComparer<K>.Default.Equals(items[index].Key, key))
            {
                index = (index + 1) % capacity;
            }

            // Вставляем элемент по найденному индексу
            items[index] = new Item<K, T>(key, value);
        }

        // Функция для поиска элемента в хэш-таблице
        public T Search(K key)
        {
            int index = GetHash(key);

            // Пока текущий индекс занят и ключи не совпадают,
            // ищем индекс элемента с заданным ключом
            while (items[index] != null && !EqualityComparer<K>.Default.Equals(items[index].Key, key))
            {
                index = (index + 1) % capacity;
            }

            // Если элемент не найден, выбрасываем исключение
            if (items[index] == null)
            {
                throw new KeyNotFoundException();
            }

            // Иначе возвращаем значение элемента
            return items[index].Value;
        }

        // Функция для вывода содержимого хэш-таблицы на экран
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

        // Функция для удаления элемента из хэш-таблицы
        public bool Delete(K key)
        {
            int index = GetHash(key);

            // Пока текущий индекс занят и ключи не совпадают,
            // ищем индекс элемента с заданным ключом
            while (items[index] != null && !EqualityComparer<K>.Default.Equals(items[index].Key, key))
            {
                index = (index + 1) % capacity;
            }

            // Если элемент не найден, возвращаем false
            if (items[index] == null)
            {
                return false;
            }
            else
            {
                // Иначе удаляем элемент из таблицы и возвращаем true
                items[index] = null;
                return true;
            }
        }

        private void Resize(int newCapacity)
        {
            // Создаем новый массив элементов с новой емкостью
            Item<K, T>[] newItems = new Item<K, T>[newCapacity];

            // Перебираем все элементы в текущей таблице и перехешируем их в новую таблицу
            for (int i = 0; i < capacity; i++)
            {
                if (items[i] != null)
                {
                    K key = items[i].Key;
                    T value = items[i].Value;

                    // Вычисляем новый хеш для ключа в новой таблице
                    int newIndex = Math.Abs(key.GetHashCode()) % newCapacity;

                    // Пока текущий индекс занят и ключи не совпадают,
                    // ищем свободный индекс для вставки элемента в новую таблицу
                    while (newItems[newIndex] != null && !EqualityComparer<K>.Default.Equals(newItems[newIndex].Key, key))
                    {
                        newIndex = (newIndex + 1) % newCapacity;
                    }

                    // Вставляем элемент в новую таблицу
                    newItems[newIndex] = new Item<K, T>(key, value);
                }
            }

            // Обновляем емкость и массив элементов текущей таблицы
            capacity = newCapacity;
            items = newItems;
        }
    }


}
