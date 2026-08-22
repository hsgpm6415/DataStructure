using System.Collections;
using System.Collections.Generic;

namespace DataStructure
{
    public class Queue<T> : IEnumerable<T>
    {
        readonly T[] _items;
        int _rear = 0;
        int _front = 0;

        public int Count    => _rear - _front;
        public int Capacity => _items.Length;
        public bool IsEmpty => _rear == _front;
        public bool IsFull  => _rear == _items.Length;

        public Queue(int initialCapacity)
        {
            if(initialCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            }

            _items = new T[initialCapacity];
        }

        public void Enqueue(T item)
        {
            if(IsFull)
            {
                throw new InvalidOperationException("큐가 가득 찼습니다.");
            }

            _items[_rear++] = item;
        }
        public bool TryEnqueue(T item)
        {
            if(IsFull)
            {                
                return false;
            }

            _items[_rear++] = item;
            return true;
        }
        public T Dequeue()
        {
            if(IsEmpty)
            {
                throw new InvalidOperationException("큐가 비어있습니다.");
            }

            T removedItem = _items[_front];

            _items[_front] = default!;
            _front++;

            if (IsEmpty)
            {
                _front = 0;
                _rear = 0;
            }

            return removedItem;
        }
        public bool TryDequeue(out T? item)
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
            if (IsEmpty)
            {
                throw new InvalidOperationException("큐가 비어있습니다.");
            }


            return _items[_front];
        }

        public bool TryPeek(out T? item)
        {
            if (IsEmpty)
            {
                item = default!;
                return false;
            }

            item = _items[_front];
            return true;
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _items.Length);
            _rear = 0;
            _front = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for(int i = _front; i < _rear; i++)
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
