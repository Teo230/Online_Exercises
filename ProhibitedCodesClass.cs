using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises
{
    internal class ProhibitedCodesClass
    {
        private static (bool valid, List<int> newPageNumbers) CheckPageRules2(List<int> pages, List<string> orderingRules)
        {
            foreach (var rule in orderingRules)
            {
                var lowPageRule = int.Parse(rule.Split('|')[0]);
                var highPageRule = int.Parse(rule.Split('|')[1]);

                var lowIndex = pages.IndexOf(lowPageRule);
                var highIndex = pages.IndexOf(highPageRule);
                if (lowIndex == -1 || highIndex == -1) continue;
                if (lowIndex > highIndex)
                {
                    var permutations = GetPermutations(pages);

                    foreach (var newOrder in permutations)
                    {
                        var isValid = CheckPageRules2(newOrder, orderingRules);
                        if (isValid.valid) return (true, newOrder);
                    }
                }
            }

            return (false, null);
        }

        private static List<List<int>> GetPermutations(List<int> pages)
        {
            _permRes = new List<List<int>>();
            GetPermutations(pages.ToArray(), 0, pages.Count - 1);
            return _permRes;
        }

        private static List<List<int>> _permRes;
        private static void GetPermutations(int[] pages, int loopCounter, int maxLoopCounter)
        {
            if (loopCounter == maxLoopCounter)
                _permRes.Add(pages.ToList());
            for (int i = loopCounter; i <= maxLoopCounter; i++)
            {
                Swap(ref pages[loopCounter], ref pages[i]);
                GetPermutations(pages, loopCounter + 1, maxLoopCounter);
                Swap(ref pages[loopCounter], ref pages[i]);
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }
    }
}
