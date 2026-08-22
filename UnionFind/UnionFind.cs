namespace DataStructure
{
    public sealed class UnionFind
    {
        readonly int[] _parent;
        readonly int[] _size;

        public int GroupCount { get; private set; }

        public UnionFind(int count)
        {
            if(count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            _parent = new int[count];
            _size = new int[count];

            for(int i = 0; i < count; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }

            GroupCount = count;
        }

        public int Find(int x)
        {
            ValidateIndex(x);

            if (_parent[x] == x)
            {
                return _parent[x];
            }

            _parent[x] = Find(_parent[x]);

            return _parent[x];
        }

        public bool Union(int a, int b)
        {
            int rootA = Find(a);
            int rootB = Find(b);

            if (rootA == rootB)
            {
                return false;
            }

            if (_size[rootA] < _size[rootB])
            {
                int temp = rootA;
                rootA = rootB;
                rootB = temp;
            }

            _parent[rootB] = rootA;
            _size[rootA] += _size[rootB];

            GroupCount--;

            return true;
        }

        public bool IsConnected(int a, int b)
        {
            return Find(a) == Find(b);
        }

        public int GetGroupSize(int x)
        {
            int root = Find(x);
            return _size[root];
        }


        void ValidateIndex(int index)
        {
            if(index < 0 || index >= _parent.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }
}
