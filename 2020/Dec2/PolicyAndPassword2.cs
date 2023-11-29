using System.Text.RegularExpressions;

namespace Dec2
{
	internal class PolicyAndPassword2
	{
		private readonly int first;
		private readonly int second;
		private readonly char neededChar;
		private readonly string password;
		public PolicyAndPassword2(string line)
		{
			var minRegex = new Regex(@"(?<min>\d+)-.+");
			var maxRegex = new Regex(@"\d+-(?<max>\d+).+");
			var charRegex = new Regex(@"\d+-\d+ (?<char>.{1}).+");
			var passwordRegex = new Regex(@"\d+-\d+ .{1}: (?<password>.+)");
			var match = minRegex.Match(line);
			first = int.Parse(match.Groups["min"].Value);
			match = maxRegex.Match(line);
			second = int.Parse(match.Groups["max"].Value);
			match = charRegex.Match(line);
			neededChar = match.Groups["char"].Value.First();
			match = passwordRegex.Match(line);
			password = match.Groups["password"].Value;
		}
		public bool IsValid()
		{
			return password[first - 1] == neededChar ^ password[second - 1] == neededChar;
		}
	}
}
