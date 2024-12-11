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
        // TODO - Not working, example good, data no
        // Last failed checksum - 90786863565
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
            Console.WriteLine("\nChecksum");
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
            var leftText = disk.TrimEnd('.');

            while (true)
            {
                var diskSpaces = disk.ToCharArray();
                leftText = disk.TrimEnd('.');
                if (!leftText.Contains('.')) return disk;

                var lastDigit = disk.Last(x => x != '.');
                var lastDigitIndex = disk.LastIndexOf(lastDigit);

                var firstFreeSpace = disk.First(x => x == '.');
                var firstDigitIndex = disk.IndexOf(firstFreeSpace);

                diskSpaces[firstDigitIndex] = lastDigit;
                diskSpaces[lastDigitIndex] = firstFreeSpace;

                disk = new string(diskSpaces);
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
    }
}
