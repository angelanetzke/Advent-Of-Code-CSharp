using System.Text;
using System.Text.RegularExpressions;

namespace Day04;

internal class WordSearch(string[] letters)
{
	private readonly string[] letters = letters;
	private readonly StringBuilder builder = new ();
	private static readonly string target = "XMAS";
	private static readonly Regex targetRegex = new (target);
	private static readonly Regex reversedRegex = new (string.Join("", target.Reverse()));
	private static readonly string target2 = "MAS";
	private static readonly Regex targetRegex2 = new (target2);
	private static readonly Regex reversedRegex2 = new (string.Join("", target2.Reverse()));

	public long Part1()
	{
		long count = 0L;
		for (int i = 0; i < letters.Length; i++)
		{
			count += targetRegex.Matches(letters[i]).Count;
			count += reversedRegex.Matches(letters[i]).Count;
		}
		for (int i = 0; i < letters[0].Length; i++)
		{
			string s = GetColumn(i);
			count += targetRegex.Matches(s).Count;
			count += reversedRegex.Matches(s).Count;
		}
		for (int i = target.Length - 1; i <= letters.Length + letters[0].Length - target.Length - 1; i++)
		{
			string s = GetDiagonalDownLeft(i);
			count += targetRegex.Matches(s).Count;
			count += reversedRegex.Matches(s).Count;
		}
		for (int i = target.Length - 1; i <= letters.Length + letters[0].Length - target.Length - 1; i++)
		{
			string s = GetDiagonalDownRight(i);
			count += targetRegex.Matches(s).Count;
			count += reversedRegex.Matches(s).Count;
		}
		return count;
	}

	public long Part2()
	{
		Dictionary<(int, int), int> usedACounts = [];
		MatchCollection mc;
		for (int i = target2.Length - 1; i <= letters.Length + letters[0].Length - target2.Length - 1; i++)
		{
			int startRow = i < letters[0].Length ? 0 : i - letters[0].Length + 1;
			int startColumn = i < letters[0].Length ? i : letters[0].Length - 1;			
			string s = GetDiagonalDownLeft(i);
			mc = targetRegex2.Matches(s);
			foreach (Match m in mc)
			{
				int aRow = startRow + m.Index + 1;
				int aColumn = startColumn - m.Index - 1;
				usedACounts[(aRow, aColumn)] = usedACounts.ContainsKey((aRow, aColumn)) ? usedACounts[(aRow, aColumn)]+ 1 : 1;
			}
			mc = reversedRegex2.Matches(s);
			foreach (Match m in mc)
			{
				int aRow = startRow + m.Index + 1;
				int aColumn = startColumn - m.Index - 1;
				usedACounts[(aRow, aColumn)] = usedACounts.ContainsKey((aRow, aColumn)) ? usedACounts[(aRow, aColumn)] + 1 : 1;
			}
		}
		for (int i = target2.Length - 1; i <= letters.Length + letters[0].Length - target2.Length - 1; i++)
		{
			int startRow = i < letters.Length ? letters.Length - i - 1 : 0;
			int startColumn = i < letters.Length ? 0 : i - letters.Length + 1;
			string s = GetDiagonalDownRight(i);
			mc = targetRegex2.Matches(s);
			foreach (Match m in mc)
			{
				int aRow = startRow + m.Index + 1;
				int aColumn = startColumn + m.Index + 1;
				usedACounts[(aRow, aColumn)] = usedACounts.ContainsKey((aRow, aColumn)) ? usedACounts[(aRow, aColumn)] + 1 : 1;
			}
			mc = reversedRegex2.Matches(s);
			foreach (Match m in mc)
			{
				int aRow = startRow + m.Index + 1;
				int aColumn = startColumn + m.Index + 1;
				usedACounts[(aRow, aColumn)] = usedACounts.ContainsKey((aRow, aColumn)) ? usedACounts[(aRow, aColumn)] + 1 : 1;
			}
		}
		return usedACounts.Values.Count(x => x > 1);
	}

	private string GetColumn(int column)
	{
		builder.Clear();
		int row = 0;
		while (row < letters.Length)
		{
			builder.Append(letters[row][column]);
			row++;
		}
		return builder.ToString();
	}

	private string GetDiagonalDownLeft(int diagonal)
	{
		builder.Clear();
		int row = diagonal < letters[0].Length ? 0 : diagonal - letters[0].Length + 1;
		int column = diagonal < letters[0].Length ? diagonal : letters[0].Length - 1;
		while (row < letters.Length && column >= 0)
		{
			builder.Append(letters[row][column]);
			row++;
			column--;
		}
		return builder.ToString();
	}

	private string GetDiagonalDownRight(int diagonal)
	{
		builder.Clear();
		int row = diagonal < letters.Length ? letters.Length - diagonal - 1 : 0;
		int column = diagonal < letters.Length ? 0 : diagonal - letters.Length + 1;
		while (row < letters.Length && column < letters[0].Length)
		{
			builder.Append(letters[row][column]);
			row++;
			column++;
		}
		return builder.ToString();
	}

}