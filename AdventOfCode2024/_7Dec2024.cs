using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _7Dec2024
    {
        public static void Part1()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/7Dec2024.txt");
            long result = 0;
            for (int i = 0; i < textLines.Length; i++)
            {
                var line = textLines[i];

                var sumResult = long.Parse(line.Split(": ")[0]);
                var numbers = line.Split(": ")[1].Split(" ").ToList().Select(x => long.Parse(x)).ToList();

                var isValid = IsValid(sumResult, numbers);
                if (isValid) result += sumResult;
            }

            Console.WriteLine("07/12/24 Part 1 - {0}", result);
        }

        private static bool IsValid(long sumResult, List<long> numbers)
        {
            if (numbers.Count == 1) return sumResult == numbers[0];
            var currentNumber = numbers.Last();
            if (sumResult % currentNumber == 0 && IsValid(sumResult / currentNumber, numbers.SkipLast(1).ToList())) return true;
            if (sumResult > currentNumber && IsValid(sumResult - currentNumber, numbers.SkipLast(1).ToList())) return true;
            return false;
        }

        public static void Part2()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/7Dec2024.txt");

            long result = 0;
            for (int i = 0; i < textLines.Length; i++)
            {
                var line = textLines[i];

                var sumResult = long.Parse(line.Split(": ")[0]);
                var numbers = line.Split(": ")[1].Split(" ").ToList().Select(x => long.Parse(x)).ToList();

                var isValid = IsValid2(sumResult, numbers);
                if (isValid) result += sumResult;
            }

            Console.WriteLine("07/12/24 Part 2 - {0}", result);
        }

        private static bool IsValid2(long checkNumber, List<long> numbers)
        {
            var combos = GetCombos(numbers.ToArray(), [" + ", " * ", " | "]);

            foreach(var combo in combos)
            {
                long result = ResolveEquation(combo);
                if(result == checkNumber) return true;
            }
            
            return false;
        }

        private static long ResolveEquation(string combo)
        {
            long result = 0;
            var concatEq = combo.Split(' ');

            for(int c = 0; c < concatEq.Length; c++)
            {
                var eqValue = concatEq[c];

                if(eqValue == "+") result += long.Parse(concatEq[c + 1]);
                else if(eqValue == "*") result *= long.Parse(concatEq[c + 1]);
                else if(eqValue == "|") result = long.Parse(string.Concat(result, concatEq[c + 1]));
                else 
                {
                    result = long.Parse(eqValue);
                    continue;
                }
                c++;
            }

            return result;
        }

        public static List<string> GetCombos(long[] numbers, string[] operators)
        {
            List<string> results = new List<string>();
            GenerateCombos(results, numbers.ToArray(), operators, "", 0);
            return results;
        }
        private static void GenerateCombos(List<string> results, long[] numbers, string[] operators, string current, int index)
        {
            if (index == numbers.Length - 1)
            {
                current += numbers[index]; 
                results.Add(current); 
                return;
            }

            current += numbers[index];          
            for (int i = 0; i < operators.Length; i++)
            {
                GenerateCombos(results, numbers, operators, current + operators[i], index + 1);
            }
        }
    }
}
