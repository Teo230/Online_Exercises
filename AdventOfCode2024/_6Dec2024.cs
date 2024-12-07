using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _6Dec2024
    {
        public static void Part1()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/6Dec2024.txt");

            int[][] labyrinth = CreateMatrix(textLines);
            var guardPosition = GetGuardPosition(labyrinth);

            int currentPosX = guardPosition.posX;
            int currentPosY = guardPosition.posY;
            Direction currentDirection = Direction.Up;
            //PrintMatrix(labyrinth);
            labyrinth[currentPosY][currentPosX] = 1;

            var result = 1;
            while (true)
            {
                //PrintMatrix(labyrinth);

                var currentValue = labyrinth[currentPosY][currentPosX];

                if (currentValue == 0)
                {
                    labyrinth[currentPosY][currentPosX] = 1;
                    result++;
                }

                var desiredPosition = TakeNextStep(labyrinth, currentPosY, currentPosX, currentDirection);
                if (desiredPosition.posX < 0 || 
                    desiredPosition.posY < 0 ||
                    desiredPosition.posX >= labyrinth.Length ||
                    desiredPosition.posY >= labyrinth.Length) 
                    break;

                var desiredPositionValue = labyrinth[desiredPosition.posY][desiredPosition.posX];
                if(desiredPositionValue == -1)
                {
                    if (currentDirection == Direction.Up) currentDirection = Direction.Right;
                    else if (currentDirection == Direction.Right) currentDirection = Direction.Down;
                    else if (currentDirection == Direction.Down) currentDirection = Direction.Left;
                    else if (currentDirection == Direction.Left) currentDirection = Direction.Up;
                    continue;
                }

                guardPosition = desiredPosition;
                currentPosX = guardPosition.posX;
                currentPosY = guardPosition.posY;
            }

            Console.WriteLine("06/12/24 Part 1 - {0}", result);
        }

        public static void Part2()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/6Dec2024.txt");

            Console.WriteLine("06/12/24 Part 2 - {0}", 0);
        }

        private static int[][] CreateMatrix(string[] textLines)
        {
            int[][] matrix = new int[textLines.Length][];
            for (int i = 0; i < textLines.Length; i++)
            {
                var textLine = textLines[i];
                var chars = textLine.ToCharArray();
                var row = matrix[i] = new int[chars.Length];

                for (int j = 0; j < chars.Length; j++)
                {
                    int convertedChar = GetCharCode(chars[j]);
                    row[j] = convertedChar;
                }
            }

            return matrix;
        }

        private static (int posY, int posX) GetGuardPosition(int[][] labyrinth)
        {
            for (int r = 0; r < labyrinth.Length; r++)
            {
                var row = labyrinth[r];

                for (int c = 0; c < row.Length; c++)
                {
                    var column = row[c];
                    var value = labyrinth[r][c];
                    if (value == 2) return new(r, c);
                }
            }

            return (-1, -1);
        }

        private static int GetCharCode(char v)
        {
            if (v == '#') return -1;
            if (v == '^') return 2;
            return 0;
        }

        private static char GetIntCode(int v)
        {
            if (v == 1) return 'X';
            if (v == -1) return '#';
            if (v == 2) return '^';
            return '.';
        }

        private static (int posY, int posX) TakeNextStep(int[][] labyrinth, int currentPosY, int currentPosX, Direction currentDirection)
        {
            int[] dx = [0, 0, -1, 1];
            int[] dy = [-1, 1, 0, 0];

            currentPosX += dx[(int)currentDirection];
            currentPosY += dy[(int)currentDirection];

            return new(currentPosY, currentPosX);
        }

        public static void PrintMatrix(int[][] matrix)
        {
            Console.Clear();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var value = GetIntCode(matrix[i][j]);
                    Console.Write(value);
                }
                Console.WriteLine();
            }

            Thread.Sleep(100);
            //Console.ReadLine();
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
