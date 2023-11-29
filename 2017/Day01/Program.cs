var text = File.ReadAllLines("input.txt")[0];
Part1(text);
Part2(text);

void Part1(string text)
{
	var sum = 0;
	for (int i = 0; i < text.Length; i++)
	{
		if (text[i] == text[(i + 1) % text.Length])
		{
			sum += int.Parse(text[i].ToString());
		}
	}
	Console.WriteLine($"Part 1: {sum}");
}

void Part2(string text)
{
	var sum = 0;
	for (int i = 0; i < text.Length; i++)
	{
		if (text[i] == text[(i + text.Length / 2) % text.Length])
		{
			sum += int.Parse(text[i].ToString());
		}
	}
	Console.WriteLine($"Part 2: {sum}");
}
