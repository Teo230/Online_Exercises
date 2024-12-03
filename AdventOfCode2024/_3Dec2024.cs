using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _3Dec2024
    {
        public static void Part1()
        {
            var stringText = File.ReadAllText("AdventOfCode2024/3Dec2024.txt");
            var mulPattern = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
            var allValidMul = mulPattern.Matches(stringText).Select(x => x.Value).ToList();

            int result = 0;
            for (int i = 0; i < allValidMul.Count(); i++) 
            {
                var cleanText = allValidMul[i].Replace("mul(","");
                cleanText = cleanText.Replace(")", "");

                var firstNumber = cleanText.Split(',')[0];
                var secondNumber = cleanText.Split(',')[1];
                var mulResult = int.Parse(firstNumber) * int.Parse(secondNumber);

                result += mulResult;
            }

            Console.WriteLine("1 - Total mul result: {0}", result);
        }

       
        public static void Part2()
        {
            var stringText = File.ReadAllText("AdventOfCode2024/3Dec2024.txt");
            var mulPattern = new Regex(@"(mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\))");
            var allValidMul = mulPattern.Matches(stringText).Select(x => x.Value).ToList();

            bool continueMul = true;
            int result = 0;
            for (int i = 0; i < allValidMul.Count(); i++)
            {
                var cleanText = allValidMul[i];

                var isDo = cleanText.Contains("do()");
                if (isDo) 
                {
                    continueMul = true;
                    continue;
                }

                var isDont = cleanText.Contains("don't()");
                if (isDont)
                {
                    continueMul = false;
                    continue;
                }

                if (!continueMul) continue;

                cleanText = allValidMul[i].Replace("mul(", "");
                cleanText = cleanText.Replace(")", "");

                var firstNumber = cleanText.Split(',')[0];
                var secondNumber = cleanText.Split(',')[1];
                var mulResult = int.Parse(firstNumber) * int.Parse(secondNumber);

                result += mulResult;
            }

            Console.WriteLine("2 - Total mul result: {0}", result);
        }
    }
}
