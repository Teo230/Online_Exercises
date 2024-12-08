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
            for(int i = 0; i < textLines.Length; i++)
            {
                var line = textLines[i];

                var sumResult = long.Parse(line.Split(": ")[0]);
                var numbers = line.Split(": ")[1].Split(" ").ToList().Select(x => long.Parse(x)).ToList();

                var isValid = IsValid(sumResult, numbers);
                if(isValid) result += sumResult;
            }

            Console.WriteLine("07/12/24 Part 1 - {0}", result);
        }

        private static bool IsValid(long sumResult, List<long> numbers)
        {
            if(numbers.Count == 1) return sumResult == numbers[0];
            var currentNumber = numbers.Last();
            if(sumResult % currentNumber == 0 && IsValid(sumResult / currentNumber, numbers.SkipLast(1).ToList())) return true;
            if(sumResult > currentNumber && IsValid(sumResult - currentNumber, numbers.SkipLast(1).ToList())) return true;
            return false;
        }

        public static void Part2()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/7Dec2024.txt");
            
            long result = 0;
            for(int i = 0; i < textLines.Length; i++)
            {
                var line = textLines[i];

                var sumResult = long.Parse(line.Split(": ")[0]);
                var numbers = line.Split(": ")[1].Split(" ").ToList().Select(x => long.Parse(x)).ToList();

                var isValid = IsValid2(sumResult, numbers);
                if(isValid) result += sumResult;
            }

            Console.WriteLine("07/12/24 Part 2 - {0}", result);
        }

        private static bool IsValid2(long sumResult, List<long> numbers)
        {
            if(numbers.Count == 1) return sumResult == numbers[0];
            var currentNumber = numbers.Last();
            if(sumResult % currentNumber == 0 && IsValid(sumResult / currentNumber, numbers.SkipLast(1).ToList())) return true;
            if(sumResult > currentNumber && IsValid(sumResult - currentNumber, numbers.SkipLast(1).ToList())) return true;
            
            //...

            return false;
        }
    }
}
