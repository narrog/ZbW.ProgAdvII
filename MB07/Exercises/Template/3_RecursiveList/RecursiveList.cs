using System;
using System.Collections.Generic;

namespace _3_RecursiveList {
    public class RecursiveList<T> {
        public class Node {
            public Node Next { get; set; }
            public T Value { get; private set; }

            public Node(T val) {
                Value = val;
            }
        }

        private Node root;
        private Node tail;

        public RecursiveList() {
            root = new Node(default(T));
            tail = root;
        }
        public IEnumerable<T> Traverse
        {
            get
            {
                yield return root.Value;
                var currentNode = root;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                    yield return currentNode.Value;
                }
            }
        }
        //public IEnumerable<T> Inverse
        //{
        //    get { 
        //        yield return root.Value;
        //    }
        //}

        public void Append(T val) {
            tail.Next = new Node(val);
            tail = tail.Next;
        }
    }
}