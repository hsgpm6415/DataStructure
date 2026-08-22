using System.Collections;

namespace DataStructure
{
    public class Deque<T> : IEnumerable<T>
    {

        T[] _items;
        int _front = 0;

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public Deque(int initialCapacity)
        {
            if(initialCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            }

            _items = new T[initialCapacity];
        }

        public void AddFirst(T item)
        {
            EnsureCapacity();

            _front = (_front - 1 + _items.Length) % _items.Length;
            _items[_front] = item;
            Count++;
        }

        public void AddLast(T item)
        {
            EnsureCapacity();

            int last = (_front + Count) % _items.Length;
            _items[last] = item;
            Count++;
        }

        public T RemoveFirst()
        {
            if(IsEmpty)
            {
                throw new InvalidOperationException("덱이 비어있습니다.");
            }

            T removedItem = _items[_front];
            _items[_front] = default!;

            _front = (_front + 1) % _items.Length;
            Count--;

            if(Count == 0)
            {
                _front = 0;
            }

            return removedItem;
        }
        public T RemoveLast()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("덱이 비어있습니다.");
            }
            int lastIndex = (_front + Count - 1) % _items.Length;

            T removedItem = _items[lastIndex];
            _items[lastIndex] = default!;

            Count--;

            if (Count == 0)
            {
                _front = 0;
            }
            return removedItem;
        }
        public T PeekFirst()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("덱이 비어있습니다.");
            }

            return _items[_front];
        }
        public T PeekLast()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("덱이 비어있습니다.");
            }

            return _items[(_front + Count - 1) % _items.Length];
        }

        void EnsureCapacity()
        {
            if (Count < _items.Length) return;

            T[] newItems = new T[_items.Length * 2];

            for (int i = 0; i < Count; i++)
            {
                newItems[i] = _items[(_front + i) % _items.Length];
            }

            _items = newItems;
            _front = 0;
        }



        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return _items[(_front + i) % _items.Length];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
