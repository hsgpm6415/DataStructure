using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        T[] _items;
        int _count;

        public int Count    => _count;
        public int Capacity => _items.Length;
        public bool IsEmpty => _count == 0;

        public DynamicArray(int initialCapacity = 4)
        {
            if(initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));

            _items = new T[initialCapacity];
            _count = 0;
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return _items[index];
            }

            set
            {
                ValidateIndex(index);
                _items[index] = value;
            }
        }
        public void Add(T item)
        {
            EnsureCapacity();

            _items[_count++] = item;
        }
        public T RemoveLast()
        {
            if(_count == 0)
            {
                throw new InvalidOperationException("Dynamic Array가 비어있습니다.");
            }

            T removedItem = _items[_count - 1];

            _items[_count - 1] = default!;

            _count--;

            return removedItem;
        }
        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            EnsureCapacity();

            for (int i = _count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            _items[index] = item;
            _count++;
        }
        public T RemoveAt(int index)
        {
            ValidateIndex(index);

            T removedItem = _items[index];

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
            _items[_count] = default!;
            return removedItem;
        }
        public void Clear()
        {
            Array.Clear(_items, 0, _count);
            _count = 0;
        }
        void EnsureCapacity()
        {
            if (_count < _items.Length) return;

            int newCapacity = _items.Length == 0 ? 1 : _items.Length * 2;

            T[] newItems = new T[newCapacity];

            Array.Copy(_items, newItems, _count);

            _items = newItems;
        }
        void ValidateIndex(int index)
        {
            if(index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
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
