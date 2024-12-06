using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _2Dec2024
    {
        public static void Part1()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/2Dec2024.txt");

            int result = 0;
            foreach (var level in stringText)
            {
                var numbers = level.Split(' ').Select(x => int.Parse(x)).ToList();

                var isAsc = numbers.IsAscAndUnique();
                var isDesc = numbers.IsDescAndUnique();

                if (!isAsc && !isDesc) continue;

                // Check distance
                var validDistance = numbers.IsValidDistance();
                if (!validDistance) continue;

                result++;
            }

            Console.WriteLine("02/12/24 Part 1 - {0}", result);
        }

        private static bool IsAscAndUnique(this List<int> numbers)
        {
            int lastNumber = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                var number = numbers[i];
                if (number < lastNumber) return false;
                if (number == lastNumber) return false;
                lastNumber = number;
            }

            return true;
        }

        private static bool IsDescAndUnique(this List<int> numbers)
        {
            int lastNumber = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                var number = numbers[i];
                if (number > lastNumber) return false;
                if (number == lastNumber) return false;
                lastNumber = number;
            }

            return true;
        }

        private static bool IsValidDistance(this List<int> numbers)
        {
            for (int i = 1; i < numbers.Count; i++)
            {
                var number = numbers[i - 1];
                var nextNumber = numbers[i];
                var delta = Math.Abs(number - nextNumber);

                if (1 > delta || delta > 3)
                    return false;
            }

            return true;
        }

        static bool _try = true;
        public static void Part2()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/2Dec2024.txt");

            int result = 0;
            foreach (var level in stringText)
            {
                var numbers = level.Split(' ').Select(x => int.Parse(x)).ToList();

                _try = true;
                var validLevel = ValidLevel(numbers);
                if(!validLevel) continue;

                result++;
            }

            Console.WriteLine("02/12/24 Part 2 - {0}", result);
        }

        private static bool ValidLevel(List<int> numbers)
        {
            var isAsc = numbers.IsAscAndUnique();
            var isDesc = numbers.IsDescAndUnique();

            if (!isAsc && !isDesc)
            {
                if (_try) return TryToFixLevel(numbers);
                else return false;
            }

            // Check distance
            var validDistance = numbers.IsValidDistance();
            if (!validDistance)
            {
                if (_try) return TryToFixLevel(numbers);
                else return false;
            }

            return true;
        }

        private static bool TryToFixLevel(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                List<int> newNumbers = new(numbers);
                newNumbers.RemoveAt(i);
                _try = false;
                var isValid = ValidLevel(newNumbers);
                if (isValid) return true;
            }

            return false;
        }
    }
}
