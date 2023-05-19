using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set
{
    class Set<T> where T : IComparable
    {
        private int size;
        private int capacity;
        private T[] data;

        public int Count => size; // Number of elements in the set

        public Set(int capacity)
        {
            size = 0;
            this.capacity = capacity > 0 ? capacity : 0;
            data = new T[this.capacity];
        }
        // Adds the specified value to the set
        public void Add(T value)
        {
            if (Contains(value))
                return;

            if (size == capacity)
            {
                capacity *= 2;
                Array.Resize(ref data, capacity);
            }

            data[size++] = value;
        }
        // Removes the specified value from the set
 
        public bool Remove(T value)
        {
            int index = GetIndex(value);
            if (index == -1)
                return false;

            for (int i = index; i < size - 1; i++)
                data[i] = data[i + 1];

            data[--size] = default(T);
            return true;
        }
        // Checks if the set contains the specified element
        public bool Contains(T element)
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(element))
                    return true;
            }
            return false;
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < size)
                    return data[index];
                return default(T);
            }
        }

        public override string ToString()
        {
            string result = "{";
            for (int i = 0; i < size; i++)
            {
                result += i < size - 1 ? $"{data[i]}, " : $"{data[i]}";
            }
            result += "}";
            return result;
        }
        // Returns a new set that represents the union of the current set and the specified set

        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(capacity + other.capacity);
            for (int i = 0; i < size; i++)
            {
                result.Add(data[i]);
            }
            for (int i = 0; i < other.size; i++)
            {
                result.Add(other[i]);
            }
            return result;
        }
        // Operator overload for the '+' operator to perform set union
        public static Set<T> operator +(Set<T> set1, Set<T> set2)
        {
            return set1.Union(set2);
        }
        // Returns a list of all subsets of the set
        public List<Set<T>> Subsets()
        {
            List<Set<T>> subsets = new List<Set<T>>();
            for (int i = 1; i <= size; i++)
            {
                Set<T> subset = new Set<T>(i);
                for (int j = 0; j < i; j++)
                {
                    subset.Add(data[j]);
                }
                subsets.Add(subset);
            }
            return subsets;
        }
        // Returns a new set that represents the intersection of the current set and the specified set
        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>(size);
            for (int i = 0; i < size; i++)
            {
                if (other.Contains(data[i]))
                {
                    result.Add(data[i]);
                }
            }
            return result;
        }
        // Operator overload for the '*' operator to perform set intersection
        public static Set<T> operator *(Set<T> set1, Set<T> set2)
        {
            return set1.Intersection(set2);
        }
        // Returns a new set that represents the difference between the current set and the specified set
        public Set<T> Difference(Set<T> other)
        {
            Set<T> result = new Set<T>(size);
            for (int i = 0; i < size; i++)
            {
                if (!other.Contains(data[i]))
                {
                    result.Add(data[i]);
                }
            }
            return result;
        }
        // Operator overload for the '-' operator to perform set difference
        public static Set<T> operator -(Set<T> set1, Set<T> set2)
        {
            return set1.Difference(set2);
        }

        private int GetIndex(T value)
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }
        // Helper method to check if there are more combinations to generate
        private bool CheckCombIndex(int[] index, int k)
        {
            for (int i = k - 1; i >= 0; i--)
            {
                if (index[i] < size - k + i + 1)
                {
                    index[i]++;
                    for (int j = i + 1; j < k; j++)
                    {
                        index[j] = index[j - 1] + 1;
                    }
                    return true;
                }
            }
            return false;
        }
        // Returns a list of all combinations of size k from the set
        public List<Set<T>> Combination(int k)
        {
            List<Set<T>> combinations = new List<Set<T>>();
            int[] index = new int[size];

            for (int i = 0; i < size; i++)
            {
                index[i] = i + 1;
            }

            if (size >= k)
            {
                Set<T> result = new Set<T>(size);

                for (int i = 0; i < k; i++)
                {
                    result.Add(data[index[i] - 1]);
                }

                combinations.Add(result);

                while (CheckCombIndex(index, k))
                {
                    Set<T> result2 = new Set<T>(size);
                    for (int i = 0; i < k; i++)
                    {
                        result2.Add(data[index[i] - 1]);
                    }
                    combinations.Add(result2);
                }
            }

            return combinations;
        }
    }
}
