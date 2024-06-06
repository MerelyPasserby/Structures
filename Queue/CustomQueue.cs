using Stack;
using LinkedList;

namespace Queue
{
    public class CustomQueue
    {
        CustomNode _head;
        CustomNode _tail;

        public bool isEmpty
        {
            get { return _head == null; }
        }

        public void Enqueue(int data)
        {
            CustomNode newNode = new CustomNode(data);

            if (_head == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
        }

        public int Dequeue()
        {
            if (isEmpty)
                throw new Exception("Queue is empty");

            int tmp = _head.Data;
            _head = _head.Next;

            if (_head == null)
                _tail = null;

            return tmp;
        }

        public int Peek()
        {
            if (isEmpty)
                throw new Exception("Queue is empty");

            return _head.Data;
        }

        public void Swap()
        {
            if (isEmpty)
                return;//throw new Exception("Queue is empty");        

            int tmp = _head.Data;
            _head.Data = _tail.Data;
            _tail.Data = tmp;
        }

        public void Reverse()
        {
            if (isEmpty)
                return;//throw new Exception("Queue is empty");

            CustomStack stack = new CustomStack();

            while (!isEmpty)
            {
                stack.Push(Dequeue());
            }
            while (!stack.isEmpty)
            {
                Enqueue(stack.Pop());
            }
        }

        public bool Contains(int data)
        {
            if (isEmpty)
                return false;//throw new Exception("Queue is empty");

            CustomNode current = _head;
            while (current != null)
            {
                if (current.Data == data)
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void Delete()
        {
            _head = _tail = null;
        }

        public void Print()
        {
            CustomNode current = _head;

            while (current != null)
            {
                Console.Write(" " + current.Data);
                current = current.Next;
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}