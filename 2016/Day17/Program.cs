using Day17;
using System.Security.Cryptography;
using System.Text;

string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static string Part1(string input)
{
	int endX = 3;
	int endY = 3;
	HashSet<char> openDoors = [ 'b', 'c', 'd', 'e', 'f' ];
	List<(int, int, string, int, int)> queue = []; // x, y, path, distance, priority
	queue.Add((0, 0, "", 0, 0));
	while (queue.Count > 0)
	{
		queue = [ ..queue.OrderBy(x => x.Item5) ];
		(int x, int y, string path, int distance, int priority) = queue[0];
		queue.RemoveAt(0);
		if (x == endX && y == endY)
		{
			return path;
		}
		string hash = string.Join("", MD5.HashData(Encoding.ASCII.GetBytes(input + path))
			.Select(b => b.ToString("x2")));
		if (y > 0 && openDoors.Contains(hash[0]))
		{
			queue.Add((x, y - 1, path + "U", distance + 1, distance + GetDistanceToEnd(x, y - 1, endX, endY)));
		}
		if (y < endY && openDoors.Contains(hash[1]))
		{
			queue.Add((x, y + 1, path + "D", distance + 1, distance + GetDistanceToEnd(x, y + 1, endX, endY)));
		}
		if (x > 0 && openDoors.Contains(hash[2]))
		{
			queue.Add((x - 1, y, path + "L", distance + 1, distance + GetDistanceToEnd(x - 1, y, endX, endY)));
		}
		if (x < endX && openDoors.Contains(hash[3]))
		{
			queue.Add((x + 1, y, path + "R", distance + 1, distance + GetDistanceToEnd(x + 1, y, endX, endY)));
		}		
	}
	return "path not found";
}

static int GetDistanceToEnd(int x, int y, int endX, int endY)
{
	return Math.Abs(endX - x) + Math.Abs(endY - y);
}

static int Part2(string input)
{
	State start = new (0, 0, "");
	return start.GetPath(input, 3, 3).Length;
}