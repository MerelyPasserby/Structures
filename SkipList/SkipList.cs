namespace SkipList
{
    public class SkipListNode
    {
        public int Value { get; private set; }
        public int Level { get; private set; }
        public SkipListNode[] Next { get; set; }
        public SkipListNode(int value, int level)
        {
            (Value, Level) = (value, level);
            Next = new SkipListNode[level];
        }
        public override string ToString()
        {
            return $"({Value})";
        }
    }
    public class SkipList
    {
        Random random;
        int level;
        public int MaxLevel { get; private set; }
        public int Level => level + 1;
        public int Count { get; private set; }
        public double P { get; private set; }
        public bool IsRandomized { get; private set; }
        SkipListNode head;

        public SkipList(int target, bool isRandomized, bool isConst, double p = 0.5)
        {
            MaxLevel = isConst ? target : (int)(Math.Log(target) / Math.Log(1.0 / p));
            (P, IsRandomized, Count, level) = (p, isRandomized, 0, 0);
            head = new SkipListNode(int.MinValue, MaxLevel);
            random = new Random();
        }

        public int GetRandomLevel() => (int)(random.NextDouble() * MaxLevel);

        public int GetConditionLevel()
        {
            int tmp = (int)(Math.Log(Count + 1) / Math.Log(1.0 / P));

            if (tmp > level) return tmp;

            return 0;
        }

        public void Add(int value)

        {
            SkipListNode[] update = new SkipListNode[MaxLevel];
            SkipListNode current = head;

            for (int i = level; i >= 0; i--)
            {
                while (current.Next[i] != null && current.Next[i].Value < value)
                {
                    current = current.Next[i];
                }
                update[i] = current;
            }

            int newLevel = IsRandomized ? GetRandomLevel() : GetConditionLevel();

            if (newLevel > level)
            {
                for (int i = level + 1; i <= newLevel; i++)
                {
                    update[i] = head;
                }
                level = newLevel;
            }

            SkipListNode newNode = new SkipListNode(value, newLevel + 1);
            for (int i = 0; i <= newLevel; i++)
            {
                newNode.Next[i] = update[i].Next[i];
                update[i].Next[i] = newNode;
            }

            Count++;
        }

        public bool Search(int value)
        {
            SkipListNode current = head;
            for (int i = level; i >= 0; i--)
            {
                while (current.Next[i] != null && current.Next[i].Value < value)
                {
                    current = current.Next[i];
                }
            }
            current = current.Next[0];

            return current != null && current.Value == value;
        }

        public void Remove(int value)
        {
            SkipListNode[] update = new SkipListNode[MaxLevel];
            SkipListNode current = head;

            for (int i = level; i >= 0; i--)
            {
                while (current.Next[i] != null && current.Next[i].Value < value)
                {
                    current = current.Next[i];
                }
                update[i] = current;
            }

            current = current.Next[0];

            if (current != null && current.Value == value)
            {
                for (int i = 0; i <= level; i++)
                {
                    if (update[i].Next[i] != current)
                    {
                        break;
                    }
                    update[i].Next[i] = current.Next[i];
                }

                while (level > 0 && head.Next[level] == null)
                {
                    level--;
                }

                Count--;
            }
        }

        public SkipList Copy()
        {
            SkipList copy = new SkipList(MaxLevel, IsRandomized, true);
            SkipListNode current = head.Next[0];
            while (current != null)
            {
                copy.Add(current.Value);
                current = current.Next[0];
            }
            return copy;
        }

        public void Clear()
        {
            head = new SkipListNode(int.MinValue, MaxLevel);
            Count = level = 0;
        }

        public void PrintList()
        {
            for (int i = MaxLevel - 1; i >= 0; i--)
            {
                SkipListNode current = head.Next[i];
                Console.Write("Level " + (i + 1) + ": ");
                while (current != null)
                {
                    Console.Write(current + "-");
                    current = current.Next[i];
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            SkipListNode current = head.Next[0];
            List<SkipListNode> list = new List<SkipListNode>();

            while (current != null)
            {
                list.Add(current);
                current = current.Next[0];
            }

            List<int> length = list.Select(x => x.ToString().Length).ToList();

            string tmp = "";
            int k = -1;
            List<string> res = new List<string>();

            for (int i = 0; i < MaxLevel; i++)
            {
                tmp += $"Level{i + 1}: ";
                foreach (var el in list)
                {
                    k++;
                    if (el.Level - 1 >= i)
                    {
                        tmp += el.ToString();
                        tmp += '-';
                        continue;
                    }
                    else
                    {
                        tmp += new string('-', length[k] + 1);
                    }

                }
                k = -1;
                tmp += '\n';
                res.Add(tmp);
                tmp = "";
            }

            res.Reverse();
            return res.Aggregate("", (r, el) => r += el);
        }
    }
}