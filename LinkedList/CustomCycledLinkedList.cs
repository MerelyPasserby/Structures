using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class CustomCycledLinkedList
    {
        CustomNode _head;
        CustomNode _tail;

        public CustomCycledLinkedList()
        {
            _head = null;
            _tail = null;
        }

        public void Append(int data)
        {
            CustomNode newNode = new CustomNode(data),
                current = _head;

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
                newNode.Next = _head;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
                newNode.Next = _head;
            }
        }

        public void Remove(int pos)
        {
            CustomNode current = _head;
            if (current == null)
            {
                Console.WriteLine("Impossible");
            }

            if (pos == -1)
            {
                if (_head == _tail)
                {
                    _head = _tail = null;
                    return;
                }
                _head = _head.Next;
                _tail.Next = _head;
                return;
            }
            while (current != null && pos > 0)
            {
                if (current == _tail)
                {
                    break;
                }
                current = current.Next;
                pos--;
            }
            if (current.Next == _tail && pos == 0)
            {
                current.Next = _head;
                _tail = current;

            }
            current.Next = current.Next.Next;
        }

        //        Задача Джозефуса: n воїнів з одного війська вбивають кожного m-го другого.
        //Необхідно визначити номер k початкової позиції воїна, який повинен бути
        //залишитися останнім.
        public static void Task(CustomCycledLinkedList a, CustomCycledLinkedList b, int m)
        {

            CustomNode current1 = a._head;
            CustomNode current2 = b._head;
            CustomNode prev = null;

            if (m == 1)
                prev = b._tail;

            while (b._head.Next != b._head)
            {
                for (int i = 0; i < m - 1; i++)
                {
                    prev = current2;
                    current2 = current2.Next;
                }

                Console.WriteLine($"{current1.Data} -> {current2.Data}");
                if (current2 == b._head)
                    prev.Next = b._head = prev.Next.Next;
                else
                    prev.Next = prev.Next.Next;

                current1 = current1.Next;
                current2 = prev.Next;
            }

            Console.WriteLine("Second list survivors:");
            b.Print();

            b._head = b._tail = null;
        }

        public void Print()
        {
            CustomNode current = _head;
            if (current != null)
                do
                {
                    Console.Write(current.Data + " ");
                    if (current == _tail)
                        break;
                    current = current.Next;
                } while (current != _head);

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
