using System.Text;

namespace Deque
{
    public class Deque
    {
        class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; } = null;
            public Node Prev { get; set; } = null;
            public Node(int value) => Value = value;
            public override string ToString() => Value.ToString();
        }

        Node head = null;
        Node tail = null;

        public Deque() { }
        public bool IsEmpty => head == null;
        public bool IsOnlyOneElement => Count == 1;
        public void Clear()
        {
            head = tail = null;
            Count = 0;
        }
        public int Count { get; private set; }
        public void PushFront(int data)
        {
            Node newNode = new Node(data);

            if (IsEmpty) head = tail = newNode;

            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }

            Count++;
        }
        public int PopFront()
        {
            if (IsEmpty) throw new Exception("No elements in collection");

            int data;

            if (IsOnlyOneElement)
            {
                data = head.Value;
                Clear();
                return data;
            }

            Node tmp = head.Next;
            data = head.Value;

            tmp.Prev = null;
            head.Next = null;
            head = tmp;

            Count--;

            return data;
        }
        public int PeekFront()
        {
            if (IsEmpty) throw new Exception("No elements in collection");

            return head.Value;
        }
        public void PushBack(int data)
        {
            Node newNode = new Node(data);

            if (IsEmpty) head = tail = newNode;

            else
            {
                newNode.Prev = tail;
                tail.Next = newNode;
                tail = newNode;
            }

            Count++;
        }
        public int PopBack()
        {
            if (IsEmpty) throw new Exception("No elements in collection");

            int data;

            if (IsOnlyOneElement)
            {
                data = tail.Value;
                Clear();
                return data;
            }

            Node tmp = tail.Prev;
            data = tail.Value;

            tmp.Next = null;
            tail.Prev = null;
            tail = tmp;

            Count--;

            return data;
        }
        public int PeekBack()
        {
            if (IsEmpty) throw new Exception("No elements in collection");

            return tail.Value;
        }
        public void Swap()
        {
            if (IsEmpty) throw new Exception("No elements in collection");

            (head.Value, tail.Value) = (tail.Value, head.Value);
        }
        public void Reverse()
        {
            if (IsEmpty) throw new Exception("No elements in collection");
            if (IsOnlyOneElement) return;

            Node n1 = head, n2 = tail;

            for (int i = 0; i < Count / 2; i++, n1 = n1.Next, n2 = n2.Prev)
            {
                (n1.Value, n2.Value) = (n2.Value, n1.Value);
            }
        }
        public bool Contains(int data)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Value == data) return true;
                current = current.Next;
            }
            return false;
        }
        public override string ToString()
        {
            Node current = head;
            StringBuilder sb = new StringBuilder();
            sb.Append("Deque: ");

            while (current != null)
            {
                sb.Append(current.Value);
                sb.Append(" ");
                current = current.Next;
            }
            Console.WriteLine();
            Console.WriteLine();

            return sb.ToString();
        }
    }
}