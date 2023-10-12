using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTreeCopy
{
    public class AVLTree<TKey, TValue> : IEnumerable<AVLNode<TKey, TValue>> where TKey : IComparable<TKey>
    {
        private AVLNode<TKey, TValue> root;

        public AVLTree()
        {
            this.root = null;
        }

        public TValue this[TKey key]
        {
            set
            {
                this.Insert(key, value);
            }
            get
            {
                TValue result;
                if (!this.TryGetValue(key, out result))
                {
                    throw new KeyNotFoundException();
                }
                return result;
            }
        }

        ///<summary>
        ///Если есть узел с заданным ключом, мы задаем параметру result его значение и возвращаем true,
        ///В противном случае возвращаем false и устанавливаем параметру result значение по умолчанию.
        ///</summary>
        public bool TryGetValue(TKey key, out TValue result)
        {
            AVLNode<TKey, TValue> current = root;
            while (current != null)
            {
                if (current.Key.CompareTo(key) == -1)
                {
                    current = current.RightChild;
                }
                else if (current.Key.CompareTo(key) == 1)
                {
                    current = current.LeftChild;
                }
                else
                {
                    result = current.Value;
                    return true;
                }
            }
            result = default(TValue);
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            TValue tmp;
            return this.TryGetValue(key, out tmp);
        }

        public void Insert(TKey key, TValue value)
        {
            if (this.root == null)
            {
                this.root = new AVLNode<TKey, TValue>(key, value);
            }
            else
            {
                AVLNode<TKey, TValue> currentNode = root;
                while (currentNode != null)
                {
                    if (currentNode.Key.CompareTo(key) == -1)
                    {
                        if (currentNode.RightChild == null)
                        {
                            currentNode.RightChild = new AVLNode<TKey, TValue>(key, value, currentNode);
                            /*
                             * Поскольку мы вставляем узел в правый дочерний элемент текущего узла, высота
                             * правого поддерева увеличивается, поэтому разница 
                             * высота (левое поддерево) - высота (правое поддерево) уменьшается на 1.
                             */

                            InsertBalanceTree(currentNode, -1);
                            break;
                        }
                        else
                        {
                            currentNode = currentNode.RightChild;
                        }
                    }
                    else if (currentNode.Key.CompareTo(key) == 1)
                    {
                        if (currentNode.LeftChild == null)
                        {
                            currentNode.LeftChild = new AVLNode<TKey, TValue>(key, value, currentNode);
                            /*
                             * Поскольку мы вставляем узел в левый дочерний элемент текущего узла, высота
                             * левого поддерева увеличивается, поэтому разница 
                             * высота (левое поддерево) - высота (правое поддерево) увеличивается на 1.
                             */
                            InsertBalanceTree(currentNode, 1);
                            break;
                        }
                        else
                        {
                            currentNode = currentNode.LeftChild;
                        }
                    }
                    else
                    {
                        currentNode.Value = value;
                        break;
                    }
                }
            }
        }

     
        public void Clear()
        {
            this.root = null;
        }

        // Корректирует коэффициенты баланса для затронутых узлов дерева.
        private void InsertBalanceTree(AVLNode<TKey, TValue> node, int addBalance)
        {
            while (node != null)
            {
                //Добавьте новое значение баланса к текущему балансу узла.
                node.Balance += addBalance;

                /*
                * Если баланс был равен -1 или +1, дерево по-прежнему сбалансировано, поэтому
                * нам не нужно его дополнительно балансировать
                */
                if (node.Balance == 0)
                {
                    break;
                }
                //height(left-subtree) - height(right-subtree) == 2
                else if (node.Balance == 2)
                {
                    if (node.LeftChild.Balance == 1)
                    {
                        RotateLeftLeft(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }
                    break;
                }

                //height(left-subtree) - height(right-subtree) == -2
                else if (node.Balance == -2)
                {
                    if (node.RightChild.Balance == -1)
                    {
                        RotateRightRight(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }
                    break;
                }

                if (node.Parent != null)
                {
                    /*
                     * Если текущий узел является левым дочерним узлом родительского узла
                     * нам нужно увеличить высоту родительского узла. 
                     * */
                    if (node.Parent.LeftChild == node)
                    {
                        addBalance = 1;
                    }
                    /*
                     * Если это правильный дочерний узел,
                     * мы уменьшаем высоту родительского узла
                     * */
                    else
                    {
                        addBalance = -1;
                    }
                }
                node = node.Parent;
            }
        }

        /// <summary>
        /// Makes right-right rotation.
        /// </summary>
        private void RotateRightRight(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> rightChild = node.RightChild;
            AVLNode<TKey, TValue> rightLeftChild = null;
            AVLNode<TKey, TValue> parent = node.Parent;

            // Проверяем, существует ли правый потомок узла
            if (rightChild != null)
            {
                // Получаем левого потомка правого потомка
                rightLeftChild = rightChild.LeftChild;
                // Устанавливаем ссылку на родителя у правого потомка
                rightChild.Parent = parent;
                // Левым потомком правого потомка становится узел
                rightChild.LeftChild = node;
                // Увеличиваем баланс правого потомка на 1
                rightChild.Balance++;
                // Обновляем баланс узла в соответствии с балансом правого потомка
                node.Balance = -rightChild.Balance;
            }

            // Правым потомком узла становится левый потомок правого потомка
            node.RightChild = rightLeftChild;
            // Устанавливаем ссылку на родителя узла на правый потомок
            node.Parent = rightChild;

            if (rightLeftChild != null)
            {
                // Обновляем ссылку на родителя у левого потомка правого потомка
                rightLeftChild.Parent = node;
            }

            if (node == this.root)
            {
                // Если узел был корневым, то обновляем корень дерева
                this.root = rightChild;
            }
            else if (parent.RightChild == node)
            {
                // Если узел был правым потомком родителя, то обновляем ссылку на правого потомка
                parent.RightChild = rightChild;
            }
            else
            {
                // Если узел был левым потомком родителя, то обновляем ссылку на левого потомка
                parent.LeftChild = rightChild;
            }
        }

        /// <summary>
        /// Makes left-left rotation.
        /// </summary>
        private void RotateLeftLeft(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> leftChild = node.LeftChild;
            AVLNode<TKey, TValue> leftRightChild = null;
            AVLNode<TKey, TValue> parent = node.Parent;

            // Проверяем, существует ли левый потомок узла
            if (leftChild != null)
            {
                // Получаем правого потомка левого потомка
                leftRightChild = leftChild.RightChild;
                // Устанавливаем ссылку на родителя у левого потомка
                leftChild.Parent = parent;
                // Правым потомком левого потомка становится узел
                leftChild.RightChild = node;
                // Уменьшаем баланс левого потомка на 1
                leftChild.Balance--;
                // Обновляем баланс узла в соответствии с балансом левого потомка
                node.Balance = -leftChild.Balance;
            }

            // Устанавливаем ссылку на родителя узла на левый потомок
            node.Parent = leftChild;
            // Левым потомком узла становится правый потомок левого потомка
            node.LeftChild = leftRightChild;

            if (leftRightChild != null)
            {
                // Обновляем ссылку на родителя у правого потомка левого потомка
                leftRightChild.Parent = node;
            }

            if (node == this.root)
            {
                // Если узел был корневым, то обновляем корень дерева
                this.root = leftChild;
            }
            else if (parent.LeftChild == node)
            {
                // Если узел был левым потомком родителя, то обновляем ссылку на левого потомка
                parent.LeftChild = leftChild;
            }
            else
            {
                // Если узел был правым потомком родителя, то обновляем ссылку на правого потомка
                parent.RightChild = leftChild;
            }
        }


        /// <summary>
        /// Makes right-left rotation.
        /// </summary>
        private void RotateRightLeft(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> rightChild = node.RightChild;
            AVLNode<TKey, TValue> rightLeftChild = null;
            AVLNode<TKey, TValue> rightLeftRightChild = null;

            // Проверяем, существует ли правый потомок у узла
            if (rightChild != null)
            {
                // Получаем левого потомка правого потомка узла
                rightLeftChild = rightChild.LeftChild;
            }
            if (rightLeftChild != null)
            {
                // Получаем правого потомка левого потомка узла
                rightLeftRightChild = rightLeftChild.RightChild;
            }

            // Правым потомком узла становится левый потомок правого потомка
            node.RightChild = rightLeftChild;

            if (rightLeftChild != null)
            {
                // Обновляем ссылку на родителя у левого потомка
                rightLeftChild.Parent = node;
                // Правым потомком левого потомка становится правый потомок узла
                rightLeftChild.RightChild = rightChild;
                // Уменьшаем баланс левого потомка на 1
                rightLeftChild.Balance--;
            }

            if (rightChild != null)
            {
                // Обновляем ссылку на родителя у правого потомка
                rightChild.Parent = rightLeftChild;
                // Левым потомком правого потомка становится правый потомок левого потомка
                rightChild.LeftChild = rightLeftRightChild;
                // Уменьшаем баланс правого потомка на 1
                rightChild.Balance--;
            }

            if (rightLeftRightChild != null)
            {
                // Обновляем ссылку на родителя у правого потомка левого потомка
                rightLeftRightChild.Parent = rightChild;
            }

            // Выполняем правый поворот-правый для узла
            RotateRightRight(node);
        }


        /// <summary>
        /// Makes left-right rotation.
        /// </summary>
        private void RotateLeftRight(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> leftChild = node.LeftChild;
            AVLNode<TKey, TValue> leftRightChild = leftChild.RightChild;
            AVLNode<TKey, TValue> leftRightLeftChild = null;
            if (leftRightChild != null)
            {
                // Получаем левого потомка правого потомка левого потомка узла
                leftRightLeftChild = leftRightChild.LeftChild;
            }

            // Левым потомком узла становится правый потомок левого потомка
            node.LeftChild = leftRightChild;

            if (leftRightChild != null)
            {
                // Обновляем ссылку на родителя у правого потомка левого потомка
                leftRightChild.Parent = node;
                // Левым потомком правого потомка левого потомка становится левый потомок узла
                leftRightChild.LeftChild = leftChild;
                // Увеличиваем баланс правого потомка на 1
                leftRightChild.Balance++;
            }

            if (leftChild != null)
            {
                // Обновляем ссылку на родителя у левого потомка
                leftChild.Parent = leftRightChild;
                // Правым потомком левого потомка становится левый потомок правого потомка левого потомка
                leftChild.RightChild = leftRightLeftChild;
                // Увеличиваем баланс левого потомка на 1
                leftChild.Balance++;
            }

            if (leftRightLeftChild != null)
            {
                // Обновляем ссылку на родителя у левого потомка правого потомка левого потомка
                leftRightLeftChild.Parent = leftChild;
            }

            // Выполняем левый поворот-левый для узла
            RotateLeftLeft(node);
        }

        /// <summary>
        /// Удаляет узел из дерева с заданным ключом.
        /// </summary>
        public void Delete(TKey key)
        {
            AVLNode<TKey, TValue> current = this.root;
            while (current != null)
            {
                if (current.Key.CompareTo(key) == -1)
                {
                    current = current.RightChild;
                }
                else if (current.Key.CompareTo(key) == 1)
                {
                    current = current.LeftChild;
                }
                else 
                {
                    if (current.LeftChild == null && current.RightChild == null)
                    {
                        if (current == root)
                        {
                            root = null;
                        }
                        else if (current.Parent.RightChild == current)
                        {
                            current.Parent.RightChild = null;
                            DeleteBalanceTree(current.Parent, 1);
                        }
                        else
                        {
                            current.Parent.LeftChild = null;
                            DeleteBalanceTree(current.Parent, -1);
                        }
                    }
                    else if (current.LeftChild != null) //Получаем самый большой узел из левого поддерева.
                    {
                        AVLNode<TKey, TValue> rightMost = current.LeftChild;
                        while (rightMost.RightChild != null)
                        {
                            rightMost = rightMost.RightChild;
                        }


                        ReplaceNodes(current, rightMost);
                        DeleteBalanceTree(rightMost.Parent, 1);
                    }
                    else //Получаем наименьший узел из правого поддерева.
                    {
                        AVLNode<TKey, TValue> leftMost = current.RightChild;
                        while (leftMost.LeftChild != null)
                        {
                            leftMost = leftMost.LeftChild;
                        }

                        ReplaceNodes(current, leftMost);
                        DeleteBalanceTree(leftMost.Parent, -1);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Заменяет два узла и настраивает дочерние и родительские соединения.
        /// </summary>
        private void ReplaceNodes(AVLNode<TKey, TValue> sourceNode, AVLNode<TKey, TValue> subtreeNode)
        {
            sourceNode.Key = subtreeNode.Key;
            sourceNode.Value = subtreeNode.Value;

            if (subtreeNode.Parent != null)
            {
                if (subtreeNode.LeftChild != null)
                {
                    subtreeNode.LeftChild.Parent = subtreeNode.Parent;
                    if (subtreeNode.Parent.LeftChild == subtreeNode)
                    {
                        subtreeNode.Parent.LeftChild = subtreeNode.LeftChild;
                    }
                    else
                    {
                        subtreeNode.Parent.RightChild = subtreeNode.LeftChild;
                    }
                }
                else if (subtreeNode.RightChild != null)
                {
                    subtreeNode.RightChild.Parent = subtreeNode.Parent;
                    if (subtreeNode.Parent.LeftChild == subtreeNode)
                    {
                        subtreeNode.Parent.LeftChild = subtreeNode.RightChild;
                    }
                    else
                    {
                        subtreeNode.Parent.RightChild = subtreeNode.RightChild;
                    }
                }
                else
                {
                    if (subtreeNode.Parent.LeftChild == subtreeNode)
                    {
                        subtreeNode.Parent.LeftChild = null;
                    }
                    else
                    {
                        subtreeNode.Parent.RightChild = null;
                    }
                }
            }
        }

        /// <summary>
        /// Корректирует коэффициенты баланса узлов дерева после удаления узла.
        /// </summary>
        private void DeleteBalanceTree(AVLNode<TKey, TValue> node, int addBalance)
        {
            while (node != null)
            {
                node.Balance += addBalance;
                addBalance = node.Balance;

                if (node.Balance == 2)
                {
                    if (node.LeftChild != null && node.LeftChild.Balance >= 0)
                    {
                        RotateLeftLeft(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }
                }
                else if (node.Balance == -2)
                {
                    if (node.RightChild != null && node.RightChild.Balance <= 0)
                    {
                        RotateRightRight(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }
                }
                else if (node.Balance != 0)
                {
                    return;
                }

                AVLNode<TKey, TValue> parent = node.Parent;

                if (parent != null)
                {
                    if (parent.LeftChild == node)
                    {
                        addBalance = -1;
                    }
                    else
                    {
                        addBalance = 1;
                    }
                }
                node = parent;
            }
        }

        public static bool operator ==(AVLTree<TKey, TValue> a, AVLTree<TKey, TValue> b)
        {
            return AVLTree<TKey, TValue>.Equals(a, b);
        }

        public static bool operator !=(AVLTree<TKey, TValue> a, AVLTree<TKey, TValue> b)
        {
            return !AVLTree<TKey, TValue>.Equals(a, b);
        }

        //Обходит дерево в предварительном порядке.(pre-order)
        public IEnumerator<AVLNode<TKey, TValue>> GetEnumerator()
        {
            Queue<AVLNode<TKey, TValue>> queue = new Queue<AVLNode<TKey, TValue>>();
            queue.Enqueue(this.root);

            AVLNode<TKey, TValue> tmp;
            while (queue.Count > 0)
            {
                tmp = queue.Dequeue();

                if (tmp.LeftChild != null)
                {
                    queue.Enqueue(tmp.LeftChild);
                }
                if (tmp.RightChild != null)
                {
                    queue.Enqueue(tmp.RightChild);
                }

                yield return tmp;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("");

            Stack<AVLNode<TKey, TValue>> stack = new Stack<AVLNode<TKey, TValue>>();
            if (this.root != null)
            {
                stack.Push(root);
                AVLNode<TKey, TValue> tmpNode;
                while (stack.Count > 0)
                {
                    tmpNode = stack.Pop();

                    if (tmpNode.Parent == null)
                    {
                        result.AppendLine(tmpNode + " is root.");
                    }
                    else if (tmpNode.Parent.RightChild == tmpNode)
                    {
                        result.AppendLine(tmpNode.Parent + " has right child: " + tmpNode);
                    }
                    else
                    {
                        result.AppendLine(tmpNode.Parent + " has left child: " + tmpNode);
                    }

                    if (tmpNode.RightChild != null)
                    {
                        stack.Push(tmpNode.RightChild);
                    }
                    if (tmpNode.LeftChild != null)
                    {
                        stack.Push(tmpNode.LeftChild);
                    }
                }
            }
            return result.ToString();
        }
        public void ViewTree()
        {
            ViewTree(root);
            Console.WriteLine();
        }
        public void ViewTree(AVLNode<TKey, TValue> node)
        {
            if (node == null) return;   
            ViewTree(node.LeftChild);
            Console.WriteLine(node);
            ViewTree(node.RightChild);

        }
        /// <summary>
        /// Проверяет, соответствует ли каждый узел в текущем дереве соответствующему узлу в другом дереве.
        /// </summary>
        public override bool Equals(object obj)
        {
            AVLTree<TKey, TValue> tree = obj as AVLTree<TKey, TValue>;
            if (tree == null)
            {
                return false;
            }
            else
            {
                Queue<Tuple<AVLNode<TKey, TValue>, AVLNode<TKey, TValue>>> queue =
                    new Queue<Tuple<AVLNode<TKey, TValue>, AVLNode<TKey, TValue>>>();

                AVLNode<TKey, TValue> item1, item2;

                queue.Enqueue(Tuple.Create(this.root, tree.root));
                while (queue.Count > 0)
                {
                    item1 = queue.Peek().Item1;
                    item2 = queue.Peek().Item2;
                    queue.Dequeue();

                    if (!item1.Equals(item2))
                    {
                        return false;
                    }
                    else if (item1.LeftChild == null && item2.LeftChild != null)
                    {
                        return false;
                    }
                    else if (item1.LeftChild != null && item2.LeftChild == null)
                    {
                        return false;
                    }
                    else if (item1.RightChild == null && item2.RightChild != null)
                    {
                        return false;
                    }
                    else if (item1.RightChild != null && item2.RightChild == null)
                    {
                        return false;
                    }
                    if (item1.LeftChild != null && item2.LeftChild != null)
                    {
                        queue.Enqueue(Tuple.Create(item1.LeftChild, item2.LeftChild));
                    }
                    if (item1.RightChild != null && item2.RightChild != null)
                    {
                        queue.Enqueue(Tuple.Create(item1.RightChild, item2.RightChild));
                    }
                }
                return true;
            }
        }
    }
}
