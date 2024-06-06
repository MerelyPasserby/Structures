using System.Collections;

namespace ClosedHashTable
{
    internal class HashEntry
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public bool Deleted { get; set; }
        public HashEntry(int key, int value) { Key = key; Value = value; Deleted = false; }
        public override string ToString() => $"[{Key} ; {Value}]";
    }
    public class ClosedHashTable
    {
        HashEntry[] _table;
        int _size;
        int _count;
        public ClosedHashTable(int M) { _size = M; _table = new HashEntry[M]; }
        int HashFunction(int key) => key % _size;
        bool isHighCapacity { get { return (double)_count / _size >= 0.75; } }
        public bool Add(int key, int value)
        {
            bool isAlreadyIn = _table.FirstOrDefault(x => x?.Key == key && !x.Deleted) != default ? true : false;
            if (isAlreadyIn) return false;

            //if (isHighCapacity) Rehash();

            int index = HashFunction(key);
            int tmp = key;
            while (_table[index] != null && !_table[index].Deleted)
            {
                index++;
                if (index >= _size) index = 0;
            }
            _table[index] = new HashEntry(key, value);
            _count++;

            if (isHighCapacity) Rehash();
            return true;
        }

        public void Rehash()
        {
            ClosedHashTable tmp = new ClosedHashTable(_size * 2);

            foreach (var entry in _table)
            {
                if (entry == null) continue;
                tmp.Add(entry.Key, entry.Value);
            }

            _table = tmp._table;
            _size = tmp._size;
            _count = tmp._count;
        }

        public bool Remove(int key)
        {
            int index = HashFunction(key);
            int tmp = key;
            for (int i = 0; i < _size; i++)
            {
                if (_table[index] == null) return false;
                if (_table[index].Key == key && !_table[index].Deleted)
                {
                    _table[index].Deleted = true;
                    _count--;
                    return true;
                }
                index++;
                if (index >= _size) index = 0;
            }
            return false;
        }
        public bool Search(int key, out int res)
        {
            int index = HashFunction(key);
            res = 0;
            int tmp = key;
            for (int i = 0; i < _size; i++)
            {
                if (_table[index] == null) return false;
                if (_table[index].Key == key && !_table[index].Deleted)
                {
                    res = _table[index].Value;
                    return true;
                }
                index++;
                if (index >= _size) index = 0;
            }
            return false;
        }
        public void PrintTable()
        {
            Console.WriteLine("HashTable: ");
            for (int i = 0; i < _size; i++)
            {
                Console.Write($"[{i}]: ");
                if (_table[i] != null && !_table[i].Deleted) Console.Write(_table[i]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}