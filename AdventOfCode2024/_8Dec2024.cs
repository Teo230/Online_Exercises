using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _8Dec2024
    {
        public static void Part1()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/8Dec2024.txt");

            var matrix = CreateMatrix(stringText);
            //PrintMatrix(matrix);

            Dictionary<char, List<(int posY, int posX)>> nodes = GetNodes(matrix);
            var result = CalculateAntinodes(matrix, nodes);

            Console.WriteLine("08/12/24 Part 1 - {0}", result);
        }

        private static long CalculateAntinodes(char[][] matrix, Dictionary<char, List<(int posY, int posX)>> nodes)
        {
            foreach (var node in nodes)
            {
                var combinations = GenerateCombos(node.Value);

                foreach (var combination in combinations)
                {
                    var point1 = combination.point1;
                    var point2 = combination.point2;

                    var antiNode1 = CalculateAntinodePosition(point1, point2);
                    if (antiNode1.posY >= 0 && antiNode1.posX >= 0 && antiNode1.posY < matrix.Length && antiNode1.posX < matrix[0].Length)
                        matrix[antiNode1.posY][antiNode1.posX] = '#';

                    var antiNode2 = CalculateAntinodePosition(point2, point1);
                    if (antiNode2.posY >= 0 && antiNode2.posX >= 0 && antiNode2.posY < matrix.Length && antiNode2.posX < matrix[0].Length)
                        matrix[antiNode2.posY][antiNode2.posX] = '#';

                    //PrintMatrix(matrix);
                }
            }

            return CountMarkedPlace(matrix);
        }

        public static void Part2()
        {
            var stringText = File.ReadAllLines("AdventOfCode2024/8Dec2024.txt");

            var matrix = CreateMatrix(stringText);
            Dictionary<char, List<(int posY, int posX)>> nodes = GetNodes(matrix);

            var result = CalculateAntinodes2(matrix, nodes);

            Console.WriteLine("08/12/24 Part 2 - {0}", result);
        }

        private static long CalculateAntinodes2(char[][] matrix, Dictionary<char, List<(int posY, int posX)>> nodes)
        {
            foreach (var node in nodes)
            {
                var combinations = GenerateCombos(node.Value);

                foreach (var combination in combinations)
                {
                    var point1 = combination.point1;
                    var point2 = combination.point2;

                    MarkMultipleAntinodes(matrix, point1, point2);
                    MarkMultipleAntinodes(matrix, point2, point1);
                }
            }

            PrintMatrix(matrix);
            return CountMarkedPlace(matrix);
        }

        private static Dictionary<char, List<(int posY, int posX)>> GetNodes(char[][] matrix)
        {
            var result = new Dictionary<char, List<(int posY, int posX)>>();

            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    var cellValue = matrix[r][c];

                    if (cellValue == '.') continue;
                    if (!result.ContainsKey(cellValue)) result.Add(cellValue, new() { (r, c) });
                    else result[cellValue].Add((r, c));
                }
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

        public static void PrintMatrix(char[][] matrix)
        {
            Console.Clear();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var value = matrix[i][j];
                    Console.Write(value);
                }
                Console.WriteLine();
            }

            Thread.Sleep(100);
        }

        private static List<((int posY, int posX) point1, (int posY, int posX) point2)> GenerateCombos(List<(int posY, int posX)> positions)
        {
            var combos = new List<((int posY, int posX) point1, (int posY, int posX) point2)>();

            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    if (positions[i] == positions[j]) continue;
                    combos.Add((positions[i], positions[j]));
                }
            }

            return combos;
        }

        private static double CalculateDistance((int posY, int posX) point1, (int posY, int posX) point2)
        {
            var deltaY = Math.Abs(point1.posY - point2.posY);
            var deltaX = Math.Abs(point1.posX - point2.posX);

            if (point1.posY == point2.posY) return deltaX;
            if (point1.posX == point2.posX) return deltaY;

            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance;
        }


        private static (int posY, int posX) CalculateAntinodePosition((int posY, int posX) point1, (int posY, int posX) point2)
        {
            (int posY, int posX) antinode = new();
            var deltaY = Math.Abs(point1.posY - point2.posY);
            var deltaX = Math.Abs(point1.posX - point2.posX);

            if(point1.posY > point2.posY)  antinode.posY = point2.posY - deltaY;
            else antinode.posY = point2.posY + deltaY;

            if(point1.posX > point2.posX) antinode.posX = point2.posX - deltaX;
            else antinode.posX = point2.posX + deltaX;

            return antinode;
        }

        private static long CountMarkedPlace(char[][] matrix)
        {
            long counter = 0;
            for (int r = 0; r < matrix.Length; r++)
            {
                var row = matrix[r];

                for (int c = 0; c < row.Length; c++)
                {
                    var value = matrix[r][c];
                    if (value == '.') continue;
                    else counter++;
                }
            }

            return counter;
        }

        private static void MarkMultipleAntinodes(char[][] matrix, (int posY, int posX) point1, (int posY, int posX) point2)
        {
            var antiNode = CalculateAntinodePosition(point1, point2);
            if (antiNode.posY >= 0 && antiNode.posX >= 0 && antiNode.posY < matrix.Length && antiNode.posX < matrix[0].Length)
            {
                matrix[antiNode.posY][antiNode.posX] = '#';
                MarkMultipleAntinodes(matrix, point2, antiNode);
            }
        }
    }
}
