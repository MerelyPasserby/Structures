namespace AVLTree
{
    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public int Height { get; set; } = 0;
        public TreeNode(int data) { Data = data; Left = null; Right = null; }
        public override string ToString() => $"{Data} ";
    }
    public class AVLTree
    {
        TreeNode _root;
        public TreeNode Root { get { return _root; } }
        public AVLTree() { _root = null; }
        public int GetHeight(TreeNode node) =>
            node == null ? -1 : node.Height;
        public void UpdateHeight(TreeNode node) =>
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        public void Swap(TreeNode a, TreeNode b)
        {
            int tmp = a.Data;
            a.Data = b.Data;
            b.Data = tmp;
        }
        public void RotateRight(TreeNode node)
        {
            Swap(node, node.Left);
            TreeNode tmp = node.Right;
            node.Right = node.Left;
            node.Left = node.Right.Left;
            node.Right.Left = node.Right.Right;
            node.Right.Right = tmp;
            UpdateHeight(node.Right);
            UpdateHeight(node);
        }
        public void RotateLeft(TreeNode node)
        {
            Swap(node, node.Right);
            TreeNode tmp = node.Left;
            node.Left = node.Right;
            node.Right = node.Left.Right;
            node.Left.Right = node.Left.Left;
            node.Left.Left = tmp;
            UpdateHeight(node.Left);
            UpdateHeight(node);
        }
        public int GetBalance(TreeNode node) =>
            node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        public void Balance(TreeNode node)
        {
            int k = GetBalance(node);
            if (k == 2)
            {
                if (GetBalance(node.Left) == -1) RotateLeft(node.Left);
                RotateRight(node);
            }
            else if (k == -2)
            {
                if (GetBalance(node.Right) == 1) RotateRight(node.Right);
                RotateLeft(node);
            }
        }
        public void Insert(TreeNode node, int data)
        {
            if (_root == null) { _root = new TreeNode(data); return; }
            if (data < node.Data)
            {
                if (node.Left == null) node.Left = new TreeNode(data);
                else Insert(node.Left, data);
            }
            else
            {
                if (node.Right == null) node.Right = new TreeNode(data);
                else Insert(node.Right, data);
            }
            UpdateHeight(node);
            Balance(node);
        }
        public TreeNode Search(TreeNode node, int data)
        {
            if (node == null) return null;
            if (node.Data == data) return node;
            return data < node.Data ? Search(node.Left, data) : Search(node.Right, data);
        }
        public TreeNode GetMin(TreeNode node)
        {
            if (node == null) return null;
            if (node.Left == null) return node;
            return GetMin(node.Left);
        }
        public TreeNode GetMax(TreeNode node)
        {
            if (node == null) return null;
            if (node.Right == null) return node;
            return GetMax(node.Right);
        }
        public TreeNode Remove(TreeNode node, int data)
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
                    TreeNode minInRight = GetMin(node.Right);
                    node.Data = minInRight.Data;
                    node.Right = Remove(node.Right, minInRight.Data);

                }
            }
            if (node != null)
            {
                UpdateHeight(node);
                Balance(node);
            }
            return node;
        }
        public void PreOrder(TreeNode node)
        {
            if (node == null) return;
            Console.Write(node + ", ");
            PreOrder(node.Left);
            PreOrder(node.Right);
        }
        public void PostOrder(TreeNode node)
        {
            if (node == null) return;
            PostOrder(node.Left);
            PostOrder(node.Right);
            Console.Write(node + ", ");
        }
        public void InOrder(TreeNode node)
        {
            if (node == null) return;
            InOrder(node.Left);
            Console.Write(node + ", ");
            InOrder(node.Right);
        }
        public void PrintTree(TreeNode node, string name = "Tree", int k = 2, char pos = ' ')
        {
            if (node == _root) Console.WriteLine(name);
            if (node == null) return;
            Console.WriteLine(new string('_', k) + node + pos);
            PrintTree(node.Left, "", k + 4, 'L');
            PrintTree(node.Right, "", k + 4, 'R');
        }
    }
}