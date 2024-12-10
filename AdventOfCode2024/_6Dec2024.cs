using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Security;
using static Online_Exercises.AdventOfCode2024._6Dec2024;
using System.Reflection.Metadata.Ecma335;

namespace Online_Exercises.AdventOfCode2024
{
    public static class _6Dec2024
    {
        public static void Part1()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/6Dec2024.txt");

            char[][] labyrinth = CreateMatrix(textLines);
            var guardPosition = GetGuardPosition(labyrinth);

            var result = StartGuardRound(labyrinth, guardPosition, Direction.Up);

            Console.WriteLine("06/12/24 Part 1 - {0}", result);
        }

        public static void Part2()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/6Dec2024.txt");

            char[][] labyrinth = CreateMatrix(textLines);
            var guardPosition = GetGuardPosition(labyrinth);

            var result = StartGuardRound2(labyrinth, guardPosition, Direction.Up);

            Console.WriteLine("06/12/24 Part 2 - {0}", result);
        }

        private static long StartGuardRound(char[][] labyrinth, (int posY, int posX) guardPosition, Direction direction)
        {
            long totalSteps = 1;
            while (true)
            {
                var desiredPosition = NextPosition(labyrinth, guardPosition.posY, guardPosition.posX, direction);

                // If touch the edges, stop the move
                if (ItTouchsTheEdges(labyrinth, desiredPosition)) return totalSteps;

                var desiredPositionValue = labyrinth[desiredPosition.posY][desiredPosition.posX];

                // If there is an obstacle, turn of 90°
                if (desiredPositionValue == '#')
                {
                    direction = ChangeDirection(direction);
                    continue;
                }

                // If it's a new place, add to the total place visited
                if (desiredPositionValue == '.') totalSteps++;

                labyrinth[guardPosition.posY][guardPosition.posX] = 'X';
                labyrinth[desiredPosition.posY][desiredPosition.posX] = '^';
                guardPosition = desiredPosition;
            }
        }

        private static long StartGuardRound2(char[][] labyrinth, (int posY, int posX) guardPosition, Direction direction)
        {
            long totalLoops = 0;
            var clonedLab = labyrinth.DeepClone();

            while (true)
            {
                var loop = CheckForLoop(clonedLab, guardPosition, direction);
                if (loop)
                    totalLoops++;

                //PrintMatrix(labyrinth);

                var desiredPosition = NextPosition(labyrinth, guardPosition.posY, guardPosition.posX, direction);

                // If touch the edges, stop the move
                if (ItTouchsTheEdges(labyrinth, desiredPosition)) return totalLoops;

                var desiredPositionValue = labyrinth[desiredPosition.posY][desiredPosition.posX];
                var guardPositionValue = labyrinth[guardPosition.posY][guardPosition.posX];

                // If there is an obstacle, turn of 90°
                if (desiredPositionValue == '#')
                {
                    direction = ChangeDirection(direction);
                    labyrinth[guardPosition.posY][guardPosition.posX] = '+';
                    continue;
                }

                // If it's a new place, add to the total place visited
                else if (desiredPositionValue == '.')
                {
                    if (guardPositionValue != '+') labyrinth[guardPosition.posY][guardPosition.posX] = GetDirectionChar(direction);
                    labyrinth[desiredPosition.posY][desiredPosition.posX] = '^';
                }

                // If it's turning around, replace with a mark
                if (desiredPositionValue == '|' || desiredPositionValue == '-')
                {
                    if (guardPositionValue != '+') labyrinth[guardPosition.posY][guardPosition.posX] = GetDirectionChar(direction);
                    labyrinth[desiredPosition.posY][desiredPosition.posX] = '+';
                }

                guardPosition = desiredPosition;
            }
        }

        private static bool CheckForLoop(char[][] labyrinth, (int posY, int posX) guardPosition, Direction currentDirection)
        {
            var startPosition = guardPosition;
            currentDirection = ChangeDirection(currentDirection);
            var clonedLab = labyrinth.DeepClone();
            List<(int, int)> loopPlaces = new List<(int, int)>() { startPosition };

            while (true)
            {
                var desiredPosition = NextPosition(clonedLab, guardPosition.posY, guardPosition.posX, currentDirection);

                // If touch the edges, stop the move
                if (ItTouchsTheEdges(clonedLab, desiredPosition)) return false;

                var desiredPositionValue = clonedLab[desiredPosition.posY][desiredPosition.posX];
                var guardPositionValue = clonedLab[guardPosition.posY][guardPosition.posX];

                // If there is an obstacle, turn of 90°
                if (desiredPositionValue == '#')
                {
                    currentDirection = ChangeDirection(currentDirection);
                    continue;
                }

                guardPosition = desiredPosition;
                loopPlaces.Add(guardPosition);

                if (guardPosition == startPosition) return true;
                if (loopPlaces.Where(x => x == guardPosition).Count() > 2) return true;
            }
        }

        private static bool ItTouchsTheEdges(char[][] labyrinth, (int posY, int posX) position)
        {
            if (position.posY < 0 || position.posX < 0 ||
                position.posY >= labyrinth.Length || position.posX >= labyrinth[0].Length)
                return true;
            return false;
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

        private static (int posY, int posX) GetGuardPosition(char[][] labyrinth)
        {
            for (int r = 0; r < labyrinth.Length; r++)
            {
                var row = labyrinth[r];

                for (int c = 0; c < row.Length; c++)
                {
                    var value = labyrinth[r][c];
                    if (value == '^') return new(r, c);
                }
            }

            return (-1, -1);
        }

        private static Direction ChangeDirection(Direction currentDirection)
        {
            if (currentDirection == Direction.Up) currentDirection = Direction.Right;
            else if (currentDirection == Direction.Right) currentDirection = Direction.Down;
            else if (currentDirection == Direction.Down) currentDirection = Direction.Left;
            else if (currentDirection == Direction.Left) currentDirection = Direction.Up;
            return currentDirection;
        }

        private static char GetDirectionChar(Direction direction)
        {
            if (direction == Direction.Up || direction == Direction.Down) return '|';
            return '-';
        }

        private static (int posY, int posX) NextPosition(char[][] labyrinth, int currentPosY, int currentPosX, Direction currentDirection)
        {
            int[] dx = [0, 0, -1, 1];
            int[] dy = [-1, 1, 0, 0];

            currentPosX += dx[(int)currentDirection];
            currentPosY += dy[(int)currentDirection];

            return new(currentPosY, currentPosX);
        }

        private static (int posY, int posX) TakeBackStep(char[][] labyrinth, int currentPosY, int currentPosX, Direction currentDirection)
        {
            int[] dx = [0, 0, 1, -1];
            int[] dy = [1, -1, 0, 0];

            currentPosX += dx[(int)currentDirection];
            currentPosY += dy[(int)currentDirection];

            return new(currentPosY, currentPosX);
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

            Thread.Sleep(250);
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                JsonSerializer.Serialize(ms, obj);
                ms.Position = 0;
                return JsonSerializer.Deserialize<T>(ms);
            }
        }
    }
}
