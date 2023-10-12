using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    enum COLORS_VERTEX
    {
        WHITE,
        GRAY,
        BLACK
    }

    class Vertex
    {
        private static int IDV = 0;
        private int ID;
        public string label; // Метка (имя вершины)
        private List<Edge> edges; // Список ребер, связанных с вершиной
        public double sumdistance; // Сумма растояний
        public COLORS_VERTEX color; // Цвет вершины
        public Vertex prevvertex; // Ссылка на предшественника
        public bool visited;

        public Vertex(string label) // Конструктор
        {
            this.label = label;
            IDV++;
            edges = new List<Edge>();
            sumdistance = Double.MaxValue;
            color = COLORS_VERTEX.WHITE;
            prevvertex = null;
            ID = IDV;
            this.visited = false;
        }

        public int GetID() { return ID; }

        // Получение списка ребер
        public List<Edge> GetEdges() { return edges; }

        public override string ToString()
        {
            string sout = "";
            sout = sout + label;
            sout = sout + "  ID=" + ID.ToString();
            return sout;
        }

        // Просмотр ребер, связанных с вершиной
        public void ViewEdges()
        {
            Console.Write("Edges for {0}", this);
            foreach (Edge curedge in edges)
                Console.Write("  {0}", curedge);
            Console.WriteLine();
        }

        // Добавление ребра
        public bool AddEdge(Edge edge)
        {
            if (edge.BeginPoint != this) return false;
            for (int i = 0; i < edges.Count; i++)
            {
                Edge CurEdge = edges[i];
                if (edge.EndPoint.Equals(CurEdge.EndPoint)) return false;
            }
            edges.Add(edge);
            return true;
        }
    }

    class Edge
    {
        public Vertex BeginPoint; // Начальная вершина
        public Vertex EndPoint;  // Конечная вершина
        public double distance; // Длина ребра

        // Конструктор
        public Edge(Vertex begin, Vertex end, double d)
        {
            this.BeginPoint = begin;
            this.EndPoint = end;
            this.distance = d;
        }

        public override string ToString()
        {
            string sout = "";
            sout = "{" + BeginPoint.label + "  " + EndPoint.label + " D=" + distance.ToString() + "}";
            return sout;
        }
    }
    class Graph
    {
        public List<Vertex> allVertexs; // Список всех вершин
        public List<Edge> allEdges; // Список всех ребер
        // Конструктор
        public Graph()
        {
            allVertexs = new List<Vertex>();
            allEdges = new List<Edge>();
        }
        // Добавление ребра
        public bool AddEdge(Vertex v1, Vertex v2, double d)
        {
            if (!allVertexs.Contains(v1)) return false;
            if (!allVertexs.Contains(v2)) return false;
            foreach (Edge cure in v1.GetEdges())
            {
                if (cure.EndPoint.GetID() == v2.GetID()) return false;
            }


            Edge ev1v2 = new Edge(v1, v2, d);
            v1.GetEdges().Add(ev1v2); allEdges.Add(ev1v2);
            return true;
        }
        // Поиск в ширину
        public void BFS(Vertex s)
        {
            // Создаем очередь, в которую будем помещать вершины графа
            Queue<Vertex> Q = new Queue<Vertex>();

            // Проходим по всем вершинам графа и устанавливаем начальные значения
            foreach (Vertex cv in allVertexs)
            {
                cv.sumdistance = double.MaxValue; // Расстояние до вершины еще не известно, устанавливаем максимальное значение
                cv.prevvertex = null; // Предыдущая вершина не известна, устанавливаем значение null
            }

            // Устанавливаем начальное состояние для стартовой вершины
            s.color = COLORS_VERTEX.GRAY; // Цвет серый - вершина еще не обработана
            s.sumdistance = 0; // Расстояние от стартовой вершины до самой себя равно 0
            Q.Enqueue(s); // Помещаем стартовую вершину в очередь

            // Объявляем переменные, которые будем использовать в цикле
            Vertex u, v;
            Edge tr;
            List<Edge> edges_u;

            // Основной цикл
            List<Vertex> used_vertexes = new List<Vertex>(); // Создаем список, в который будем добавлять уже посещенные вершины
            while (Q.Count > 0) // Пока очередь не пуста
            {
                u = Q.Dequeue(); // Получаем первую вершину из очереди

                used_vertexes.Add(u); // Добавляем вершину в список посещенных

                // Получаем список всех смежных вершин для вершины u
                edges_u = u.GetEdges();
                foreach (Edge e in edges_u) // Перебираем все смежные вершины
                {
                    v = e.EndPoint;
                    // Если вершина v еще не была посещена, то добавляем ее в очередь и обновляем информацию о ней
                    if (v.color == COLORS_VERTEX.WHITE)
                    {
                        v.color = COLORS_VERTEX.GRAY; // Помечаем вершину как обрабатываемую
                        v.sumdistance = u.sumdistance + e.distance; // Обновляем расстояние до вершины v
                        v.prevvertex = u; // Обновляем информацию о предыдущей вершине для v
                        Q.Enqueue(v); // Добавляем вершину v в очередь
                    }
                }
                u.color = COLORS_VERTEX.BLACK; // Помечаем вершину как посещенную
            }
        }
        
        public void DFS(Vertex startVertex)
        {
            // Инициализация
            foreach (Vertex vertex in allVertexs)
            {
                vertex.color = COLORS_VERTEX.WHITE; // Устанавливаем цвет вершины в белый (вершина не посещена)
                vertex.prevvertex = null; // Устанавливаем предыдущую вершину в null (предыдущая вершина еще не известна)
            }

            // Вызываем вспомогательную рекурсивную функцию для посещения вершин
            DFSVisit(startVertex);

            // Получаем пути и дистанции от начальной вершины до всех вершин в графе
            foreach (Vertex vertex in allVertexs)
            {
                List<Vertex> path = Get_Path(startVertex, vertex);
                double distance = vertex.sumdistance;

                Console.WriteLine("Path from {0} to {1}: ", startVertex.label, vertex.label);
                foreach (Vertex v in path)
                {
                    Console.Write(v.label + " -> ");
                }
                Console.WriteLine("Distance: " + distance);
            }
        }

        private void DFSVisit(Vertex currentVertex)
        {
            currentVertex.color = COLORS_VERTEX.GRAY; // Устанавливаем цвет текущей вершины в серый (вершина посещается)

            foreach (Edge edge in currentVertex.GetEdges()) // Перебираем все ребра, выходящие из текущей вершины
            {
                Vertex nextVertex = edge.EndPoint; // Получаем следующую вершину по ребру

                if (nextVertex.color == COLORS_VERTEX.WHITE) // Если следующая вершина еще не была посещена
                {
                    nextVertex.prevvertex = currentVertex; // Устанавливаем текущую вершину как предыдущую для следующей вершины
                    DFSVisit(nextVertex); // Рекурсивно вызываем функцию посещения для следующей вершины
                }
            }

            currentVertex.color = COLORS_VERTEX.BLACK; // Устанавливаем цвет текущей вершины в черный (вершина посещена полностью)
        }
        
        public List<Vertex> Get_Path(Vertex s, Vertex v)
        {
            List<Vertex> list = new List<Vertex>();
            if (v.sumdistance == double.MaxValue) return list;
            if (v == s) { list.Add(s); return list; }
            Vertex tmp;
            tmp = v;
            list.Add(v);
            while (tmp != null)
            {
                // list.Add(tmp); 
                if (tmp == s) return list;
                tmp = tmp.prevvertex;
                list.Add(tmp);
            }

            return new List<Vertex>();
        }
        
       
        /*
            Код реализует алгоритм Дейкстры для поиска кратчайшего пути в графе, начиная с заданной вершины s. Ниже представлен переписанный код с комментариями: 
         */
        public void Dijkstra(Vertex s)
        {
            // Инициализация
            foreach (Vertex v in allVertexs)
            {
                v.sumdistance = double.MaxValue; // Устанавливаем расстояние до вершины как бесконечность
                v.prevvertex = null; // Устанавливаем предыдущую вершину в null (предыдущая вершина еще не известна)
            }
            s.sumdistance = 0; // Устанавливаем расстояние до начальной вершины как 0
            HashSet<Vertex> S = new HashSet<Vertex>(); // Множество посещенных вершин
            HashSet<Vertex> V = new HashSet<Vertex>(allVertexs); // Множество всех вершин
            while (S.Count != V.Count) // Пока не посетили все вершины
            {
                // Находим вершину с минимальным расстоянием
                Vertex u = null;
                foreach (Vertex v in V.Except(S)) // Перебираем все вершины, не принадлежащие множеству посещенных вершин
                {
                    if (u == null || v.sumdistance < u.sumdistance)
                        u = v;
                }
                S.Add(u); // Добавляем вершину с минимальным расстоянием в множество посещенных вершин
                          // Пересчет расстояний до всех соседей
                List<Edge> edges_u = u.GetEdges(); // Получаем список ребер, выходящих из вершины u
                foreach (Edge e in edges_u)
                {
                    Vertex v = e.EndPoint; // Получаем конечную вершину ребра
                    if (S.Contains(v)) // Если вершина v уже была посещена, пропускаем ее
                        continue;
                    double dist = u.sumdistance + e.distance; // Вычисляем расстояние до вершины v через вершину u
                    if (dist < v.sumdistance) // Если полученное расстояние меньше текущего расстояния до вершины v
                    {
                        v.sumdistance = dist; // Обновляем расстояние до вершины v
                        v.prevvertex = u; // Обновляем предыдущую вершину для вершины v
                    }
                }
            }
        }

        /*
         *  доп метод
         *  мы будем запускать алгоритм BFS из каждой вершины, которая еще не была посещена, и после каждого запуска увеличивать счетчик подграфов на 1.
         */

        public int CountSubgraphs()
        {
            int subgraphsCount = 0; // Счетчик подграфов
            List<Vertex> visitedVertices = new List<Vertex>(); // Список уже посещенных вершин

            foreach (Vertex cv in allVertexs)
            {
                if (!visitedVertices.Contains(cv))
                {
                    BFS(cv); // Запускаем алгоритм BFS из текущей вершины

                    // Помечаем все вершины, которые были посещены в текущем запуске BFS
                    foreach (Vertex v in allVertexs)
                    {
                        if (v.color == COLORS_VERTEX.BLACK && !visitedVertices.Contains(v))
                        {
                            visitedVertices.Add(v);
                        }
                    }

                    subgraphsCount++; // Увеличиваем счетчик подграфов на 1
                }
            }

            return subgraphsCount;
        }
        
    }
}
