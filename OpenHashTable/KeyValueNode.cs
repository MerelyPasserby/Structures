namespace OpenHashTable
{
    public class KeyValueNode
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public KeyValueNode Next { get; set; }
        public KeyValueNode(int key, int value) { Key = key; Value = value; }
        public override string ToString() => $"[{Key} ; {Value}]";
    }
}