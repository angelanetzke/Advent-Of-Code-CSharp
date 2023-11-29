using Dec21;

var allLines = System.IO.File.ReadAllLines("input.txt");
var foods = new FoodCollection(allLines);
Part1(foods);
Part2(foods);

static void Part1(FoodCollection foods)
{
	int safeCount = foods.GetSafeCount();	
	Console.WriteLine($"Part 1: {safeCount}");
}

static void Part2(FoodCollection foods)
{
	string dangerousIngredients = foods.GetDangerousList();
	Console.WriteLine($"Part 2: {dangerousIngredients}");
}


