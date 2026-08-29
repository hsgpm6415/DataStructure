using System;
using System.Collections.Generic;

namespace DataStructure
{
    public class WeightedGraph<T> where T : notnull
    {
        private readonly Dictionary<T, List<Edge<T>>> adjacencyList;

        public IEqualityComparer<T> Comparer { get; }

        public IEnumerable<T> Vertices => adjacencyList.Keys;

        public WeightedGraph(IEqualityComparer<T>? comparer = null)
        {
            Comparer = comparer ?? EqualityComparer<T>.Default;
            adjacencyList = new Dictionary<T, List<Edge<T>>>(Comparer);
        }

        public void AddVertex(T vertex)
        {
            adjacencyList.TryAdd(vertex, new List<Edge<T>>());
        }

        public void AddEdge(T from, T to, int weight, bool directed = false)
        {
            // 없는 정점은 자동으로 추가
            AddVertex(from);
            AddVertex(to);

            adjacencyList[from].Add(new Edge<T>(from, to, weight));

            // 무방향 그래프
            if (!directed)
            {
                adjacencyList[to].Add(new Edge<T>(from, to, weight));
            }
        }

        public IReadOnlyList<Edge<T>> GetEdges(T vertex)
        {
            if (!adjacencyList.TryGetValue(vertex, out var edges))
            {
                throw new KeyNotFoundException(
                    $"정점 '{vertex}'이 그래프에 없습니다.");
            }

            return edges;
        }

        public bool ContainsVertex(T vertex)
        {
            return adjacencyList.ContainsKey(vertex);
        }
    }
}