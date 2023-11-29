using Day21;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2();

static void Part1(string[] allLines)
{
	var theDevice = new Device(allLines);
	var result = theDevice.Execute();
	Console.WriteLine($"Part 1: {result}");
}

static void Part2()
{
	var skipToLine8 = false;
	var cache = new HashSet<string>();
	var valueFirstAppearance = new Dictionary<int, int>();
	var cycleNumber = 0;
	var reg1 = 0;
	var reg2 = 0;
	var reg4 = 0;
	var reg5 = 0;
	while(true)
	{
		if (!skipToLine8)
		{
			reg5 = reg4 | 65536;
			reg4 = 1855046;
		}
		skipToLine8 = false;		
		reg2 = reg5 & 255;
		reg4 = reg4 + reg2;
		reg4 = reg4 & 16777215;
		reg4 = reg4 * 65899;
		reg4 = reg4 & 16777215;
		reg2 = 256 > reg5 ? 1 : 0;
		if (reg2 == 1)
		{
			reg2 = 0;
			var regsAsString = reg1.ToString() + "," + reg2.ToString() + ","
				+ reg4.ToString() + "," + reg5.ToString();
			if (cache.Contains(regsAsString))
			{
				var latestValue = valueFirstAppearance.Values.Max();
				var result = 
					valueFirstAppearance.Where(x => x.Value == latestValue)
					.Select(x => x.Key)
					.First();
				Console.WriteLine($"Part 2: {result}");				
				break;
			}
			cache.Add(regsAsString);
			if (!valueFirstAppearance.ContainsKey(reg4))
			{
				valueFirstAppearance[reg4] = cycleNumber;
			}
			cycleNumber++;
			continue;
		}
		do
		{
			reg1 = reg2 + 1;
			reg1 = reg1 * 256;
			reg1 = reg1 > reg5 ? 1 : 0;
			reg2 = reg1 == 0 ? reg2 + 1 : reg2;	
		} while (reg1 == 0);
		reg5 = reg2;
		skipToLine8 = true;
	}
}