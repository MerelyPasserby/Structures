namespace RedBlackTree
{
    public enum Color
    {
        Black,
        Red
    }
    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; } = null;
        public TreeNode Right { get; set; } = null;
        public TreeNode Parent { get; set; } = null;
        public Color Color { get; set; } = Color.Black;
        public bool IsExist { get; set; } = false;
        public TreeNode() { }
        public TreeNode(int data)
        {
            Data = data;
            Color = Color.Red;
            IsExist = true;
            Left = new TreeNode();
            Right = new TreeNode();
            Parent = new TreeNode();
        }
        public override string ToString() => $"{Data} ";
    }
    public class RedBlackTree
    {
        TreeNode root;
        public TreeNode Root { get { return root; } }
        public RedBlackTree() => root = new TreeNode();
        void LeftRotate(TreeNode node)
        {
            TreeNode tmp = node.Right;
            node.Right = tmp.Left;

            if (tmp.Left.IsExist)
                tmp.Left.Parent = node;
            tmp.Parent = node.Parent;

            if (!node.Parent.IsExist)
                root = tmp;
            else if (node == node.Parent.Left)
                node.Parent.Left = tmp;
            else node.Parent.Right = tmp;
            tmp.Left = node;
            node.Parent = tmp;
        }
        void RightRotate(TreeNode node)
        {
            TreeNode tmp = node.Left; //
            node.Left = tmp.Right; //

            if (tmp.Right.IsExist) //
                tmp.Right.Parent = node; //
            tmp.Parent = node.Parent;

            if (!node.Parent.IsExist)
                root = tmp;
            else if (node == node.Parent.Left)
                node.Parent.Left = tmp;
            else node.Parent.Right = tmp;
            tmp.Right = node; //
            node.Parent = tmp;
        }
        public void Insert(int data)
        {
            TreeNode tmp1 = root,
                tmp2 = new TreeNode(), node = new TreeNode(data);

            while (tmp1.IsExist)
            {
                tmp2 = tmp1;
                if (node.Data < tmp1.Data)
                    tmp1 = tmp1.Left;
                else
                    tmp1 = tmp1.Right;
            }

            node.Parent = tmp2;
            if (!tmp2.IsExist)
                root = node;
            else if (node.Data < tmp2.Data)
                tmp2.Left = node;
            else
                tmp2.Right = node;

            InsertFix(node);
        }
        void InsertFix(TreeNode node)
        {
            while (node.Parent.Color == Color.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    TreeNode uncle = node.Parent.Parent.Right;
                    if (uncle.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black;
                        uncle.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    TreeNode uncle = node.Parent.Parent.Left;
                    if (uncle.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black;
                        uncle.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            root.Color = Color.Black;
        }
        TreeNode Search(TreeNode node, int data)
        {
            if (node == null || !node.IsExist) return null;
            if (node.Data == data) return node;
            return data < node.Data ? Search(node.Left, data) : Search(node.Right, data);
        }
        TreeNode TreeSuccessor(TreeNode node)
        {
            while (node.Left != null && node.Left.IsExist)
                node = node.Left;
            return node;
        }
        public void Remove(int data)
        {
            TreeNode node = Search(root, data);
            TreeNode tmp1 = new TreeNode(), tmp2 = new TreeNode();
            if (node != null)
            {
                if (!node.Left.IsExist || !node.Right.IsExist)
                    tmp1 = node;
                else
                    tmp1 = TreeSuccessor(node.Right);

                if (tmp1.Left.IsExist)
                    tmp2 = tmp1.Left;
                else
                    tmp2 = tmp1.Right;

                tmp2.Parent = tmp1.Parent;
                if (!tmp1.Parent.IsExist)
                    root = tmp2;
                else if (tmp1 == tmp1.Parent.Left)
                    tmp1.Parent.Left = tmp2;
                else
                    tmp1.Parent.Right = tmp2;

                if (tmp1 != node)
                    node.Data = tmp1.Data;
                if (tmp1.Color == Color.Black)
                    RemoveFix(tmp2);
            }
        }
        void RemoveFix(TreeNode node)
        {
            while (node != root && node.Color == Color.Black)
            {
                if (node == node.Parent.Left)
                {
                    TreeNode brother = node.Parent.Right;
                    if (brother.Color == Color.Red)
                    {
                        brother.Color = Color.Black;
                        node.Parent.Color = Color.Red;
                        LeftRotate(node.Parent);
                        brother = node.Parent.Right;
                    }
                    if (brother.Left.Color == Color.Black && brother.Right.Color == Color.Black)
                    {
                        brother.Color = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (brother.Right.Color == Color.Black)
                        {
                            brother.Color = Color.Red;
                            brother.Left.Color = Color.Black;
                            RightRotate(brother);
                            brother = node.Parent.Right;
                        }
                        brother.Color = node.Parent.Color;
                        node.Parent.Color = Color.Black;
                        brother.Right.Color = Color.Black;
                        LeftRotate(node.Parent);
                        node = root;
                    }
                }
                else
                {
                    TreeNode brother = node.Parent.Left; //
                    if (brother.Color == Color.Red)
                    {
                        brother.Color = Color.Black;
                        node.Parent.Color = Color.Red;
                        RightRotate(node.Parent); //
                        brother = node.Parent.Left; //
                    }
                    if (brother.Left.Color == Color.Black && brother.Right.Color == Color.Black)
                    {
                        brother.Color = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (brother.Left.Color == Color.Black) //
                        {
                            brother.Color = Color.Red;
                            brother.Right.Color = Color.Black; //
                            LeftRotate(brother); //
                            brother = node.Parent.Left; //
                        }
                        brother.Color = node.Parent.Color;
                        node.Parent.Color = Color.Black;
                        brother.Left.Color = Color.Black; //
                        RightRotate(node.Parent); //
                        node = root;
                    }
                }
            }
            node.Color = Color.Black;
        }
        public void PrintTree(TreeNode node, string name = "Tree", int k = 2, char pos = ' ')
        {
            if (node == root) Console.WriteLine(name);
            if (node == null || !node.IsExist) return;

            if (node.Color == Color.Red) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine(new string('_', k) + node + pos);
            PrintTree(node.Left, "", k + 4, 'L');
            PrintTree(node.Right, "", k + 4, 'R');
        }
    }
}