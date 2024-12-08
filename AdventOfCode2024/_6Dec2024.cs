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

namespace Online_Exercises.AdventOfCode2024
{
    public static class _6Dec2024
    {
        public static void Both()
        {
            var textLines = File.ReadAllLines("AdventOfCode2024/6Dec2024.txt");

            char[][] labyrinth = CreateMatrix(textLines);
            var guardPosition = GetGuardPosition(labyrinth);

            var result = DoPath(labyrinth, guardPosition.posY, guardPosition.posX, Direction.Up);

            Console.WriteLine("06/12/24 Part 1 - {0}", result.guardSteps);
            Console.WriteLine("06/12/24 Part 2 - {0}", result.loops);
        }

        private static (long guardSteps, long loops) DoPath(char[][] labyrinth, int currentPosY, int currentPosX, Direction currentDirection)
        {
            List<(int posY, int posX)> guardSteps = new();
            long loopsResult = 0;
            char[][] cleanLabyrith = labyrinth.DeepClone();
            long check = 0;

            while (true)
            {
                check++;
                var currentValue = labyrinth[currentPosY][currentPosX];
                if (currentValue == '.' || currentValue == '^')
                {
                    labyrinth[currentPosY][currentPosX] = GetDirectionChar(currentDirection);
                    guardSteps.Add((currentPosY, currentPosX));
                }
                else if (currentValue == '|' || currentValue == '-')
                {
                    labyrinth[currentPosY][currentPosX] = '+';
                }

                // Check for loop
                var loop = CheckForLoop(cleanLabyrith, currentPosY, currentPosX, currentDirection);
                if (loop) loopsResult++;

                var desiredPosition = TakeNextStep(labyrinth, currentPosY, currentPosX, currentDirection);
                if (desiredPosition.posX < 0 || desiredPosition.posY < 0 || desiredPosition.posX >= labyrinth.Length || desiredPosition.posY >= labyrinth.Length)
                    break;

                var desiredPositionValue = labyrinth[desiredPosition.posY][desiredPosition.posX];
                if (desiredPositionValue == '#')
                {
                    currentDirection = ChangeDirection(currentDirection);
                    continue;
                }

                currentPosX = desiredPosition.posX;
                currentPosY = desiredPosition.posY;
            }

            return (guardSteps.Count, loopsResult);
        }

        private static bool CheckForLoop(char[][] labyrinth, int currentPosY, int currentPosX, Direction currentDirection)
        {
            List<(int posY, int posX)> guardSteps = new();
            char[][] cleanLabyrith = labyrinth.DeepClone();
            currentDirection = ChangeDirection(currentDirection);

            while (true)
            {
                var currentValue = cleanLabyrith[currentPosY][currentPosX];
                if (currentValue == '.' || currentValue == '^')
                {
                    cleanLabyrith[currentPosY][currentPosX] = GetDirectionChar(currentDirection);
                    guardSteps.Add((currentPosY, currentPosX));
                }
                else if (currentValue == '|' || currentValue == '-')
                {
                    cleanLabyrith[currentPosY][currentPosX] = '+';
                }

                var desiredPosition = TakeNextStep(cleanLabyrith, currentPosY, currentPosX, currentDirection);
                if (desiredPosition.posX < 0 || desiredPosition.posY < 0 || desiredPosition.posX >= cleanLabyrith.Length || desiredPosition.posY >= cleanLabyrith.Length)
                    return false;

                var desiredPositionValue = cleanLabyrith[desiredPosition.posY][desiredPosition.posX];

                if (guardSteps.Contains((desiredPosition.posY, desiredPosition.posX)))
                    return true;

                if (desiredPositionValue == '#')
                {
                    currentDirection = ChangeDirection(currentDirection);
                    continue;
                }

                currentPosX = desiredPosition.posX;
                currentPosY = desiredPosition.posY;
            }
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
                    var column = row[c];
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

        private static (int posY, int posX) TakeNextStep(char[][] labyrinth, int currentPosY, int currentPosX, Direction currentDirection)
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

            //Console.ReadLine();
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
