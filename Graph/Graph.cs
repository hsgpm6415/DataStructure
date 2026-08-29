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
        void ValidateVertex(T vertex)
        {
            if(!_adjacency.ContainsKey(vertex))
            {
                throw new ArgumentException($"존재하지 않는 정점입니다: {vertex}");
            }
        }
        public bool HasPath(T start, T destination)
        {
            ValidateVertex(start);
            ValidateVertex(destination);

            var visited = new HashSet<T>();
            var queue = new CircularQueue<T>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                T current = queue.Dequeue();

                if(EqualityComparer<T>.Default.Equals(current, destination))
                {
                    return true;
                }

                foreach (var neighbor in _adjacency[current])
                {
                    if(visited.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }
        public List<List<T>> FindConnectedComponents()
        {
            if (IsDirected)
            {
                throw new InvalidOperationException("무방향 그래프가 아닙니다.");
            }

            var results = new List<List<T>>();
            var visited = new HashSet<T>();

            foreach(T vertex in _adjacency.Keys)
            {
                if(visited.Contains(vertex))
                {
                    continue;
                }

                var queue = new CircularQueue<T>();
                var result = new List<T>();

                queue.Enqueue(vertex);
                visited.Add(vertex);

                while(queue.Count > 0)
                {
                    T current = queue.Dequeue();
                    result.Add(current);

                    foreach (T neighbor in _adjacency[current])
                    {
                        if(visited.Add(neighbor))
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                results.Add(result);
            }
            return results;
        }
        public List<T>? FindPath(T start, T destination)
        {
            ValidateVertex(start);
            ValidateVertex(destination);

            var result = new List<T>();
            var record = new Dictionary<T, T>();
            var queue = new CircularQueue<T>();
            var visited = new HashSet<T>();

            queue.Enqueue(start);
            visited.Add(start);
            
            if(EqualityComparer<T>.Default.Equals(start, destination))
            {
                result.Add(start);
                return result;
            }

            while (queue.Count > 0)
            {
                T current = queue.Dequeue();
                
                if(EqualityComparer<T>.Default.Equals(current, destination))
                {
                    break;
                }

                foreach (T neighbor in _adjacency[current])
                {
                    if(visited.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        record[neighbor] = current;
                    }
                }
            }

            if (!visited.Contains(destination))
            {
                return null;
            }

            T back = destination;
            result.Add(back);

            while (!EqualityComparer<T>.Default.Equals(back, start))
            {
                back = record[back];
                result.Add(back);
            }

            result.Reverse();

            

            return result;
        }
        public int GetShortestDistance(T start, T destination)
        {
            ValidateVertex(start);
            ValidateVertex(destination);

            var distances = BFSDistance(start);
            return distances[destination];
        }
        public List<T>? FindShortestPath(T start, T destination)
        {
            return FindPath(start, destination);
        }
        public bool UndirectedCycleDetection()
        {
            if(IsDirected)
            {
                throw new InvalidOperationException("무방향 그래프가 아닙니다.");
            }

            var visited = new HashSet<T>();
            var history = new Dictionary<T, T>();
            
            foreach (T vertex in _adjacency.Keys)
            {
                if (visited.Contains(vertex)) continue;

                var queue = new CircularQueue<T>();
                queue.Enqueue(vertex);
                visited.Add(vertex);
                
                while(queue.Count > 0)
                {
                    T current = queue.Dequeue();

                    foreach (T neighbor in _adjacency[current])
                    {
                        if(visited.Add(neighbor))
                        {
                            queue.Enqueue(neighbor);
                            history[neighbor] = current;
                        }
                        else
                        {
                            bool isParent =  history.TryGetValue(current, out T? parent) &&
                                                EqualityComparer<T>.Default.Equals(parent, neighbor);

                            if(!isParent)
                            {
                                return true;
                            }
                        }
                    }
                }

            }

            return false;
        }
        public bool DirectedCycleDetection()
        {
            if (!IsDirected)
            {
                throw new InvalidOperationException("방향 그래프가 아닙니다.");
            }

            var visited = new HashSet<T>();
            var visiting = new HashSet<T>();
            

            foreach (T vertex in _adjacency.Keys)
            {
                if(visited.Contains(vertex)) continue;

                if(DFSForDirectedCycleDetection(vertex, visiting, visited))
                {
                    return true;
                }
            }

            return false;
        }
        private bool DFSForDirectedCycleDetection(T vertex, HashSet<T> visiting, HashSet<T> visited)
        {
            if (visited.Contains(vertex))
            { 
                return false;
            }
            else if(visiting.Contains(vertex))
            {
                return true;
            }

            visiting.Add(vertex);

            foreach (T neighbor in _adjacency[vertex])
            {
                if (DFSForDirectedCycleDetection(neighbor, visiting, visited))
                {
                    return true;
                }
            }

            visiting.Remove(vertex);
            visited.Add(vertex);
            return false;
        }
        public List<T> TopologicalSortKhan()
        {
            if(!IsDirected)
            {
                throw new InvalidOperationException("방향 그래프가 아닙니다.");
            }

            var indegrees = new Dictionary<T, int>();

            foreach (T vertex in _adjacency.Keys)
            {
                indegrees[vertex] = 0;
            }
            
            foreach(T vertex in _adjacency.Keys)
            {
                foreach (T neighbor in _adjacency[vertex])
                {
                    indegrees[neighbor]++;
                }
            }

            var queue = new CircularQueue<T>();
            var result = new List<T>();


            foreach (T vertex in indegrees.Keys)
            {
                if (indegrees[vertex]==0)
                {
                    queue.Enqueue(vertex);
                }
            }

            while (queue.Count > 0)
            {
                T current = queue.Dequeue();
                result.Add(current);

                foreach (T neighbor in _adjacency[current])
                {
                    indegrees[neighbor]--;

                    if(indegrees[neighbor]==0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            if (result.Count != _adjacency.Count)
            {
                throw new InvalidOperationException(
                    "사이클이 존재하여 위상 정렬할 수 없습니다.");
            }

            return result;
        }

        public List<T> TopologicalSortDFS()
        {
            if(!IsDirected)
            {
                throw new InvalidOperationException("방향 그래프가 아닙니다.");
            }

            var result = new List<T>();
            var stack = new Stack<T>();
            var visiting = new HashSet<T>();
            var visited = new HashSet<T>();

            foreach (T vertex in _adjacency.Keys)
            {
                if (visited.Contains(vertex)) continue;

                DFSForTopologicalSort(vertex, visiting, visited, stack);
            }

            while(stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        public void DFSForTopologicalSort(T vertex, HashSet<T> visiting, HashSet<T> visited, Stack<T> stack)
        {
            if(visited.Contains(vertex)) return;

            if(!visiting.Add(vertex))
            {
                throw new InvalidOperationException("사이클이 존재합니다.");
            }

            foreach (T neighbor in _adjacency[vertex])
            {
                DFSForTopologicalSort(neighbor, visiting, visited, stack);
            }

            visiting.Remove(vertex);
            visited.Add(vertex);
            stack.Push(vertex);
        }
    }
}
