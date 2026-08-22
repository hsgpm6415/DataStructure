using System.Collections;

namespace DataStructure
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        sealed class Node
        {
            public T Value { get; }
            public Node? Next { get; set; }
            public Node? Prev { get; set; }
            public Node(T value)
            {
                Value = value;
            }
        }

        Node? _head;
        Node? _tail;

        public int Count { get; private set; }
        public bool IsEmpty => Count == 0;

        public T First
        {
            get
            {
                if(_head is null)
                {
                    throw new InvalidOperationException("리스트가 비어있습니다.");
                }
                return _head.Value;
            }

        }
        public T Last
        {
            get
            {
                if(_tail is null)
                {
                    throw new InvalidOperationException("리스트가 비어있습니다.");
                }
                return _tail.Value;
            }
        }

        public void AddFirst(T value)
        {
            Node newNode = new Node(value);

            if (_tail is null)
            {
                _tail = newNode;
            }

            if (_head is null)
            {
                _head = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head.Prev = newNode;
                _head = newNode;
            }

            Count++;
        }
        public void AddLast(T value)
        {
            Node newNode = new Node(value);

            if (_head is null)
            {
                _head = newNode;
            }

            if (_tail is null)
            {
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
                _tail = newNode;
            }

            Count++;
        }

        public T RemoveFirst()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("리스트가 비어있습니다.");
            }

            Node removedNode = _head;
            
            if(_head == _tail)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                Node newHead = removedNode.Next!;
                newHead.Prev = null;
                removedNode.Next = null;
                _head = newHead;
            }

            Count--;
            return removedNode.Value;
        }

        public T RemoveLast()
        {
            if (_tail is null)
            {
                throw new InvalidOperationException("리스트가 비어있습니다.");
            }

            Node removedNode = _tail;

            if(_tail == _head)
            {
                _tail = null;
                _head = null;
            }
            else
            {
                Node newTail = removedNode.Prev!;
                newTail.Next = null;
                removedNode.Prev = null;
                _tail = newTail;
            }
                

            Count--;
            return removedNode.Value;
        }

        public bool Remove(T value)
        {
            if (IsEmpty) return false;

            Node? current = _head;

            var comparer = EqualityComparer<T>.Default;


            while (current != null)
            {
                if(comparer.Equals(current.Value, value))
                {
                    if(current.Prev == null)
                    {
                        _head = current.Next;
                    }
                    else
                    {
                        current.Prev.Next = current.Next;
                    }

                    if (current.Next == null)
                    {
                        _tail = current.Prev;
                    }
                    else
                    {
                        current.Next.Prev = current.Prev;
                    }
                    current.Prev = null;
                    current.Next = null;
                    Count--;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public bool Contains(T value)
        {
            if (IsEmpty)
            {
                return false;
            }

            Node? current = _head;

            var comparer = EqualityComparer<T>.Default;


            while (current != null)
            {
                if (comparer.Equals(current.Value, value))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }
        public void Clear()
        {
            if (IsEmpty) return;

            Node? current = _head;

            while (current != null)
            {
                Node? next = current.Next;

                current.Prev = null;
                current.Next = null;

                current = next;
            }

            Count = 0;
            _head = null;
            _tail = null;
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node? current = _head;

            while (current != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
