using LinkedList;

namespace Stack
{
    public class CustomStack
    {
        CustomNode _top;

        public bool isEmpty
        {
            get { return _top == null; }
        }

        public void Push(int data)
        {
            CustomNode newNode = new CustomNode(data);

            if (isEmpty)
            {
                _top = newNode;
            }
            else
            {
                newNode.Next = _top;
                _top = newNode;
            }
        }

        public int Pop()
        {
            if (isEmpty)
                throw new Exception("Stack is empty");

            int tmp = _top.Data;
            _top = _top.Next;
            return tmp;
        }

        public int Peek()
        {
            if (isEmpty)
                throw new Exception("Stack is empty");

            return _top.Data;
        }

        public void Swap()
        {
            if (isEmpty)
                return;//throw new Exception("Stack is empty");

            CustomNode current = _top;

            while (current.Next != null)
            {
                current = current.Next;
            }

            int tmp = _top.Data;
            _top.Data = current.Data;
            current.Data = tmp;
        }

        public void Reverse()
        {
            if (isEmpty)
                return;//throw new Exception("Stack is empty");

            CustomStack stack = new CustomStack();

            while (!isEmpty)
            {
                stack.Push(Pop());
            }

            _top = stack._top;
        }

        public bool Contains(int data)
        {
            if (isEmpty)
                return false;//throw new Exception("Stack is empty");

            CustomNode current = _top;

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
            _top = null;
        }

        public void Print()
        {
            CustomNode current = _top;

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