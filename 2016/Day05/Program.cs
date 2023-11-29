using System.Security.Cryptography;
using System.Text;

var text = File.ReadAllLines("input.txt")[0];
Part1(text);
Part2(text);

static void Part1(string text)
{
	var md5 = MD5.Create();
	var password = new StringBuilder();
	int i = 0;
	while (password.Length < 8)
	{
		var byteHash = md5.ComputeHash(Encoding.ASCII.GetBytes(text + i.ToString()));
		var hexHash = new StringBuilder();
		foreach (byte thisByte in byteHash)
		{
			hexHash.Append(thisByte.ToString("X2"));
		}
		if (hexHash.ToString().StartsWith("00000"))
		{
			password.Append(hexHash.ToString()[5]);
		}
		i++;
	}
	Console.WriteLine($"Part 1: {password.ToString().ToLower()}");
}

static void Part2(string text)
{
	var md5 = MD5.Create();
	var password = Enumerable.Repeat((char?)null, 8).ToArray();
	int i = 0;
	while (password.Count(x => x == null) > 0)
	{
		var byteHash = md5.ComputeHash(Encoding.ASCII.GetBytes(text + i.ToString()));
		var hexHash = new StringBuilder();
		foreach (byte thisByte in byteHash)
		{
			hexHash.Append(thisByte.ToString("X2"));
		}
		if (hexHash.ToString().StartsWith("00000"))
		{
			var index = int.Parse(hexHash.ToString()[5].ToString(), System.Globalization.NumberStyles.HexNumber);
			var value = hexHash.ToString()[6];
			if (index < password.Length && password[index] == null)
			{
				password[index] = value;
			}
		}
		i++;
	}
	Console.WriteLine($"Part 2: {string.Join("", password).ToLower()}");
}