namespace Heap
{
    public class Heap
    {
        int Size { get; set; } = 100;
        int Capacity { get; set; } = 0;
        int[] Items { get; set; } = new int[100];
        public Heap() { }
        public Heap(int[] array)
        {
            Items = array;
            Capacity = Size = array.Length;
            for (int i = Capacity / 2; i >= 0; i--)
                HeapifyDown(i);
        }
        //get index
        int GetLeftChildIndex(int index) => index * 2 + 1;
        int GetRightChildIndex(int index) => index * 2 + 2;
        int GetParentIndex(int index) => (index - 1) / 2;
        //check index
        bool HasLeftChild(int index) => GetLeftChildIndex(index) < Capacity;
        bool HasRightChild(int index) => GetRightChildIndex(index) < Capacity;
        bool HasParent(int index) => index != 0 ? GetParentIndex(index) >= 0 : false;
        //get value
        int GetLeftChild(int index) => Items[GetLeftChildIndex(index)];
        int GetRightChild(int index) => Items[GetRightChildIndex(index)];
        int GetParent(int index) => Items[GetParentIndex(index)];
        //methods
        void Swap(int index1, int index2)
        {
            int tmp = Items[index1];
            Items[index1] = Items[index2];
            Items[index2] = tmp;
        }
        bool NeedsResize { get => Size == Capacity; }
        void Resize()
        {
            int[] tmp = new int[Size * 2];
            int j = 0;
            foreach (int i in Items)
                tmp[j++] = i;
            Items = tmp;
            Size *= 2;
        }
        public int? Remove()
        {
            if (Capacity == 0) return null;
            int res = Items[0];
            Items[0] = Items[Capacity - 1];
            Capacity--;
            HeapifyDown();
            return res;
        }
        public int? Remove(int value)
        {
            if (Capacity == 0) return null;
            for (int i = 0; i < Capacity; i++)
            {
                if (Items[i] == value)
                {
                    int res = Items[i];
                    Items[i] = Items[Capacity - 1];
                    Capacity--;
                    HeapifyDown(i);
                    return res;
                }
            }
            return null;
        }
        public void Add(int value) //
        {
            if (NeedsResize) Resize();
            Items[Capacity] = value;
            Capacity++;
            HeapifyUp(Capacity - 1); //
        }
        void HeapifyDown(int index = 0) //
        {
            //while (HasLeftChild(index))
            //{
            //    int smallerChildIndex = GetLeftChildIndex(index);
            //    if (HasRightChild(index) && GetRightChild(index) < GetLeftChild(index))
            //        smallerChildIndex = GetRightChildIndex(index);

            //    if (Items[index] < Items[smallerChildIndex])
            //        break;
            //    else
            //    {
            //        Swap(index, smallerChildIndex);
            //        index = smallerChildIndex;
            //    }
            //}

            int l, r, min = index;

            if (HasLeftChild(index))
            {
                l = GetLeftChildIndex(index);
                if (l < Capacity && Items[index] > GetLeftChild(index))
                    min = l;
                else
                    min = index;
            }
            if (HasRightChild(index))
            {
                r = GetRightChildIndex(index);
                if (r < Capacity && Items[min] > GetRightChild(index))
                    min = r;
            }
            if (min != index)
            {
                Swap(index, min);
                HeapifyDown(min);
            }

        }
        void HeapifyUp(int index) //
        {
            //int index = Capacity - 1;
            //while (HasParent(index) && GetParent(index) > Items[index])
            //{
            //    Swap(index, GetParentIndex(index));
            //    index = GetParentIndex(index);
            //}
            if (HasParent(index))
            {
                if (GetParent(index) > Items[index])
                {
                    Swap(index, GetParentIndex(index));
                    HeapifyUp(GetParentIndex(index));
                }
            }

        }
        public void PrintHeap()
        {
            Console.WriteLine("Heap:");
            if (Capacity == 0) return;
            int range = (int)Math.Log2(Capacity) + 1;
            int amount = 0, count = 0;
            for (int i = 0; i < range; i++)
            {
                amount = i == 0 ? 1 : amount * 2;
                Console.Write(new string(' ', 42 - amount * 2));
                for (int j = 0; j < amount && count < Capacity; j++)
                {
                    Console.Write(Items[count++] + "  ");
                }
                Console.WriteLine();
            }
        }
        // new // HeapSort
        public static IEnumerable<int> HeapSort(int[] arr)
        {
            Heap heap = new Heap(arr);
            for (int i = arr.Length - 1; i > 0; i--)
            {
                heap.Swap(0, i);
                heap.Capacity -= 1;
                heap.HeapifyDown();
            }
            arr = heap.Items;
            Array.Reverse(arr);
            return arr;
        }
    }
}