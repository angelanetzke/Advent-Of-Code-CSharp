using Day19;

var allLines = File.ReadAllLines("input.txt");

var diagram = new Diagram(allLines);
var result = diagram.Navigate();
Console.WriteLine($"Part 1: {result.code}");
Console.WriteLine($"Part 2: {result.steps}");




