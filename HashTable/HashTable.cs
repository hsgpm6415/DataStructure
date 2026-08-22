using System;
using System.Collections.Generic;

namespace DataStructure
{
    public class HashTable<TKey, TValue> where TKey : notnull
    {
        sealed class Entry
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Entry? Next { get; set; } 

            public Entry(TKey key, TValue value, Entry? next)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }

        Entry?[] _buckets;
        EqualityComparer<TKey> _comparer;
        const float LoadFactor = 0.75f;
        public int Count { get; private set; }
        
        public HashTable(int initialCapacity, EqualityComparer<TKey>? comparer = null)
        {
            if(initialCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            }

            _buckets = new Entry[initialCapacity];
            _comparer = comparer ?? EqualityComparer<TKey>.Default;
        }

        public TValue this[TKey key]
        {
            get
            {
                if(TryGetValue(key, out var value))
                {
                    return value;
                }
                else
                {
                    throw new KeyNotFoundException($"{key}를 찾을 수 없습니다.");
                }
            }
            set
            {
                Put(key, value);
            }
        }
        public void Put(TKey key, TValue value)
        {
            int index = GetBucketIndex(key, _buckets.Length);

            Entry? current = _buckets[index];

            while (current != null)
            {
                if(_comparer.Equals(current.Key, key))
                {
                    current.Value = value;
                    return;
                }
                current = current.Next;
            }

            if(Count + 1 > _buckets.Length * LoadFactor)
            {
                Resize();
                index = GetBucketIndex(key, _buckets.Length);
            }
            _buckets[index] = new Entry(key, value, _buckets[index]);
            Count++;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = GetBucketIndex(key, _buckets.Length);

            Entry? current = _buckets[index];

            while(current != null)
            {
                if(_comparer.Equals(current.Key, key))
                {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
            }

            value = default!;
            return false;
        }
        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out _);
        }
        public bool Remove(TKey key)
        {
            int index = GetBucketIndex(key, _buckets.Length);
            Entry? current = _buckets[index];
            Entry? previous = null;

            while(current != null)
            {
                if(_comparer.Equals(current.Key, key))
                {
                    if (previous == null)
                    {
                        _buckets[index] = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    Count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }

            return false;
        }
        int GetBucketIndex(TKey key, int bucketCount)
        {
            int hashCode = _comparer.GetHashCode(key);

            int positiveIndex = hashCode & 0x7fffffff;

            return positiveIndex % bucketCount;
        }

        void Resize()
        {
            Entry?[] oldBuckets = _buckets;
            _buckets = new Entry[_buckets.Length * 2];

            foreach( Entry? entry in oldBuckets )
            {
                Entry? current = entry;

                while( current != null )
                {
                    Entry? next = current.Next;

                    int newIndex = GetBucketIndex(current.Key, _buckets.Length);

                    current.Next = _buckets[newIndex];
                    _buckets[newIndex] = current;

                    current = next;
                }
            }
        }
    }
}
