using System;
using System.Collections.Generic;

namespace DataStructure
{
    public class Graph<T> where T : notnull
    {
        private readonly Dictionary<T, HashSet<T>> _adjacency = new ();
        public bool IsDirected { get; }

        public Graph(bool isDirected)
        {
            IsDirected = isDirected;
        }

        public void AddVertex(T vertex)
        {
            if(!_adjacency.ContainsKey(vertex))
            {
                _adjacency[vertex] = new HashSet<T>();
            }
        }

        public void AddEdge(T from, T to)
        {
            AddVertex(from);
            AddVertex(to);

            _adjacency[from].Add(to);

            if(!IsDirected)
            {
                _adjacency[to].Add(from);
            }
        }
        public IEnumerable<T> GetNeighbors(T vertex)
        {
            if(!_adjacency.TryGetValue(vertex, out HashSet<T>? neighbors))
            {
                throw new ArgumentException($"존재하지 않는 정점입니다: {vertex}");
            }

            return neighbors;
        }

        public List<T> RecursiveDFS(T start)
        {
            ValidateVertex(start);

            var result = new List<T>();
            var visited = new HashSet<T>();

            RecursiveDFSInternal(start, result, visited);

            return result;
        }

        private void RecursiveDFSInternal(T current, List<T> result,  HashSet<T> visited)
        {
            if(!visited.Add(current))
            {
                return;
            }

            result.Add(current);

            foreach(T neighbor in  _adjacency[current])
            {
                RecursiveDFSInternal(neighbor, result, visited);
            }
        }

        public List<T> IterativeDFS(T start)
        {
            ValidateVertex(start);

            List<T> result = new List<T>();
            Stack<T> stack = new Stack<T>();
            HashSet<T> visited = new HashSet<T>();

            stack.Push(start);
            visited.Add(start);

            while (stack.Count > 0)
            {
                T current = stack.Pop();
                result.Add(current);

                foreach(T neighbor in _adjacency[current])
                {
                    if (visited.Add(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }

            return result;
        }

        public List<T> BFSOrder(T start)
        {
            ValidateVertex(start);

            var result = new List<T>();
            var visited = new HashSet<T>();
            var queue = new CircularQueue<T>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (T neighbor in _adjacency[current])
                {
                    if(visited.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        public Dictionary<T, int> BFSDistance(T start)
        {
            ValidateVertex(start);

            var queue = new CircularQueue<T>();
            var distances = new Dictionary<T, int>();

            foreach (var elem in _adjacency.Keys)
            {
                distances[elem] = -1;
            }

            queue.Enqueue(start);
            distances[start] = 0;
            
            while(queue.Count > 0)
            {
                T current = queue.Dequeue();

                foreach (var next in _adjacency[current])
                {
                    if (distances[next] != -1)
                        continue;

                    distances[next] = distances[current] + 1;
                    queue.Enqueue(next);
                }
            }

            return distances;
        }

        public void ValidateVertex(T vertex)
        {
            if(!_adjacency.ContainsKey(vertex))
            {
                throw new ArgumentException($"존재하지 않는 정점입니다: {vertex}");
            }
        }
    }
}
