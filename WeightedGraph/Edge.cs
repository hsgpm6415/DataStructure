using System;
using System.Collections.Generic;

namespace DataStructure
{
    public class Edge<T> where T : notnull
    {
        public T From { get; private set; }
        public T To { get; private set; }
        public long Weight { get; private set; }

        public Edge(T from, T to, long weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
