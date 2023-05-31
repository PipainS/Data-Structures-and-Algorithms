using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    public class Stack<T>
    {
        private T[] _array;
        private int _top;

        public Stack(int size)
        {
            _array = new T[size];
            _top = 0;
        }

        public int Top => _top;

        public int Size => _array.Length;

        public bool IsFull()
        {
            return _top == _array.Length;
        }

        public bool IsEmpty()
        {
            return _top == 0;
        }

        public void Push(T element)
        {
            if (IsFull())
                Resize(_array.Length * 2);
            _array[_top++] = element;
        }

        public T? Peek()
        {
            if (!IsEmpty())
                return _array[_top - 1];
            return default(T);
        }

        public T? Pop()
        {
            if (!IsEmpty())
                return _array[--_top];
            return default(T);
        }

        private void Resize(int newSize)
        {
            T[] newArray = new T[newSize];
            Array.Copy(_array, newArray, Math.Min(newSize, _array.Length));
            _array = newArray;
        }
    }
}
