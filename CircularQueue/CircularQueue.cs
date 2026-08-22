using System;
using System.Collections;

namespace DataStructure
{
    public class CircularQueue<T> : IEnumerable<T>
    {
        T[] _items;
        int _front = 0;
        int _rear = 0;
        int _count = 0;
        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public CircularQueue(int initialCapacity = 4)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            }

            _items = new T[initialCapacity];
        }
        public void Enqueue(T item)
        {
            Resize();

            _items[_rear] = item;
            _rear = (_rear + 1) % _items.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("큐가 비어있습니다.");
            }

            T removedItem = _items[_front];

            _items[_front] = default!;

            _front = (_front + 1) % _items.Length;
            _count--;

            return removedItem!;
        }

        public bool TryDequeue(out T item)
        {
            if (IsEmpty)
            {
                item = default!;
                return false;
            }

            item = Dequeue();
            return true;
        }

        public T Peek()
        {
            if(IsEmpty)
            {
                throw new InvalidOperationException("큐가 비어있습니다.");
            }

            return _items[_front];
        }
        public bool TryPeek(out T item)
        {
            if (IsEmpty)
            {
                item = default!;
                return false;
            }

            item = Peek();
            return true;
        }
        
        public void Clear()
        {
            Array.Clear(_items, 0, _items.Length);
            _count = 0;
            _front = 0;
            _rear = 0;
        }
        void Resize()
        {
            if (_count < _items.Length) return;

            int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;

            T[] newItems = new T[newCapacity];

            for (int i = 0; i < _count; i++)
            {
                newItems[i] = _items[(_front + i) % _items.Length];
            }

            _items = newItems;
            _front = 0;
            _rear = _count;
        }


        public IEnumerator<T> GetEnumerator()
        {
            int index = _front;

            for (int i = 0; i < _count; i++)
            {
                yield return _items[index];

                index = (index + 1) % _items.Length;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
