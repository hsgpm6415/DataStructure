using System;
using System.Collections;
using System.Collections.Generic;


namespace DataStructure
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        sealed class Node
        {
            public T Value { get; }
            public Node? Next { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        Node? _head;
        Node? _tail;

        int _count;
        public int Count => _count;
        public bool IsEmpty => _count == 0;

        public void AddFirst(T value)
        {
            Node newNode = new Node(value);

            if(_head == null)
            {
                _tail = newNode;
            }
            else
            {
                newNode.Next = _head;
            }
            _head = newNode;
            _count++;
        }
        public void AddLast(T value)
        {
            Node newNode = new Node(value);

            if (_tail == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }

            _count++;
        }
        public T PeekFirst()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("연결 리스트가 비어있습니다.");
            }

            return _head.Value;
        }
        public T RemoveFirst()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("연결 리스트가 비어있습니다.");
            }

            Node removedNode = _head;

            _head = _head.Next;

            _count--;

            if(_head == null)
            {
                _tail = null;
            }

            return removedNode.Value;
        }
        public bool Remove(T value)
        {            
            Node? currentNode = _head;
            Node? prevNode = null;

            EqualityComparer<T> comparer =EqualityComparer<T>.Default;

            while (currentNode != null)
            {
                if(comparer.Equals(currentNode.Value, value))
                {
                    if (prevNode == null)
                    {
                        _head = currentNode.Next;
                    }
                    else
                    {
                        prevNode.Next = currentNode.Next;
                    }

                    if(currentNode == _tail)
                    {
                        _tail = prevNode;
                    }
                    _count--;
                    return true;
                }
                prevNode = currentNode;
                currentNode = currentNode.Next;
            }
            
            return false;
        }
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
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
