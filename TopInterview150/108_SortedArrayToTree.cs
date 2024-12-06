using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.TopInterview150
{
    public class _108_SortedArrayToTree
    {
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            // Get middle position
            int middleIndex = 0;
            var isEven = nums.Length % 2 == 0;
            if (isEven)
                middleIndex = nums.Length / 2;
            else
                middleIndex = (nums.Length - 1) / 2;

            var middleNum = nums[middleIndex];

            var leftNums = nums[..(middleIndex)];
            var rightNums = nums[(middleIndex + 1)..];

            var newTree = new TreeNode(middleNum);
            if (leftNums.Any()) newTree.left = SortedArrayToBST(leftNums);
            if (rightNums.Any()) newTree.right = SortedArrayToBST(rightNums);

            return newTree;
        }

        public static void Test()
        {
            // Test1
            var tree = SortedArrayToBST([-10, -3, 0, 5, 9]);

            // Test2
            tree = SortedArrayToBST([1, 3]);

            Console.WriteLine("Test 226 [✓]");
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
