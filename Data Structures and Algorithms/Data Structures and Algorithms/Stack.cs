using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    public class Stack<T>
    {
        private int _Size;
        private T[] _Array;
        private int _Top;

        /// Конструктор. Создаём стек
        public Stack(int Size)
        {
            this._Size = Size;
            this._Top = 0;
            this._Array = new T[this._Size];
        }

        /// Маркер верхнего элемента стека
        public int Top
        {
            get
            {
                return this._Top;
            }
        }

        // Размер стека
        public int Size
        {
            get
            {
                return this._Size;
            }
        }

        // Проверить заполнен ли стек
        public bool IsFull()
        {
            return this._Top == this._Size;
        }

        // Проверить пустой ли стек
        public bool IsEmpty()
        {
            return this._Top == 0;
        }

        // Добавляем элемент в стек
        public void Push(T Element)
        {
            if (this.IsFull())
                Resize(this._Size * 2);
            this._Array[this._Top++] = Element; // сначала добавляем, потом делаем инкремент переменной 
        }
        // Вернуть верхний элемент
        public T Peek()
        {
            if (!this.IsEmpty())
                return this._Array[this._Top - 1]; // маркер не меняется 
            return default(T);
        }

        // Считывание из стека верхнего элемента
        public T Pop()
        {
            if (!this.IsEmpty())
            {
                return this._Array[--this._Top];
            }// маркеr меняется
            return default(T);
        }

        private void Resize(int newSize)
        {
            T[] newArray = new T[newSize];
            Array.Copy(_Array, newArray, Math.Min(newSize, _Size));
            this._Array = newArray;
            _Size = newSize;
            if (_Top > newSize)
                _Top = newSize;
        }
    }

}
