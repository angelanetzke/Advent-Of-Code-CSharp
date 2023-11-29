using System.Text.RegularExpressions;

namespace Dec2
{
	internal class PolicyAndPassword
	{
		private readonly int min;
		private readonly int max;
		private readonly char neededChar;
		private readonly string password;
		public PolicyAndPassword(string line)
		{
			var minRegex = new Regex(@"(?<min>\d+)-.+");
			var maxRegex = new Regex(@"\d+-(?<max>\d+).+");
			var charRegex = new Regex(@"\d+-\d+ (?<char>.{1}).+");
			var passwordRegex = new Regex(@"\d+-\d+ .{1}: (?<password>.+)");
			var match = minRegex.Match(line);
			min = int.Parse(match.Groups["min"].Value);
			match = maxRegex.Match(line);
			max = int.Parse(match.Groups["max"].Value);
			match = charRegex.Match(line);
			neededChar = match.Groups["char"].Value.First();
			match = passwordRegex.Match(line);
			password = match.Groups["password"].Value;
		}
		public bool IsValid()
		{
			int neededCharCount = password.Count(x => x == neededChar);
			return min <= neededCharCount && neededCharCount <= max;
		}
	}
}
