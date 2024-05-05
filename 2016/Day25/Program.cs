string[] allLines = File.ReadAllLines("input.txt");
int number1 = int.Parse(allLines[1].Split(' ')[1]);
int number2 = int.Parse(allLines[2].Split(' ')[1]);
int product = number1 * number2;
int sum = int.Parse("101010101010", System.Globalization.NumberStyles.BinaryNumber);
int result = sum - product;
Console.WriteLine($"Part 1: {result}");
