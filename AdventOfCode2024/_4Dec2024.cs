using Online_Exercises.AdventOfCode2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _4Dec2024
    {
        public static void Part1()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/4Dec2024.txt");

            var matrix = CreateMatrix(textLines);

            int totalMatch = 0;

            for (int y = 0; y < matrix.Length; y++)
            {
                var row = matrix[y];

                for (int x = 0; x < row.Length; x++)
                {
                    var cell = row[x];
                    if (cell == 'X')
                    {
                        totalMatch += CountMatch(y, x, matrix);
                    }
                }
            }

            Console.WriteLine("1 - Times XMAS appears: {0}", totalMatch);
        }

        // VOGLIO MORIRE
        private static int CountMatch(int positionRow, int positionColumn, char[][] matrix)
        {
            int result = 0;
            const string XMAS = "XMAS";

            var rowLength = matrix.Length;
            var columnLength = matrix[0].Length;

            // Define all 8 possible directions
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dirCount = 0; dirCount < 8; dirCount++)
            {
                string word = "";
                int currentRow = positionRow;
                int currentColumn = positionColumn;

                // Each interaction the gap goes higher because of the sum (ln 65, 66)
                // Interaction for the length of the word to search
                for (int nextChar = 0; nextChar < XMAS.Length; nextChar++)
                {
                    if (currentRow >= 0 && 
                        currentRow < rowLength && 
                        currentColumn >= 0 && 
                        currentColumn < columnLength)
                    {
                        word += matrix[currentRow][currentColumn];
                        currentRow += dy[dirCount];
                        currentColumn += dx[dirCount];
                    }
                    else break;
                }

                if (word == XMAS)
                    result++;
            }

            return result;
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

        public static void Part2()
        {
            var stringText = File.ReadAllText("AdventOfCode2024/4Dec2024.txt");

            Console.WriteLine("2 - Times XMAS appears: {0}", 0);
        }
    }
}
