namespace Day13;

internal class Pattern
{
  private readonly List<string> rows = [];

  public void AddRow(string newRow)
  {
    rows.Add(newRow);
  }

  public int GetReflectionValue()
  {
    int leftValue = GetLeftValue(null);
    if (leftValue > - 1)
    {
      return leftValue;
    }
    int topValue = GetTopValue(null);
    if (topValue > - 1)
    {
      return topValue * 100;
    }
    return -1;
  }

  private int GetLeftValue(int? oldValue)
  {
    string left = "";
    string right;
    for (int columnIndex = 0; columnIndex < rows[0].Length - 1; columnIndex++)
    {
      bool isMatch = true;
      for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
      {
        left = rows[rowIndex][..(columnIndex + 1)];
        left = new string(left.Reverse().ToArray());
        right = rows[rowIndex][(columnIndex + 1)..];
        int compareLength = Math.Min(left.Length, right.Length);
        if (left[..compareLength] != right[..compareLength])
        {
          isMatch = false;
          break;
        }
      }
      if (isMatch && oldValue != left.Length)
      {
        return left.Length;
      }
    }
    return -1;
  }

  private int GetTopValue(int? oldValue)
  {
    for (int rowIndex = 0; rowIndex < rows.Count - 1; rowIndex++)
    {
      int compareLength = Math.Min(rowIndex + 1, rows.Count - rowIndex - 1);
      bool isMatch = true;
      for (int i = 0; i < compareLength; i++)
      {
        string topRow = rows[rowIndex - i];
        string bottomRow = rows[rowIndex + i + 1];
        if (topRow != bottomRow)
        {
          isMatch = false;
          break;
        }
      }
      if (isMatch && oldValue != rowIndex + 1)
      {
        return rowIndex + 1;
      }
    }
    return -1;
  }

  public int GetSmudgedReflectionValue()
  {
    for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
    {
      string oldRow = rows[rowIndex];
      for (int columnIndex = 0; columnIndex < rows[0].Length; columnIndex++)
      {
        char newChar = '#';
        if (rows[rowIndex][columnIndex] == '#')
        {
          newChar = '.';
        }
        string newRow;
        newRow = oldRow[..columnIndex] + newChar + oldRow[(columnIndex + 1)..];
        int oldLeftValue = GetLeftValue(null);
        int oldTopValue = GetTopValue(null);
        rows[rowIndex] = newRow;
        int newLeftValue = GetLeftValue(oldLeftValue);
        if (newLeftValue > - 1)
        {
          rows[rowIndex] = oldRow;
          return newLeftValue;
        }
        int newTopValue = GetTopValue(oldTopValue);
        if (newTopValue > - 1)
        {
          rows[rowIndex] = oldRow;
          return newTopValue * 100;
        }
        rows[rowIndex] = oldRow;
      }
    }
    return -1;
  }

  public bool IsEmpty()
  {
    return rows.Count == 0;
  }
}