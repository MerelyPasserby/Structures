namespace Treap
{
    public class TreeNode
    {
        public int Key { get; set; }
        public int Priority { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public TreeNode(int k, int pr) { Key = k; Priority = pr; Left = null; Right = null; }
        public override string ToString() => $"({Key} : {Priority}) ";
    }
    public class Treap
    {
        TreeNode root;
        public TreeNode Root { get => root; }
        public Treap() { }
        TreeNode[] Split(TreeNode node, int key)
        {
            TreeNode[] group;
            if (node == null) return new TreeNode[] { null, null };
            else if (key > node.Key)
            {
                group = Split(node.Right, key);
                node.Right = group[0];
                return new TreeNode[] { node, group[1] };
            }
            else
            {
                group = Split(node.Left, key);
                node.Left = group[1];
                return new TreeNode[] { group[0], node };
            }
        }
        TreeNode Merge(TreeNode left, TreeNode right)
        {
            if (right == null) return left;
            if (left == null) return right;
            else if (left.Priority > right.Priority)
            {
                left.Right = Merge(left.Right, right);
                return left;
            }
            else
            {
                right.Left = Merge(left, right.Left);
                return right;
            }
        }
        public void Add(int k, int pr)
        {
            TreeNode newNode = new TreeNode(k, pr);
            TreeNode[] group = Split(root, k);
            root = Merge(Merge(group[0], newNode), group[1]);
        }
        TreeNode GetMinParent(TreeNode node)
        {
            if (node == null) return null;
            if (node.Left == null || node.Left.Left == null) return node;
            return GetMinParent(node.Left);
        }
        public void Remove(int k)
        {
            TreeNode[] group = Split(root, k);
            TreeNode? tmp = GetMinParent(group[1]);
            if (tmp == null)
                root = Merge(group[0], group[1]);
            else if (tmp.Left?.Key == k)
            {
                TreeNode node = tmp.Left;
                tmp.Left = tmp.Left.Right;
                node.Right = null;
                root = Merge(group[0], group[1]);
            }
            else if (tmp.Left == null && tmp.Key == k)
            {
                TreeNode node = tmp.Right;
                tmp.Right = null;
                root = Merge(group[0], node);
            }
        }
        public void PrintTree(TreeNode node, int k = 2, char pos = ' ')
        {
            if (node == root) Console.WriteLine("Treap:");
            if (node == null) return;
            Console.WriteLine(new string('_', k) + node + pos);
            PrintTree(node.Left, k + 4, 'L');
            PrintTree(node.Right, k + 4, 'R');
        }
    }
}