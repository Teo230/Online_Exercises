using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _10Dec2024
    {
        public static void Part1()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/10Dec2024.txt");

            var map = CreateMatrix(stringText);

            int result = 0;
            for (int r = 0; r < stringText.Length; r++)
            {
                for (int c = 0; c < stringText.Length; c++)
                {
                    int height = int.Parse(stringText[r][c].ToString());
                    if (height == 0)
                    {
                        if (MakePaths(map, r, c)) result++;
                    }
                }
            }

            Console.WriteLine("10/12/24 Part 1 - {0}", result);
        }

        public static void Part2()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/10Dec2024.txt");


            Console.WriteLine("01/12/24 Part 2 - {0}", 0);
        }

        private static char[][] CreateMatrix(string[] textLines)
        {
            char[][] matrix = new char[textLines.Length][];
            for (int i = 0; i < textLines.Length; i++)
            {
                var textLine = textLines[i];
                var chars = textLine.ToCharArray();
                var row = matrix[i] = new char[chars.Length];

                for (int j = 0; j < chars.Length; j++)
                {
                    row[j] = chars[j];
                }
            }

            return matrix;
        }


        private static bool MakePaths(char[][] map, int r, int c, int currentHeight = 0)
        {
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int d = 0; d < 8; d++)
            {
                var posY = dy[d] + r;
                var posX = dx[d] + c;

                if (posY < 0 || posY >= map.Length) continue;
                if (posX < 0 || posX >= map[0].Length) continue;

                var posValue = int.Parse(map[posY][posX].ToString());

                if (currentHeight + 1 == posValue && posValue <= 9) return true;
                else return MakePaths(map, posY, posX, posValue);
            }

            return false;
        }
    }
}
