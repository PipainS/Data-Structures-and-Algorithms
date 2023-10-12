using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data_Structures_and_Algorithms
{
    class Node<T>
    {

        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;

        public T value;
        public int key;

        public Node(T value, int key)
        {
            this.value = value;
            this.key = key;
            left = null; right = null; parent = null;
        }
        public Node()
        {
            this.value = default(T);
            this.key = 0;
            left = null; right = null; parent = null;
        }
        public override string ToString()
        {
            string node = string.Format("(Key = {0} Value = {1})", key, value);
            if (parent != null) node = node + string.Format(" Parent key={0}", parent.key);
            if (left != null) node = node + string.Format(" Left key={0}", left.key);
            if (right != null) node = node + string.Format(" Right key={0}", right.key);
            return node;
        }


    }
    class BinaryTree<T>
    {
        public Node<T> root; // Корень дерева
                      // вставка нового узла
        public void Insert(T value, int key)
        {
            Node<T> node = new Node<T>(value, key);
            // Если дерево пустое, то добавляем узел в качестве корня.
            if (root == null)
            {
                root = node; return;
            }
            // Если узел с таким ключом уже есть в дереве, то обновляем его значение.
            if (Find(node.key) != null)
            {
                Find(node.key).value = value; return;
            }
            // Если дерево не пустое и узел с таким ключом еще не существует в дереве, то добавляем узел в дерево.
            Node<T> currentNode = root;
            while (true)
            {
                // Если ключ текущего узла больше вставляемого ключа, то двигаемся влево.
                if (currentNode.key > key)
                {
                    // Если левый потомок отсутствует, то добавляем новый узел в качестве левого потомка.
                    if (currentNode.left == null)
                    {
                        currentNode.left = node;
                        node.parent = currentNode;
                        break;

                    } else { currentNode = currentNode.left; } // Если левый потомок существует, то двигаемся дальше влево.
                }
                // Если ключ текущего узла меньше вставляемого ключа, то двигаемся вправо.
                else
                {
                    // Если правый потомок отсутствует, то добавляем новый узел в качестве правого потомка.
                    if (currentNode.right == null)
                    {
                        currentNode.right = node;
                        node.parent = currentNode;
                        break;
                    } else { currentNode = currentNode.right; } // Если правый потомок существует, то двигаемся дальше вправо.
                }
            } 
        }
        // поиск узла по ключу
        public Node<T> Find(int key)
        {
            Node<T> currentNode = root;
            while (currentNode != null)
            {
                // проверка на корень
                if (currentNode.key == key) return currentNode;
                // движение вправо
                if (currentNode.key < key)
                {
                    currentNode = currentNode.right;
                    if (currentNode == null) return null;
                }
                // движение влево
                if (currentNode.key > key)
                {
                    currentNode = currentNode.left;
                }
            }
            return null;
        }
        // удаление по ключу
        public void Delete(int key)
        {
            Node<T> delete = Find(key);  // Найти узел с заданным ключом

            if (delete == null) return; // Если узел не найден, выйти из функции

            Node<T> right = delete.right; // Получить правого потомка узла
            Node<T> left = delete.left;   // Получить левого потомка узла

            if (delete == root) // Если узел для удаления - корневой узел
            {
                root = null;    // Установить корневой узел в null
                return;
            }

            if (right == null && left == null)  // Если узел не имеет потомков
            {
                // Удалить листовой узел
                DeleteLeaf(delete);
            }
            else if (right == null) // Если узел имеет только одного потомка (левого)
            {
                // Удалить узел с одним потомком
                DeleteOneChild(delete, left);
            }
            else if (left == null)  // Если узел имеет только одного потомка (правого)
            {
                // Удалить узел с одним потомком
                DeleteOneChild(delete, right);
            }
            else  // Если узел имеет двух потомков
            {
                // Удалить узел с двумя потомками
                DeleteTwoChildren(delete, right, left);
            }
        }
        private void DeleteLeaf(Node<T> node)
        {
            
            Node<T> parentNode = node.parent;
            if (parentNode != null)
            {
                if (parentNode.left == node)
                {
                    parentNode.left = null;
                }
                else
                {
                    parentNode.right = null;
                }
            }
            node = null;
        }

        private void DeleteOneChild(Node<T> node, Node<T> child)
        {
            Node<T> parentNode = node.parent;
            // правый дочерний узел
            if (parentNode.right == node)
            {
                parentNode.right = child;
                child.parent = parentNode;
                node = null;
            }
            // левый дочерний узел 
            else
            {
                parentNode.left = child;
                child.parent = parentNode;
                node = null;    
            }

        }

        private void DeleteTwoChildren(Node<T> node, Node<T> right, Node<T> left)
        {
            // Найдем узел, следующий за удаляемым
            Node<T> min = FindMin(right);
            // Запомним родительский узел удаляемого узла
            Node<T> parentNode = node.parent;

            if (min != right)
            {
                // Если найденный узел не является правым дочерним узлом удаляемого узла
                if (parentNode.right == node)
                {
                    // Правый дочерний узел найденного узла станет правым дочерним узлом родительского узла удаляемого узла
                    min.right = right;
                    // Левый дочерний узел найденного узла станет левым дочерним узлом удаляемого узла
                    min.left = left;
                    // Найденный узел становится дочерним узлом родительского узла удаляемого узла
                    parentNode.right = min;
                    // Установим ссылку на родительский узел для правого дочернего узла найденного узла
                    right.parent = min;
                    // Установим ссылку на родительский узел для левого дочернего узла найденного узла
                    left.parent = min;
                    // Удаляемый узел теперь ссылается на null
                    node = null;
                }
                // Если найденный узел не является левым дочерним узлом удаляемого узла
                else
                {
                    // Правый дочерний узел найденного узла станет правым дочерним узлом удаляемого узла
                    min.right = right;
                    // Левый дочерний узел найденного узла станет левым дочерним узлом удаляемого узла
                    min.left = left;
                    // Найденный узел становится дочерним узлом родительского узла удаляемого узла
                    parentNode.left = min;
                    // Установим ссылку на родительский узел для правого дочернего узла найденного узла
                    right.parent = min;
                    // Установим ссылку на родительский узел для левого дочернего узла найденного узла
                    left.parent = min;
                    // Удаляемый узел теперь ссылается на null
                    node = null;
                }
            }
            // Если найденный узел является правым дочерним узлом удаляемого узла
            else
            {
                min.left = left;
                left.parent = min;
                min.parent = parentNode;
                if (node == parentNode.right)
                {
                    parentNode.right = min;
                }
                else parentNode.left = min;
            }
        }
        
        // просмотр дерева
        public void ViewTree()
        {
            ViewTree(root);
            Console.WriteLine();
        }
        // рекурсивный метод просмотра поддерева
        public void ViewTree(Node<T> node)
        {
            if (node == null) return;
            // просмотр левого поддерева
            ViewTree(node.left);
            // информация о текущем узле
            Console.WriteLine(node);
            // просмотр правого поддерева
            ViewTree(node.right);
        }
        
        
        // поиск узла с минимальным значением ключа начиная с узла node
        public Node<T> FindMin(Node<T> node)
        {
            while (node.left != null)
            {
                node = node.left;
            }
            return node;
        }
        // поиск узла с максимальным значением ключа начиная с узла node
        public Node<T> FindMax(Node<T> node)
        {
            Node<T> node1= node.right;
            return node1;
        }
        // поиск узла со следующим значением ключа чем t.key
        public Node<T> Next(Node<T> t)
        {
            if (t != null)
            {
                if (t.right != null) return FindMin(t.right);
                Node<T> y = t.parent;
                while (y != null && t == y.right)
                {
                    t = y;
                    y = y.parent;
                }
                return y;
            }
            return null;
        }
        // поиск узла с предыдущем значением ключа чем t.key
        public Node<T> Prev(Node<T> node)
        {
            if (node != null)
            {
                if (node.left != null) return FindMax(node.left);
                Node<T> y = node.parent;
                while (y != null && node == y.left)
                {
                    node = y;
                    y = y.parent;
                }
                return y;
            }
            return null;
        }
        public Node<T> prev(Node<T> t)
        {
            if (t == null) return null;
            if (t.left != null)
            {
                t = t.left;
                while (t.right != null)
                {
                    t = t.right;
                }
                return t;
            }
            else
            {
                Node<T> y = t.parent;
                while (y != null && t == y.left)
                {
                    t = y;
                    y = y.parent;
                }
                return y;
            }
        }
        public void ViewFromMin()
        {
            Node<T> currentNode = FindMin(root);
            while (Next(currentNode) != null)
            {
                Console.WriteLine(currentNode);
                currentNode = Next(currentNode);
            }
            Console.WriteLine(currentNode);
        }

        public void ViewFromMax()
        {
            Node<T> currentNode = FindMax(root);
            while (currentNode != null)
            {
                Console.WriteLine(currentNode);
                currentNode = prev(currentNode);
            }
            Console.WriteLine(currentNode);
        }
        public bool IsBalanced()
        {
            return CheckBalance(root) != -1;
        }

        private int CheckBalance(Node<T> node)
        {
            if (node == null)
                return 0;

            int leftHeight = CheckBalance(node.left);
            if (leftHeight == -1)
                return -1;

            int rightHeight = CheckBalance(node.right);
            if (rightHeight == -1)
                return -1;

            int balanceFactor = leftHeight - rightHeight;
            if (Math.Abs(balanceFactor) > 1)
                return -1;

            return Math.Max(leftHeight, rightHeight) + 1;
        }


    }

}
