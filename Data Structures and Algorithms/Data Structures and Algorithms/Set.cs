using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    class Set<T> where T : IComparable
    {
        int size;
        int capacity;
        T[] data;
        bool done;
        
        public Set(int n)
        {
            this.capacity = 0;
            size = 0;
            if (n > 0)
            {
                data = new T[n];
                this.capacity = n;
            }
        }
        public int GetSize() { return capacity; }
        public int GetCount() { return size; }
        public int Count { get { return size; } } // Количество элементов в множестве

        public int GetIndex(T value)
        {
            int i;
            int select = -1;
            for (i = 0; i < size; i++)
                // if (data[i] == value) { select = i; break; }
                if (data[i].Equals(value)) { select = i; break; }
            return select;
        }
        public bool IsContains(T element)
        {
            for (int i = 0; i < size; i++) if (data[i].CompareTo(element) == 0) return true;
            return false;
        }

        public T this[int index]
        {
            get { if (index >= 0 && index < size) return data[index]; else return default(T); }
        }
        public bool Exists(T value)
        {
            if (GetIndex(value) >= 0) return true;
            else return false;
        }
        public void Add(T value)
        {
            int i;
            int select = GetIndex(value);
            if (select == -1)
            {
                if (size < capacity) { data[size] = value; size++; }
                else
                {
                    T[] temp = new T[capacity];
                    for (i = 0; i < capacity; i++) temp[i] = data[i];
                    data = new T[capacity * 2];
                    for (i = 0; i < capacity; i++) data[i] = temp[i];
                    capacity = 2 * capacity;
                    data[size] = value; size++;

                }
            }
        }
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < size)
            {
                int i = 0;
                for (i = index; i < size - 1; i++)
                    data[i] = data[i + 1];
                data[size] = default(T); size--;
            }
        }
        public void Remove(T value)
        {
            int index = GetIndex(value);
            RemoveAt(index);
        }
        public T GetValue(int index)
        {
            if (index >= 0 && index < size) return data[index];
            return default(T);
        }
        public override string ToString()
        {
            string ss = "{";
            int i;
            for (i = 0; i < size; i++)
                if (i < (size - 1)) ss = ss + string.Format("{0}, ", data[i]);
                else ss = ss + string.Format("{0}", data[i]);
            ss = ss + "}";

            return ss;
        }

        public Set<T> Union(Set<T> ss)
        {
            int resSize = this.capacity + ss.GetSize();

            Set<T> result = new(resSize);
            for (int i = 0; i < size; i++)
                result.Add(data[i]);
            for (int i = 0; i < ss.GetCount(); i++)
                result.Add(ss.GetValue(i));
            return result;
        }
        public static Set<T> operator +(Set<T> s1, Set<T> s2) => s1.Union(s2);
        public List<Set<T>> subSets()
        {
            List<Set<T>> LL = new List<Set<T>>();
            for (int i = 1; i <= size; i++)
            {
                Set<T> L = new Set<T>(i);
                for (int j = 0; j < i; j++)
                {
                    L.Add(this.data[j]);
                }
                LL.Add(L);
            }
            return LL;
        }
        public Set<T> Interesection(Set<T> s2)
        {
            Set<T> res = new(this.size);

            for (int i = 0; i < this.size; i++)
            {
                if (s2.IsContains(this[i]))
                {
                    res.Add(this[i]);
                }
            }
            return res;

        }
        public static Set<T> operator *(Set<T> s1, Set<T> s2) => s1.Interesection(s2);
        public Set<T> Addition(Set<T> s2)
        {
            Set<T> res = new(size);
            for (int i = 0; i < size; i++)
            {
                if (!s2.IsContains(this[i]))
                {
                    res.Add(this[i]);
                }
            }
            return res;
        }
        public static Set<T> operator -(Set<T> s1, Set<T> s2) => s1.Addition(s2);
       
        public List<Set<T>> GetCombinations(int combinationSize)
        {
            List<Set<T>> combinations = new List<Set<T>>(1);
            combinations.Add(new Set<T>(0)); // добавляем пустое множество в результат

            GenerateCombinations(combinations, new Set<T>(0), combinationSize, 0);

            return combinations;
        }

        private void GenerateCombinations(List<Set<T>> combinations, Set<T> currentCombination, int combinationSize, int startIndex)
        {
            if (currentCombination.GetCount() == combinationSize)
            {
                combinations.Add(currentCombination);
                return;
            }

            for (int i = startIndex; i < size; i++)
            {
                Set<T> newCombination = new Set<T>(currentCombination.GetCount() + 1);

                // копируем элементы из текущей комбинации в новую комбинацию
                for (int j = 0; j < currentCombination.GetCount(); j++)
                {
                    newCombination.Add(currentCombination[j]);
                }

                // добавляем текущий элемент множества
                newCombination.Add(data[i]);

                GenerateCombinations(combinations, newCombination, combinationSize, i + 1);
            }
        }
    }
}
