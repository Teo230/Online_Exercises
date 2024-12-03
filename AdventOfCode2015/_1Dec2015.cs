using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2015
{
    public static class _1Dec2015
    {
        public static void Part1()
        {
            var stringText = File.ReadAllText("AdventOfCode2015/1Dec2015.txt");

            var steps = stringText.Chunk(1).Select(x => x.First()).ToList();

            var totalUp = steps.Where(letter => letter == '(').Count();
            var totalDown = steps.Where(letter => letter == ')').Count();

            var delta = totalUp - totalDown;

            Console.WriteLine("The floor is {0}", delta);
        }

        public static void Part2()
        {
            var stringText = File.ReadAllText("AdventOfCode2015/1Dec2015.txt");

            var steps = stringText.Chunk(1).Select(x => x.First()).ToList();

            int currentFloor = 0;
            for (var i = 0; i < steps.Count; i++)
            {
                if (steps[i] == '(') currentFloor++;
                else currentFloor--;

                if(currentFloor < 0)
                {
                    Console.WriteLine("Santa enter the basement at {0}", i + 1);
                    return;
                }
            }
        }
    }
}
