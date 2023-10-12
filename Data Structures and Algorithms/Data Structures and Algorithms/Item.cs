using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
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

}
