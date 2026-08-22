using System.Collections.Generic;

namespace DataStructure
{
    public class BinaryHeap<T>
    {
        readonly List<T> _items;
        readonly IComparer<T> _comparer;

        public int Count => _items.Count;

        public BinaryHeap(IEnumerable<T> source, IComparer<T>? comparer = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            _items = new List<T>(source);

            _comparer = comparer ?? Comparer<T>.Default;

            Heapify();
        }

        public void Push(T item)
        {
            _items.Add(item);

            SiftUp(_items.Count - 1);
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new Exception("이진 힙이 비어있습니다.");
            }

            T root = _items[0];

            int lastIndex = _items.Count - 1;
            if(lastIndex == 0)
            {
                _items.RemoveAt(lastIndex);
                
                return root;
            }

            _items[0] = _items[lastIndex];
            _items.RemoveAt(lastIndex);

            SiftDown(0);
            return root;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new Exception("이진 힙이 비어있습니다.");
            }

            return _items[0];
        }

        public bool TryPeek(out T item)
        {
            if(Count == 0)
            {
                item = default!;
                return false;
            }

            item = _items[0];
            return true;
        }

        public bool TryPop(out T item)
        {
            if (Count == 0)
            {
                item = default!;
                return false;
            }

            item = Pop();
            return true;
        }
        void Heapify()
        {
            for (int i = _items.Count / 2 - 1; i >= 0; i--)
            {
                SiftDown(i);
            }
        }

        void SiftDown(int index)
        {
            while(true)
            {
                int leftIndex = index * 2 + 1;
                int rightIndex = index * 2 + 2;

                int smallestIndex = leftIndex;

                if (leftIndex >= _items.Count)
                    return;

                if (rightIndex < _items.Count &&
                   _comparer.Compare(_items[rightIndex], _items[smallestIndex]) < 0)
                {
                    smallestIndex = rightIndex;
                }

                if (_comparer.Compare(_items[smallestIndex], _items[index]) >= 0)
                    return;

                Swap(smallestIndex, index);
                index = smallestIndex;
            }

        }

        void SiftUp(int index)
        {
            while(index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (_comparer.Compare(_items[index], _items[parentIndex]) >= 0) return;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        void Swap(int first, int second)
        {
            (_items[first], _items[second]) = (_items[second], _items[first]);
        }

    }
}
