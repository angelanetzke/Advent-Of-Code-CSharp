using System.Collections.Generic;
using System;
using System.IO;

namespace Dec01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Part 1
            string[] allLines = System.IO.File.ReadAllLines("input.txt");
            int increaseCount = 0;
            int lastValue = -1;
            int currentValue = 0;
            foreach (string line in allLines)
            {
                if (lastValue == -1)
                {
                    lastValue = int.Parse(line);
                }
                else
                {
                    currentValue = int.Parse(line);
                    if (currentValue > lastValue)
                    {
                        increaseCount++;
                    }
                    lastValue = currentValue;
                }
            }
            Console.WriteLine("part 1: " + increaseCount);

            // Part 2
            List<int> allValues = new List<int>();
            foreach (string line in allLines)
            {
                allValues.Add(int.Parse(line));
            }
            increaseCount = 0;
            const int WINDOW_SIZE = 3;
            int lastStart = 0;
            int currentStart = 1;
            int lastWindow = 0;
            int currentWindow = 0;
            while (currentStart <= allValues.Count - WINDOW_SIZE)
            {
                lastWindow = 0;
                for (int i = lastStart; i < lastStart + WINDOW_SIZE; i++)
                {
                    lastWindow += allValues[i];
                }
                currentWindow = 0;
                for (int i = currentStart; i < currentStart + WINDOW_SIZE; i++)
                {
                    currentWindow += allValues[i];
                }
                if (currentWindow > lastWindow)
                {
                    increaseCount++;
                }
                lastStart++;
                currentStart++;
            }
            Console.WriteLine("part 2: " + increaseCount);
        }

    }
}
