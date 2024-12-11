using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _9Dec2024
    {
        public static void Part1()
        {
            var stringText = File.ReadAllText("AdventOfCode2024/9Dec2024.txt");

            var defragmentedString = Defragment(stringText);

            Console.WriteLine("Defragmented String");
            //Console.WriteLine(defragmentedString);

            var compactDisk = ZipBlocks(defragmentedString);

            Console.WriteLine("\nCompact Disk");
            //Console.WriteLine(compactDisk);

            var checksum = CalculateChecksum(compactDisk);
            Console.WriteLine("\nChecksum"); // TODO - Something wrong > Make it more performat Swap-Block
            Console.WriteLine(checksum);
        }

        public static void Part2()
        {
            var stringText = File.ReadAllText("AdventOfCode2024/9Dec2024.txt");



            //Console.WriteLine("01/12/24 Part 2 - {0}", 0);
        }

        private static string Defragment(string stringText)
        {
            var digits = stringText.ToCharArray();

            string defragmentString = "";
            int id = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                var isFile = i % 2 == 0;

                var times = int.Parse(digits[i].ToString());
                for (var t = 0; t < times; t++)
                {
                    if (isFile) defragmentString += id;
                    else defragmentString += ".";
                }

                if (isFile) id++;
            }

            return defragmentString;
        }

        private static string ZipBlocks(string disk)
        {
            var regex = new Regex("\\.");
            var totalFreeSpace = regex.Count(disk);
            var leftText = disk.TrimEnd('.');
            double totalZip = 0.0;

            while (true)
            {
                leftText = disk.TrimEnd('.');
                double blockSpace = disk.Length - totalFreeSpace;
                if (blockSpace == leftText.Length) return disk;

                var newTotalZip = (blockSpace / leftText.Length) *100;
                if (totalZip != newTotalZip) Console.WriteLine($"{totalZip:#.0}%");
                totalZip = newTotalZip;

                var lastDigit = disk.Last(x => x != '.');
                var lastDigitIndex = disk.LastIndexOf(lastDigit);

                var firstFreeSpace = disk.First(x => x == '.');
                var firstDigitIndex = disk.IndexOf(firstFreeSpace);

                disk = disk.Swap(firstDigitIndex, lastDigitIndex);
            }
        }

        private static long CalculateChecksum(string compactDisk)
        {
            compactDisk = compactDisk.Replace(".","");
            var digits = compactDisk.ToCharArray();

            long result = 0;
            for (var i = 0; i < digits.Length; i++)
            {
                var blockSize = int.Parse(digits[i].ToString());
                long checkSumDigit = i * blockSize;
                result += checkSumDigit;
            }

            return result;
        }

        public static string Swap(this string input, int index1, int index2)
        {
            if (index1 == index2 || 
                index1 < 0 || 
                index2 < 0 || 
                index1 >= input.Length || 
                index2 >= input.Length)
                return input;

            var charArray = input.Select((c, i) => i == index1 ? input[index2] : i == index2 ? input[index1] : c).ToArray();
            return new string(charArray);
        }
    }
}
