using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenHashTable
{
    public class OpenHashTable
    {
        HashTableStack[] table;
        int size;
        public OpenHashTable(int M) { size = M; table = new HashTableStack[size]; }
        int HashFunction(int key) => key % size;
        public bool Add(int key, int value)
        {
            int index = HashFunction(key);
            if (table[index] == null) table[index] = new HashTableStack();

            bool isAlreadyIn;
            table[index].Search(key, out isAlreadyIn);
            if (!isAlreadyIn)
            {
                table[index].Push(key, value);
                return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            int index = HashFunction(key);

            bool isPresented = false;
            table[index]?.Remove(key, out isPresented);

            if (isPresented) return true;
            return false;
        }
        public bool Search(int key, out int res)
        {
            int index = HashFunction(key);

            bool isPresented = false;
            int? tmp;
            tmp = table[index]?.Search(key, out isPresented);

            if (tmp == null) res = 0;
            else res = tmp.Value;

            if (isPresented) return true;
            return false;
        }

        public void PrintTable()
        {
            Console.WriteLine("HashTable: ");
            int i = 0;
            foreach (var stack in table)
            {
                Console.Write($"[{i++}]: ");
                stack?.Print();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
