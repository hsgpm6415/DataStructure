using System.Collections.Generic;

namespace DataStructure
{
    public class MinPriorityQueue<TElement, TPriority>
    {
        public List<(TElement element, TPriority priority)> _items = new();
        public IComparer<TPriority> _comparer;

        public int Count => _items.Count;

        public MinPriorityQueue(IComparer<TPriority>? comparer = null)
        {
            _comparer = comparer ?? Comparer<TPriority>.Default;
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            _items.Add((element, priority));

            SiftUp(_items.Count - 1);
        }

        public TElement Dequeue()
        {
            if(!TryDequeue(out TElement element, out _ ))
            {
                throw new InvalidOperationException("없어양");
            }

            return element;
        }

        public bool TryDequeue(out TElement element, out TPriority priority)
        {
            if(_items.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }

            var root = _items[0];
            int lastIndex = _items.Count - 1;

            _items[0] = _items[lastIndex];
            _items.RemoveAt(lastIndex);

            if (_items.Count > 0)
                SiftDown(0);

            element = root.element;
            priority = root.priority;
            return true;
        }
        public void Clear()
        {
            _items.Clear();
        }
        void SiftUp(int index)
        {
            while(index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (Compare(parentIndex, index) <= 0)
                    return;

                Swap(index, parentIndex);
                index = parentIndex;
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
                    Compare(rightIndex, smallestIndex) < 0)
                {
                    smallestIndex = rightIndex;
                }

                if (Compare(smallestIndex, index) >= 0)
                    return;

                Swap(smallestIndex, index);
                index = smallestIndex;
            }
        }
        int Compare(int first, int second)
        {
            return _comparer.Compare(_items[first].priority, _items[second].priority);
        }
        void Swap(int first, int second)
        {
            var temp = _items[first];
            _items[first] = _items[second];
            _items[second] = temp;
        }
    }
}
