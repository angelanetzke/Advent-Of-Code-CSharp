using Day10;

var text = File.ReadAllLines("input.txt")[0];
var lengthArray = text.Split(',');
Part1(lengthArray);
Part2(text);

void Part1(string[] lengthArray)
{
	var circle = new Circle(256);
	var result = circle.Execute(lengthArray);
	Console.WriteLine($"Part 1: {result}");
}

void Part2(string text)
{
	var circle = new Circle(256);
	var result = circle.Execute(text);
	Console.WriteLine($"Part 2: {result}");
}



