using Online_Exercises.AdventOfCode2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _5Dec2024
    {
        public static void Part1()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/5Dec2024.txt");

            List<string> orderingRules = new();
            List<string> pages = new();
            foreach (var line in textLines)
            {
                if (line.Contains('|')) orderingRules.Add(line);
                else if (string.IsNullOrWhiteSpace(line)) continue;
                else pages.Add(line);
            }

            int result = 0;
            foreach (var page in pages)
            {
                var pageTexts = page.Split(',');
                var pageNumbers = pageTexts.Select(page => int.Parse(page)).ToList();
                var valid = CheckPageRules(pageNumbers, orderingRules);
                if (!valid) continue;

                var middleIndex = pageNumbers.Count / 2;
                int middlePageNumber = pageNumbers[middleIndex];
                result += middlePageNumber;
            }

            Console.WriteLine("05/12/24 Part 1 - {0}", result);
        }

        private static bool CheckPageRules(List<int> pages, List<string> orderingRules)
        {
            foreach (var rule in orderingRules)
            {
                var lowPageRule = int.Parse(rule.Split('|')[0]);
                var highPageRule = int.Parse(rule.Split('|')[1]);

                var lowIndex = pages.IndexOf(lowPageRule);
                var highIndex = pages.IndexOf(highPageRule);
                if (lowIndex == -1 || highIndex == -1) continue;
                if (lowIndex > highIndex) return false;
            }

            return true;
        }

        public static void Part2()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/5Dec2024.txt");

            List<(int, int)> orderingRules = new();
            List<string> pages = new();
            foreach (var line in textLines)
            {
                if (line.Contains('|'))
                {
                    var firstRule = int.Parse(line.Split('|')[0]);
                    var secondRule = int.Parse(line.Split('|')[1]);
                    orderingRules.Add(new(firstRule, secondRule));
                }
                else if (string.IsNullOrWhiteSpace(line)) continue;
                else pages.Add(line);
            }

            int result = 0;
            foreach (var page in pages)
            {
                var pageTexts = page.Split(',');
                var pageNumbers = pageTexts.Select(page => int.Parse(page)).ToList();
                var checkRes = CheckPageRules2(pageNumbers, orderingRules);
                if (!checkRes.valid) continue;
                pageNumbers = checkRes.newPageNumbers;

                var middleIndex = pageNumbers.Count / 2;
                int middlePageNumber = pageNumbers[middleIndex];
                result += middlePageNumber;
            }

            Console.WriteLine("05/12/24 Part 2 - {0}", result);
        }

        private static (bool valid, List<int> newPageNumbers) CheckPageRules2(List<int> pages, List<(int firstRule, int secondRule)> orderingRules)
        {
            foreach (var rule in orderingRules)
            {
                var lowIndex = pages.IndexOf(rule.firstRule);
                var highIndex = pages.IndexOf(rule.secondRule);
                if (lowIndex == -1 || highIndex == -1) continue;
                if (lowIndex > highIndex)
                {
                    return (true, SortCorrectly(pages, orderingRules));
                }
            }

            return (false, null);
        }

        private static List<int> SortCorrectly(List<int> pages, List<(int firstRule, int secondRule)> orderingRules)
        {
            bool sorted = true;
            for (var i = 0; i < pages.Count - 1; i++)
            {
                var firstPage = pages[i];
                var secondPage = pages[i + 1];

                var exist = orderingRules.Exists(x => x.firstRule == secondPage && x.secondRule == firstPage);
                if (exist)
                {
                    sorted = false;
                    pages = Swap(pages, i, i + 1);
                }
            }

            if (sorted) return pages;
            else return SortCorrectly(pages, orderingRules);
        }

        private static List<int> Swap(List<int> pages, int firstIndex, int secondIndex)
        {
            if (firstIndex == secondIndex) return pages;

            var temp = pages[firstIndex];
            pages[firstIndex] = pages[secondIndex];
            pages[secondIndex] = temp;

            return pages;
        }
    }
}
