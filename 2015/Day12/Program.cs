using System.Text.Json;

string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	JsonDocument document = JsonDocument.Parse(input);
	return GetNumberSum(document.RootElement);
}

static int Part2(string input)
{
	JsonDocument document = JsonDocument.Parse(input);
	return GetNumberSum2(document.RootElement);
}

static int GetNumberSum(JsonElement element)
{
	int sum = 0;
	if (element.ValueKind == JsonValueKind.Object)
	{
		foreach (var thisProperty in element.EnumerateObject())
		{
			if (thisProperty.Value.ValueKind == JsonValueKind.Number)
			{
				sum += int.Parse(thisProperty.Value.ToString());
			}
			else
			{
				sum += GetNumberSum(thisProperty.Value);
			}
		}
	}
	else if (element.ValueKind == JsonValueKind.Array)
	{
		foreach (var thisElement in element.EnumerateArray())
		{
			if (thisElement.ValueKind == JsonValueKind.Number)
			{
				sum += thisElement.GetInt32();
			}
			else
			{
				sum += GetNumberSum(thisElement);
			}
		}
	}
	return sum;
}

static int GetNumberSum2(JsonElement element)
{
	List<int> valuesToAdd = [];
	if (element.ValueKind == JsonValueKind.Object)
	{
		foreach (var thisProperty in element.EnumerateObject())
		{
			if (thisProperty.Name == "red" || thisProperty.Value.ToString() == "red")
			{
				return 0;
			}
			if (thisProperty.Value.ValueKind == JsonValueKind.Number)
			{
				valuesToAdd.Add(int.Parse(thisProperty.Value.ToString()));
			}
			else
			{
				valuesToAdd.Add(GetNumberSum2(thisProperty.Value));
			}
		}
	}
	else if (element.ValueKind == JsonValueKind.Array)
	{
		foreach (var thisElement in element.EnumerateArray())
		{
			if (thisElement.ValueKind == JsonValueKind.Number)
			{
				valuesToAdd.Add(thisElement.GetInt32());
			}
			else
			{
				valuesToAdd.Add(GetNumberSum2(thisElement));
			}
		}
	}
	return valuesToAdd.Sum();
}

