using System;

namespace DataStructure
{
    public class BinarySearchTree<T> where T: IComparable<T>
    {
        private sealed class Node
        {
            public T Value;
            public Node? Left;
            public Node? Right;

            public Node(T value)
            {
                Value = value;
            }
        }

        Node? _root;
        public int Count { get; private set; }

        public bool Add(T value)
        {
            if(_root == null)
            {
                _root = new Node(value);
                Count++;
                return true;
            }

            Node current = _root;

            while (true)
            {
                int comparison = value.CompareTo(current.Value);
                
                if(comparison < 0)
                {
                    if(current.Left == null)
                    {
                        current.Left = new Node(value);
                        Count++;
                        return true;
                    }
                    current = current.Left;
                }
                else if(comparison > 0)
                {
                    if(current.Right == null)
                    {
                        current.Right = new Node(value);
                        Count++;
                        return true;
                    }
                    current = current.Right;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool Contains(T value)
        {
            if (_root == null) return false;

            Node current = _root;

            while (true)
            {
                int comparison = value.CompareTo(current.Value);

                if (comparison < 0)
                {
                    if(current.Left == null)
                        return false;
                    else
                        current = current.Left;
                }
                else if( comparison > 0)
                {
                    if(current.Right == null)
                        return false;
                    else
                        current = current.Right;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool Remove(T value)
        {
            bool removed = false;

            _root = Remove(_root, value, ref removed);

            if (removed)
            {
                Count--;
            }

            return removed;
        }

        Node? Remove(Node? node, T value, ref bool removed)
        {
            if(node == null) return null;

            int comparison = value.CompareTo(node.Value);

            if(comparison < 0)
            {
                node.Left = Remove(node.Left, value, ref removed);
            }
            else if( comparison > 0)
            {
                node.Right = Remove(node.Right, value, ref removed);
            }
            else
            {
                removed = true;

                if(node.Left == null)
                {
                    return node.Right;
                }

                if( node.Right == null)
                {
                    return node.Left;
                }

                Node successor = FindMinNode(node.Right);
                node.Value = successor.Value;

                node.Right = RemoveMinNode(node.Right);
            }

            return node;
        }

        Node FindMinNode(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        Node? RemoveMinNode(Node node)
        {
            if(node.Left == null)
            {
                return node.Right;
            }

            node.Left = RemoveMinNode(node.Left);
            return node;
        }
        public T Min()
        {
            if (_root == null)
                throw new InvalidOperationException("트리가 비어있습니다.");

            return FindMinNode(_root).Value;
        }

        public T Max()
        {
            if (_root == null)
                throw new InvalidOperationException("트리가 비어있습니다.");

            Node current = _root;

            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Value;
        }

        public IEnumerable<T> InOrder()
        {
            return InOrder(_root);
        }
        public IEnumerable<T> PostOrder()
        {
            return PostOrder(_root);
        }
        public IEnumerable<T> PreOrder()
        {
            return PreOrder(_root);
        }
        IEnumerable<T> InOrder(Node? node)
        {
            if(node == null)
            {
                yield break;
            }

            foreach(T value in InOrder(node.Left))
            {
                yield return value;
            }

            yield return node.Value;

            foreach (T value in InOrder(node.Right))
            {
                yield return value;
            }
        }

        IEnumerable<T> PostOrder(Node? node)
        {
            if(node == null)
            {
                yield break;
            }


            foreach(T value in PostOrder(node.Left))
            {
                yield return value;
            }

            foreach(T value in PostOrder(node.Right))
            {
                yield return value;
            }
            yield return node.Value;
        }

        IEnumerable<T> PreOrder(Node? node)
        {
            if (node == null)
            {
                yield break;
            }
            yield return node.Value;

            foreach (T value in PreOrder(node.Left))
            {
                yield return value;
            }

            foreach (T value in PreOrder(node.Right))
            {
                yield return value;
            }

        }
    }
}
