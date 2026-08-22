namespace DataStructure
{
    public static class DataStructure
    {
        public static void Main(string[] args)
        {
            #region [Dynamic Array]
            {
                //DynamicArray<int> dynamicArray = new DynamicArray<int>(10);

                //dynamicArray.Add(1);
                //dynamicArray.Add(2);
                //dynamicArray.Add(3);

                //Console.WriteLine(string.Join(", ", dynamicArray));
            }
            #endregion
            #region [SinglyLinkedList]
            {
                //SinglyLinkedList<int> numbers = new SinglyLinkedList<int>();

                //numbers.AddLast(10);
                //numbers.AddLast(20);
                //numbers.AddFirst(6);

                //Console.WriteLine(string.Join(", ", numbers));
            }
            #endregion
            #region [DoublyLinkedList]
            {
                //DoublyLinkedList<char> doublyLinkedList = new DoublyLinkedList<char>();
                //doublyLinkedList.AddFirst('a');
                //doublyLinkedList.AddFirst('b');
                //doublyLinkedList.AddLast('c');
                //doublyLinkedList.AddLast('d');

                //Console.WriteLine(string.Join(", ", doublyLinkedList));

                //doublyLinkedList.RemoveFirst();
                //Console.WriteLine(string.Join(", ", doublyLinkedList));
            }
            #endregion
            #region [Stack]
            {
                //Console.WriteLine("== Stack ==");
                //Stack<int> stack = new Stack<int>();
                //stack.Push(1);
                //stack.Push(2);
                //stack.Push(3);

                //Console.WriteLine(string.Join(", ", stack));
            }
            #endregion
            #region [Queue]
            {
                //Console.WriteLine("== Queue ==");
                //Queue<char> queue = new Queue<char>(3);

                //queue.Enqueue('a');
                //queue.Enqueue('b');
                //queue.Enqueue('c');

                //Console.WriteLine(string.Join(", ", queue));                
            }
            #endregion
            #region [CircularQueue]
            {
                //CircularQueue<int> circualrQueue = new CircularQueue<int>();
                //circualrQueue.Enqueue(1);
                //circualrQueue.Enqueue(2);
                //circualrQueue.Enqueue(5);

                //Console.WriteLine(string.Join(", ", circualrQueue));
            }
            #endregion
            #region [Deque]
            {
                //Deque<int> deque = new Deque<int>(5);

                //deque.AddFirst(1);
                //deque.AddFirst(2);
                //deque.AddFirst(3);
                //deque.AddLast(4);
                //deque.AddLast(5);

                
                //Console.WriteLine(string.Join(", ", deque));
            }
            #endregion
            #region [HashTable]
            {
                //HashTable<string, int> table = 
                //    new HashTable<string, int>(16, EqualityComparer<string>.Default);

                //table.Put("검", 10);
                //table.Put("방패", 20);
                //table["물약"] = 5;

                //// 기존 키의 값을 수정
                //table["검"] = 15;

                //if (table.TryGetValue("검", out int attack))
                //{
                //    Console.WriteLine(attack); // 15
                //}

                //Console.WriteLine(table.ContainsKey("방패")); // True

                //table.Remove("방패");

                //Console.WriteLine(table.Count); // 2
            }
            #endregion
            #region [BinarySearchTree]
            {
                //BinarySearchTree<int> tree = new();

                //tree.Add(8);
                //tree.Add(3);
                //tree.Add(10);
                //tree.Add(1);
                //tree.Add(6);
                //tree.Add(14);

                //Console.WriteLine(tree.Contains(6));  // True
                //Console.WriteLine(tree.Contains(7));  // False

                //Console.WriteLine(string.Join(", ", tree.InOrder()));
                //// 1, 3, 6, 8, 10, 14

                //Console.WriteLine($"최솟값: {tree.Min()}"); // 1
                //Console.WriteLine($"최댓값: {tree.Max()}"); // 14

                //tree.Remove(3);

                //Console.WriteLine(string.Join(", ", tree.InOrder()));
                //// 1, 6, 8, 10, 14

                //Console.WriteLine(tree.Count); // 5
            }
            #endregion
            #region [BinaryHeap]
            {

            }
            #endregion [MinPriorityQueue]
            #region
            {

            }
            #endregion
            #region [UnionFind]
            {

            }
            #endregion
            #region [Graph]
            {
                var graph = new Graph<string>(false);

                graph.AddEdge("A", "B");
                graph.AddEdge("A", "C");
                graph.AddEdge("B", "D");
                graph.AddEdge("C", "E");

                Console.WriteLine(string.Join(" -> ", graph.BFSOrder("A")));
                Console.WriteLine(string.Join(" -> ", graph.IterativeDFS("A")));
            }
            #endregion

        }
    }
}