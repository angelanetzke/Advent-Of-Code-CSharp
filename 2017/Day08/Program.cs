var allLines = File.ReadAllLines("input.txt");
var highestRegisterValue = int.MinValue;
var registers = new Dictionary<string, int>();
foreach (string thisLine in allLines)
{
	var tokens = thisLine.Split(' ');
	var comparisonRegister = tokens[4];
	var comparisonRegisterValue = registers.ContainsKey(comparisonRegister) 
		? registers[comparisonRegister] : 0;
	var comparisonType = tokens[5];
	var comparisonValue = int.Parse(tokens[6]);
	bool isComparisonTrue;
	isComparisonTrue = comparisonType switch
	{
		"<" => comparisonRegisterValue < comparisonValue,
		">" => comparisonRegisterValue > comparisonValue,
		"<=" => comparisonRegisterValue <= comparisonValue,
		">=" => comparisonRegisterValue >= comparisonValue,
		"==" => comparisonRegisterValue == comparisonValue,
		"!=" => comparisonRegisterValue != comparisonValue,
		_ => false
	};	
	if (isComparisonTrue)
	{
		var resultRegister = tokens[0];
		if (!registers.ContainsKey(resultRegister))
		{
			registers[resultRegister] = 0;
		}
		var resultValue = int.Parse(tokens[2]);
		var resultType = tokens[1];
		registers[resultRegister] = resultType switch
		{
			"inc" => registers[resultRegister] += resultValue,
			"dec" => registers[resultRegister] -= resultValue,
			_ => registers[resultRegister]
		};
		highestRegisterValue = Math.Max(highestRegisterValue, registers[resultRegister]);
	}
}
var result = registers.Values.Max();
Console.WriteLine($"Part 1: {result}");
Console.WriteLine($"Part 2: {highestRegisterValue}");
