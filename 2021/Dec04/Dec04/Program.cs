using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Dec04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("input.txt");
            string[] numbers = allLines[0].Split(',');
            const int SIZE = 5;
            int startIndex = 2;
            List<BingoCard> cards = new List<BingoCard>();
            while (startIndex + SIZE < allLines.Length)
            {
                BingoCard thisCard = new BingoCard();
                for (int i = startIndex; i <= startIndex + SIZE - 1; i++)
                {
                    thisCard.AddRow(allLines[i]);
                }
                cards.Add(thisCard);
                startIndex += SIZE + 1;
            }
            int lastNumber = -1;
            BingoCard winner = null;
            foreach (string thisNumber in numbers)
            {
                lastNumber = int.Parse(thisNumber);
                cards.ForEach(element => element.MarkSquare(lastNumber));
                foreach (BingoCard thisCard in cards)
                {
                    if (thisCard.IsAWinner())
                    {
                        winner = thisCard;
                        break;
                    }
                }
                if (winner != null)
                {
                    break;
                }
            }
            if (winner != null)
            {
                long part1Answer = (long)lastNumber * winner.GetUnmarkedSum();
                Console.WriteLine($"part 1: {part1Answer}");
            }

            // Part 2
            BingoCard lastWinner = null;
            List<BingoCard> previousWinners = new List<BingoCard>();
            cards.ForEach(element => element.Reset());
            List<BingoCard> stillInPlay = new List<BingoCard>(cards);
            foreach (string thisNumber in numbers)
            {
                stillInPlay.ForEach(element => element.MarkSquare(int.Parse(thisNumber)));
                List<BingoCard> winners = stillInPlay.Where(element => element.IsAWinner()).ToList();
                foreach (BingoCard thisWinner in winners)
                {
                    previousWinners.Add(thisWinner);
                    stillInPlay.Remove(thisWinner);
                    lastWinner = thisWinner;
                    lastNumber = int.Parse(thisNumber);
                }
            }
            if (lastWinner != null)
            {
                int temp = lastWinner.GetUnmarkedSum();
                long part2Answer = (long)lastNumber * lastWinner.GetUnmarkedSum();
                Console.WriteLine($"part 2: {part2Answer}");
            }

        }
    }
}
