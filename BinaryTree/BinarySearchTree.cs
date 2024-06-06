namespace BinarySearchTree
{
    public class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node(int data) { Data = data; Left = null; Right = null; }
        public override string ToString() => $"{Data} ";
    }
    public class BinarySearchTree
    {
        Node _root;
        public Node Root { get { return _root; } }
        public BinarySearchTree() { _root = null; }
        public void Insert(Node node, int data)
        {
            if (_root == null) { _root = new Node(data); return; }
            if (data < node.Data)
            {
                if (node.Left == null) node.Left = new Node(data);
                else Insert(node.Left, data);
            }
            else
            {
                if (node.Right == null) node.Right = new Node(data);
                else Insert(node.Right, data);
            }
        }
        public Node Search(Node node, int data)
        {
            if (node == null) return null;
            if (node.Data == data) return node;
            return data < node.Data ? Search(node.Left, data) : Search(node.Right, data);
        }
        public Node GetMin(Node node)
        {
            if (node == null) return null;
            if (node.Left == null) return node;
            return GetMin(node.Left);
        }
        public Node GetMax(Node node)
        {
            if (node == null) return null;
            if (node.Right == null) return node;
            return GetMax(node.Right);
        }
        public Node Remove(Node node, int data)
        {
            if (node == null) return null;
            if (data < node.Data) node.Left = Remove(node.Left, data);
            else if (data > node.Data) node.Right = Remove(node.Right, data);
            else
            {
                if (node.Left == null || node.Right == null)
                    if (node == _root)
                        node = _root = node.Left == null ? node.Right : node.Left;
                    else
                        node = node.Left == null ? node.Right : node.Left;
                else
                {
                    Node minInRight = GetMin(node.Right);
                    node.Data = minInRight.Data;
                    node.Right = Remove(node.Right, minInRight.Data);

                }
            }
            return node;
        }
        public void PreOrder(Node node)
        {
            if (node == null) return;
            Console.Write(node + ", ");
            PreOrder(node.Left);
            PreOrder(node.Right);
        }
        public void PostOrder(Node node)
        {
            if (node == null) return;
            PostOrder(node.Left);
            PostOrder(node.Right);
            Console.Write(node + ", ");
        }
        public void InOrder(Node node)
        {
            if (node == null) return;
            InOrder(node.Left);
            Console.Write(node + ", ");
            InOrder(node.Right);
        }
        public void PrintTree(Node node, int k = 2, char pos = ' ')
        {
            if (node == _root) Console.WriteLine("Tree:");
            if (node == null) return;
            Console.WriteLine(new string('_', k) + node + pos);
            PrintTree(node.Left, k + 4, 'L');
            PrintTree(node.Right, k + 4, 'R');
        }
    }
}