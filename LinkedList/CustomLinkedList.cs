using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class CustomLinkedList
    {
        CustomNode _head;

        public CustomLinkedList()
        {
            _head = null;
        }

        public void Add(int data)
        {
            CustomNode newNode = new CustomNode(data);
            if (_head == null)
                _head = newNode;
            else
            {
                newNode.Next = _head;
                _head = newNode;
            }
        }

        public void Insert(int data, int pos)
        {
            CustomNode current = _head;
            while (current != null && (pos - 1) != 0)
            {
                current = current.Next;
                pos--;
            }
            if (current == null)
            {
                Console.WriteLine("Impossible");
                return;
            }
            else
            {
                CustomNode newNode = new CustomNode(data),
                    tmp = current.Next;
                current.Next = newNode;
                newNode.Next = tmp;
            }
        }

        public void Move(int pos, int newPos)
        {
            if (newPos + 2 > GetListLength())
            {
                Console.WriteLine("Impossible");
                return;
            }

            CustomNode current = _head, tmp;

            while (current != null && pos > 0)
            {
                current = current.Next;
                pos--;
            }

            if (pos == -1)
            {
                tmp = current;
                _head = tmp.Next;
                current = _head;
            }
            else
            {
                tmp = current.Next;
                current.Next = tmp.Next;
            }

            while (current != null && newPos > 0)
            {
                current = current.Next;
                newPos--;
            }

            tmp.Next = current.Next;
            current.Next = tmp;
        }

        public void Delete(int pos)
        {
            CustomNode current = _head, tmp;

            if (pos == -1)
            {
                _head = current.Next;
                return;
            }
            for (int i = 0; i < pos; i++)
            {
                current = current.Next;
            }
            tmp = current.Next;
            current.Next = tmp.Next;
        }

        public void DeletEls(int pos)
        {
            CustomNode current = _head;
            int n = 1;

            if (pos == 1)
                _head = null;

            while (current != null)
            {
                if ((n + 1) % pos == 0)
                {
                    if (current.Next != null)
                    {
                        current.Next = current.Next.Next;
                        n++;
                    }
                }
                current = current.Next;
                n++;
            }

        }

        public void Sort()
        {
            int[] arr = new int[GetListLength()];
            int i = 0;
            CustomNode current = _head;

            while (current != null)
            {
                arr[i] = current.Data;
                current = current.Next;
                i++;
            }

            int j, key;

            for (j = 1; j < arr.Length; j++)
            {
                key = arr[j];
                i = j - 1;
                while (i >= 0 && arr[i] > key)
                {
                    arr[i + 1] = arr[i];
                    i--;
                }
                arr[i + 1] = key;
            }

            CustomLinkedList newList = new CustomLinkedList();
            foreach (int number in arr)
            {
                newList.Append(number);
            }
            _head = newList._head;
        }


        public CustomLinkedList Copy()
        {
            CustomLinkedList newList = new CustomLinkedList();
            CustomNode current = _head;

            while (current != null)
            {
                newList.Append(current.Data);
                current = current.Next;
            }
            return newList;

        }

        public void Clear()
        {
            _head = null;
        }

        public static void Merge(CustomLinkedList a, CustomLinkedList b)
        {
            CustomNode current = a._head;
            if (current == null)
            {
                Console.WriteLine("Impossible");
                return;
            }
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = b._head;
            b._head = null;
        }

        public static CustomLinkedList Intersection(CustomLinkedList a, CustomLinkedList b)
        {
            CustomLinkedList newList = new CustomLinkedList();
            CustomNode currentA = a._head;
            CustomNode currentB = b._head;

            while (currentB != null)
            {
                while (currentA != null)
                {
                    if (currentA.Data == currentB.Data)
                    {
                        newList.Append(currentA.Data);
                        break;
                    }
                    currentA = currentA.Next;
                }
                currentA = a._head;
                currentB = currentB.Next;
            }
            return newList;

        }

        public void Print()
        {
            CustomNode current = _head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        int GetListLength()
        {
            int res = 0;
            CustomNode current = _head;
            while (current != null)
            {
                current = current.Next;
                res++;
            }
            return res;
        }

        void Append(int data)
        {
            CustomNode newNode = new CustomNode(data),
                current = _head;

            if (_head == null)
                _head = newNode;
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

    }
}
