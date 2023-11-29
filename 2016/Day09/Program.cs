using System.Text;
using System.Text.RegularExpressions;

var text = File.ReadAllLines("input.txt")[0];
Part1(text);
Part2(text);

static void Part1(string text)
{
	var builder = new StringBuilder();
	while (text.Length > 0)
	{
		var nextMarker = new Regex(@"\({1}[x\d]+\){1}").Match(text);
		if (nextMarker.Length > 0)
		{
			builder.Append(text[..nextMarker.Index]);
			var characterCount = int.Parse(nextMarker.Value.Split('x')[0][1..]);
			var repeatCount = int.Parse(nextMarker.Value.Split('x')[1][..^1]);
			var repeatIndex = nextMarker.Index + nextMarker.Length;
			var repeatedSection = text.Substring(repeatIndex, characterCount);
			for (int i = 1; i <= repeatCount; i++)
			{
				builder.Append(repeatedSection);
			}
			if (repeatIndex + characterCount >= text.Length)
			{
				text = "";
			}
			else
			{
				text = text[(repeatIndex + characterCount)..];
			}
			
		}
		else
		{
			builder.Append(text);
			text = "";
		}
	}
	Console.WriteLine($"Part 1: {builder.Length}");
}

static void Part2(string text)
{
	var length = GetLength(text);
	Console.WriteLine($"Part 2: {length}");
}

static long GetLength(string text)
{
	var nextMarker = new Regex(@"\({1}[x\d]+\){1}").Match(text);
	if (nextMarker.Length > 0)
	{
		long length = nextMarker.Index;
		var characterCount = int.Parse(nextMarker.Value.Split('x')[0][1..]);
		var repeatCount = int.Parse(nextMarker.Value.Split('x')[1][..^1]);
		var repeatIndex = nextMarker.Index + nextMarker.Length;
		var repeatedSection = text.Substring(repeatIndex, characterCount);
		if (new Regex(@"\({1}[x\d]+\){1}").IsMatch(repeatedSection))
		{
			length += repeatCount * GetLength(repeatedSection);
		}
		else
		{
			length += repeatCount * repeatedSection.Length;
		}
		if (repeatIndex + characterCount < text.Length)
		{
			length += GetLength(text[(repeatIndex + characterCount)..]);
		}
		return length;	
	}
	else
	{
		return text.Length;
	}	
}


