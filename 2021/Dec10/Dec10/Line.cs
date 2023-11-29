using System.Collections.Generic;

namespace Dec10
{
	internal class Line
	{
		private string text;
		private static readonly List<char> leftCharacters = new() { '(', '[', '{', '<' };
		private static readonly List<char> rightCharacters = new() { ')', ']', '}', '>' };
		private static readonly Dictionary<char, char> partners = new()
		{
			['('] = ')',
			['['] = ']',
			['{'] = '}',
			['<'] = '>'
		};
		private static readonly Dictionary<char, int> part1Values = new()
		{
			[')'] = 3,
			[']'] = 57,
			['}'] = 1197,
			['>'] = 25137,
		};
		private static readonly Dictionary<char, long> part2Values = new()
		{
			['('] = 1L,
			['['] = 2L,
			['{'] = 3L,
			['<'] = 4L
		};
		public Line(string text)
		{
			this.text = text;
		}

		public int GetPart1Score()
		{
			Stack<char> chars = new();
			foreach (char thisChar in text)
			{
				if (leftCharacters.Contains(thisChar))
				{
					chars.Push(thisChar);
				}
				else if (rightCharacters.Contains(thisChar))
				{
					char topChar = chars.Peek();
					if (partners[topChar] == thisChar)
					{
						chars.Pop();
					}
					else
					{
						return part1Values[thisChar];
					}
				}
			}
			return 0;
		}

		public long GetPart2Score()
		{			
			Stack<char> chars = new();
			foreach (char thisChar in text)
			{
				if (leftCharacters.Contains(thisChar))
				{
					chars.Push(thisChar);
				}
				else if (rightCharacters.Contains(thisChar))
				{
					char topChar = chars.Peek();
					if (partners[topChar] == thisChar)
					{
						chars.Pop();
					}					
				}
			}
			long score = 0L;
			while(chars.Count > 0)
			{
				char topChar = chars.Pop();
				score = score * 5 + part2Values[topChar];
			}
			return score;
		}


	}
}
