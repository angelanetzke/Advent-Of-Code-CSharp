using System;

namespace Dec02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Part 1
            string[] allLines = System.IO.File.ReadAllLines("input.txt");
            long horizontalPosition = 0;
            long verticalPosition = 0;
            String thisDirection;
            long thisDistance;
            foreach (string line in allLines)
            {
                string[] tokens = line.Split(" ");
                thisDirection = tokens[0];
                thisDistance = long.Parse(tokens[1]);
                switch (thisDirection)
                {
                    case "forward":
                        horizontalPosition += thisDistance;
                        break;
                    case "up":
                        verticalPosition -= thisDistance;
                        break;
                    case "down":
                        verticalPosition += thisDistance;
                        break;
                }
            }
            long part1Answer = horizontalPosition * verticalPosition;
            Console.WriteLine("part 1: " + part1Answer);

            //Part 2
            long aim = 0l;
            horizontalPosition = 0l;
            verticalPosition = 0l;
            foreach (string thisLine in allLines)
            {
                String[] tokens = thisLine.Split(" ");
                thisDirection = tokens[0];
                thisDistance = long.Parse(tokens[1]);
                switch (thisDirection)
                {
                    case "forward":
                        horizontalPosition += thisDistance;
                        verticalPosition += aim * thisDistance;
                        break;
                    case "up":
                        aim -= thisDistance;
                        break;
                    case "down":
                        aim += thisDistance;
                        break;
                }
            }
            long part2Answer = horizontalPosition * verticalPosition;
            Console.Write("part 2: " + part2Answer);
        }

    }
}
