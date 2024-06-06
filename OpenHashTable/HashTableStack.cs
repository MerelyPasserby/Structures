using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHashTable
{
    public class HashTableStack
    {
        KeyValueNode _top;

        public void Push(int key, int value)
        {
            KeyValueNode newNode = new KeyValueNode(key, value);
            if (_top == null) _top = newNode;
            else
            {
                newNode.Next = _top;
                _top = newNode;
            }
        }
        public int Remove(int key, out bool flag)
        {
            KeyValueNode current = _top, prev = null;
            int res;
            while (current != null && current.Key != key)
            {
                prev = current;
                current = current.Next;
            }
            // Если вышли по условию нахождения ключа
            if (current != null)
            {
                // Если ключ это голова
                if (current == _top)
                {
                    // Если голова одна
                    if (_top.Next == null)
                    {
                        res = _top.Value;
                        _top = null;
                        flag = true;
                        return res;
                    }

                    _top = current.Next;
                    flag = true;
                    return current.Value;
                }
                prev.Next = current.Next;
                flag = true;
                return current.Value;
            }
            // Если вышли по null
            flag = false;
            return 0;
        }
        public int Search(int key, out bool flag)
        {
            KeyValueNode current = _top;
            while (current != null && current.Key != key) current = current.Next;
            // Вышли по ключу
            if (current != null)
            {
                flag = true;
                return current.Value;
            }

            //Вышли по null
            flag = false;
            return 0;
        }
        public void Print()
        {
            KeyValueNode current = _top;
            while (current != null)
            {
                Console.Write(current + ", ");
                current = current.Next;
            }
        }
    }
}
