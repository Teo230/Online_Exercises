using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.TopInterview150
{
    public class _226_InvertBinaryTree
    {
        // Given the root of a binary tree, invert the tree, and return its root.
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return default;
            if (-100 > root.val || root.val > 100) return default;
            int nodeCount = NodeCount(root);
            if (0 > nodeCount || nodeCount > 100) return default;
            SwitchNodes(root);
            return root;
        }

        private static void SwitchNodes(TreeNode root)
        {
            if (root.left != null) SwitchNodes(root.left);
            if (root.right != null) SwitchNodes(root.right);
            if (root.left == null && root.right == null) return;
            TreeNode tempNode = root.left;
            root.left = root.right;
            root.right = tempNode;
        }

        private static int NodeCount(TreeNode root)
        {
            int result = 1;
            if (root.left != null) result += NodeCount(root.left);
            if (root.right != null) result += NodeCount(root.right);
            return result;
        }

        public static void Test()
        {
            //Test
            TreeNode root = new(4, new(2, new(1), new(3)), new(7, new(6), new(9)));
            InvertTree(root);

            Console.WriteLine("Test 226 passed");
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}
