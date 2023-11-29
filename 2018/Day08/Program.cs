using Day08;

var inputLine = File.ReadAllLines("input.txt")[0];
Node.SetValues(inputLine);
var root = new Node();
Part1(root);
Part2(root);

static void Part1(Node root)
{	
	var sum = root.GetMetadataSum();
	Console.WriteLine($"Part 1: {sum}");
}

static void Part2(Node root)
{	
	var value = root.GetValue();
	Console.WriteLine($"Part 2: {value}");
}

