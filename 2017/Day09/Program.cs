var text = File.ReadAllLines("input.txt")[0];
var garbageCount = 0;
var stack = new Stack<char>();
var total = 0;
var nextValue = 1;
var isGarbage = false;
for (int i = 0; i < text.Length; i++)
{	
	if (text[i] == '!')
	{
		i++;
	}
	else if (isGarbage)
	{
		if (text[i] == '>')
		{
			isGarbage = false;
		}
		else
		{
			garbageCount++;
		}
	}
	else if (text[i] == '<')
	{
		isGarbage = true;
	}
	else if (text[i] == '{')
	{
		stack.Push(text[i]);
		total += nextValue;
		nextValue++;
	}
	else if (text[i] == '}')
	{
		stack.Pop();
		nextValue--;
	}		
}
Console.WriteLine($"Part 1: {total}");
Console.WriteLine($"Part 2: {garbageCount}");

