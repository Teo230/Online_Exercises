﻿using System.Collections.Specialized;

namespace Online_Exercises.TopInterview150
{
    public class _88_MergeSortedArray
    {
        /// You are given two integer arrays nums1 and nums2, sorted in non-decreasing order,
        /// and two integers m and n, representing the number of elements in nums1 and nums2 respectively.
        /// Merge nums1 and nums2 into a single array sorted in non-decreasing order.

        /// The final sorted array should not be returned by the function, but instead be stored inside the array nums1.
        /// To accommodate this, nums1 has a length of m + n, where the first m elements denote the elements that should be merged,
        /// and the last n elements are set to 0 and should be ignored. nums2 has a length of n.

        /// Example 1:

        /// Input: nums1 = [1, 2, 3, 0, 0, 0], m = 3, nums2 = [2, 5, 6], n = 3
        /// Output: [1, 2, 2, 3, 5, 6]
        ///         Explanation: The arrays we are merging are [1, 2, 3] and [2, 5, 6].
        /// The result of the merge is [1, 2, 2, 3, 5, 6] with the underlined elements coming from nums1.
        /// Example 2:

        /// Input: nums1 = [1], m = 1, nums2 = [], n = 0
        /// Output: [1]
        ///         Explanation: The arrays we are merging are [1] and [].
        /// The result of the merge is [1].
        /// Example 3:

        /// Input: nums1 = [0], m = 0, nums2 = [1], n = 1
        /// Output: [1]
        ///         Explanation: The arrays we are merging are [] and[1].
        /// The result of the merge is [1].
        /// Note that because m = 0, there are no elements in nums1.The 0 is only there to ensure the merge result can fit in nums1.
        ///         Constraints:

        /// nums1.length == m + n
        ///         nums2.length == n
        /// 0 <= m, n <= 200
        /// 1 <= m + n <= 200
        /// -10^9 <= nums1[i], nums2[j] <= 10^9

        private static void Merge(int[] nums1, int count1, int[] nums2, int count2)
        {
            // Check constraints
            if (nums1.Length != count1 + count2) return;
            if (nums2.Length != count2) return;
            if (count1 < 0 || count2 > 200) return;
            if (1 > (count1 + count2) || (count1 + count2) > 200) return;

            if (nums1.Length == 1 && nums2.Length == 1)
            {
                nums1[0] = nums2[0];
                return;
            }

            for (int i = 0; i < nums1.Length; i++)
            {
                // Merge without ordering
                if (i < count1) continue;
                nums1 [i] = nums2[i - count1];
            }

            // Order by asc
            OrderArrayAsc(nums1);

            return;
        }

        private static void OrderArrayAsc(int[] nums1)
        {
            bool ordered = true;
            for (int i = 0; i < nums1.Length - 1; i++)
            {
                var num = nums1[i];
                var nextNum = nums1 [i + 1];
                if(num > nextNum)
                {
                    var tempNum = nextNum;
                    nums1[i + 1] = num;
                    nums1[i] = tempNum;
                    ordered = false;
                }
            }

            if(!ordered) OrderArrayAsc(nums1);
        }

        public static void Test()
        {
            // Test1
            int[] nums1 = [1, 2, 3, 0, 0, 0];
            int[] nums2 = [2, 5, 6];
            Merge(nums1, 3, nums2, 3);
            if (!nums1.SequenceEqual([1, 2, 2, 3, 5, 6])) throw new Exception();

            // Test2
            nums1 = [1];
            nums2 = [0];
            Merge(nums1, 1, nums2, 0);
            if (!nums1.SequenceEqual([1])) throw new Exception();

            // Test3
            nums1 = [0];
            nums2 = [1];
            Merge(nums1, 0, nums2, 1);
            if (!nums1.SequenceEqual([1])) throw new Exception();

            // Test4
            nums1 = [2, 0];
            nums2 = [1];
            Merge(nums1, 1, nums2, 1);
            if (!nums1.SequenceEqual([1,2])) throw new Exception();

            // Test5
            nums1 = [-1, 0, 0, 3, 3, 3, 0, 0, 0];
            nums2 = [1, 2, 2];
            Merge(nums1, 6, nums2, 3);
            if (!nums1.SequenceEqual([-1, 0, 0, 1, 2, 2, 3, 3, 3])) throw new Exception();

            // Test6
            nums1 = [1,0];
            nums2 = [2];
            Merge(nums1, 1, nums2, 1);
            if (!nums1.SequenceEqual([1,2])) throw new Exception();

            Console.WriteLine("Test 88 [✓]");
        }
    }
}
