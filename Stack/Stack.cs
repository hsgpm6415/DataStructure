using System.Collections;

namespace DataStructure
{
    public class Stack<T> : IEnumerable<T>
    {
        T[] _items;
        int _count;

        public int Count => _count;
        public int Capacity => _items.Length;
        public bool IsEmpty => _count == 0;

        public Stack(int initialCapacity = 4)
        {
            if(initialCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            }

            _items = new T[initialCapacity];
        }

        public void Push(T item)
        {
            Resize();

            _items[_count++] = item;
        }
        public T Pop()
        {
            if(_count == 0)
            {
                throw new InvalidOperationException("스택이 비어있습니다.");
            }

            T removedItem = _items[_count - 1];

            _items[_count - 1] = default!;

            _count--;

            return removedItem;
        }
        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("스택이 비어있습니다.");
            }

            return _items[_count - 1];
        }
        public void Clear()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                _items[i] = default!;
            }

            // = Array.Clear(_items, 0, _count);

            _count = 0;
        }
        void Resize()
        {
            if (_count != _items.Length) return;

            T[] newItems = new T[_items.Length * 2];

            Array.Copy(_items, newItems, _count);

            _items = newItems;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
