using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dec04
{
    internal class BingoCard
    {
        private static readonly int SIZE = 5;
        private BingoSquare[] squares;
        private int insertionPoint;
        private static int nextID = 0;
        private readonly int ID;
        public BingoCard()
        {
            squares = new BingoSquare[SIZE * SIZE];
            insertionPoint = 0;
            ID = nextID;
            nextID++;
        }

        public void AddRow(String nextRow)
        {
            string[] tokens = nextRow.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string thisToken in tokens)
            {
                squares[insertionPoint] = new BingoSquare(int.Parse(thisToken));
                insertionPoint++;
            }
        }

        public void MarkSquare(int number)
        {
            foreach (BingoSquare thisSquare in squares)
            {
                thisSquare.MarkIfMatched(number);
            }
        }

        public int GetUnmarkedSum()
        {
            IEnumerable<BingoSquare> unMarkedSquares = squares.Where(element => !element.IsMarked());
            return unMarkedSquares.Select(element => element.GetValue()).Sum();            
        }

        public bool IsAWinner()
        {
            for (int i = 0; i < SIZE; i++)
            {
                if ( GetRow(i).Where(element => element.IsMarked()).Count() == SIZE )
                {
                    return true;
                }
                if (GetColumn(i).Where(element => element.IsMarked()).Count() == SIZE)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            Array.ForEach(squares, element => element.Reset());
        }

        private BingoSquare[] GetRow(int rowNumber)
        {
            return squares.Where((element, index) => index / SIZE == rowNumber).ToArray();
        }

        private BingoSquare[] GetColumn(int columnNumber)
        {
            return squares.Where((element, index) => index % SIZE == columnNumber).ToArray();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                BingoSquare[] thisRow = GetRow(i);
                foreach (BingoSquare thisSquare in thisRow)
                {
                    int thisValue = thisSquare.GetValue();
                    if (thisValue < 10)
                    {
                        builder.Append(" " + thisValue.ToString() + " ");
                    }
                    else
                    {
                        builder.Append(thisValue.ToString() + " ");
                    }
                }
                builder.Append('\n');
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is BingoCard card &&
                   ID == card.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
