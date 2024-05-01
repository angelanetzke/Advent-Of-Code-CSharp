using System.Security.Cryptography;
using System.Text;

namespace Day17;

internal class State
{
	private static string input = "";
	private static int endX = -1;
	private static int endY = -1;
	private static string longestPath = "";
	private static readonly HashSet<char> openDoors = [ 'b', 'c', 'd', 'e', 'f' ];
	private readonly int x;
	private readonly int y;
	private readonly string path;

	public State(int x, int y, string path)
	{
		this.x = x;
		this.y = y;
		this.path = path;
	}

	public string GetPath(string input, int endX, int endY)
	{
		State.input = input;
		State.endX = endX;
		State.endY = endY;
		GetPathRecursive();
		return longestPath;
	}

	private void GetPathRecursive()
	{
		if (x == endX && y == endY)
		{
			if (path.Length > longestPath.Length)
			{
				longestPath = path;
			}
			return;
		}
		string hash = string.Join("", MD5.HashData(Encoding.ASCII.GetBytes(input + path))
			.Select(b => b.ToString("x2")));
		if (y > 0 && openDoors.Contains(hash[0]))
		{
			new State(x, y - 1, path + "U").GetPathRecursive();
		}
		if (y < endY && openDoors.Contains(hash[1]))
		{
			new State(x, y + 1, path + "D").GetPathRecursive();
		}
		if (x > 0 && openDoors.Contains(hash[2]))
		{
			new State(x - 1, y, path + "L").GetPathRecursive();
		}
		if (x < endX && openDoors.Contains(hash[3]))
		{
			new State(x + 1, y, path + "R").GetPathRecursive();
		}
	}

}