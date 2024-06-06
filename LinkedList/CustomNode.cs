namespace LinkedList
{
    public class CustomNode
    {
        public int Data { get; set; }
        public CustomNode Next { get; set; }
        public CustomNode(int data)
        {
            Data = data;
            Next = null;
        }
    }
}