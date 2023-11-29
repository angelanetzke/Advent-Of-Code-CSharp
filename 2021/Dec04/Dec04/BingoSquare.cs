namespace Dec04
{
    internal class BingoSquare
    {
        private readonly int value;
        private bool isMarked;

        public BingoSquare(int value)
        {
            this.value = value;
            isMarked = false;
        }

        public void MarkIfMatched(int otherValue)
        {
            if (value == otherValue)
            {
                isMarked = true;
            }
        }

        public int GetValue()
        {
            return value;
        }

        public bool IsMarked()
        {
            return isMarked;
        }

        public void Reset()
        {
            isMarked = false;
        }

    }

}
