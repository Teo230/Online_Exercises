using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _1Dec2024
    {
        public static void Part1()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/1Dec2024.txt");

            var column1Text = stringText.Select(x => x.Split("   ")[0]).ToList();
            var column2Text = stringText.Select(x => x.Split("   ")[1]).ToList();

            column1Text = column1Text.Order().ToList();
            column2Text = column2Text.Order().ToList();

            int result = 0;
            for (int i = 0; i < column1Text.Count; i++)
            {
                var column1Value = int.Parse(column1Text[i]);
                var column2Value = int.Parse(column2Text[i]);

                var delta = Math.Abs(column1Value - column2Value);
                result += delta;
            }

            Console.WriteLine("01/12/24 Part 1 - {0}", result);
        }

        public static void Part2()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/1Dec2024.txt");

            var column1Text = stringText.Select(x => x.Split("   ")[0]).ToList();
            var column2Text = stringText.Select(x => x.Split("   ")[1]).ToList();

            List<int> column1Values = column1Text.Order().Select(x => int.Parse(x)).ToList();
            List<int> column2Values = column2Text.Order().Select(x => int.Parse(x)).ToList();

            int result = 0;
            for (int i = 0; i < column1Values.Count; i++)
            {
                var column1Value = column1Values[i];

                var value2Count = column2Values.Where(value => value == column1Value).Count();

                result = result + (column1Value * value2Count);
            }

            Console.WriteLine("01/12/24 Part 2 - {0}", result);
        }
    }
}
