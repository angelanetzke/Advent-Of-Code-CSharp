using System.Text.RegularExpressions;

namespace Dec4
{
	internal class Passport
	{
		private readonly Dictionary<string, string> fields = new();
		private static readonly string[] requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
		private static readonly string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
		public void AddFields(string nextLine)
		{
			var entries = nextLine.Split(" ");
			foreach (string thisEntry in entries)
			{
				var components = thisEntry.Split(":");
				fields.Add(components[0], components[1]);
			}
		}

		public bool IsEmpty()
		{
			return fields.Count == 0;
		}

		public bool IsValid()
		{
			foreach (string thisRequiredField in requiredFields)
			{
				if (!fields.ContainsKey(thisRequiredField))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsDataValid()
		{
			if (!IsByrValid())
			{
				return false;
			}
			if (!IsIyrValid())
			{
				return false;
			}
			if (!IsEyrValid())
			{
				return false;
			}
			if (!IsHgtValid())
			{
				return false;
			}
			if (!IsHclValid())
			{
				return false;
			}
			if (!IsEclValid())
			{
				return false;
			}
			if (!IsPidValid())
			{
				return false;
			}
			return true;
		}

		private bool IsByrValid()
		{
			if (fields.TryGetValue("byr", out string? byrString))
			{
				var byrRegex = new Regex(@"^\d{4}$");
				if (!byrRegex.IsMatch(byrString))
				{
					return false;
				}
				int byrValue = int.Parse(byrString);
				if (1920 <= byrValue && byrValue <= 2002)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool IsIyrValid()
		{
			if (fields.TryGetValue("iyr", out string? iyrString))
			{
				var iyrRegex = new Regex(@"^\d{4}$");
				if (!iyrRegex.IsMatch(iyrString))
				{
					return false;
				}
				int iyrValue = int.Parse(iyrString);
				if (2010 <= iyrValue && iyrValue <= 2020)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool IsEyrValid()
		{
			if (fields.TryGetValue("eyr", out string? eyrString))
			{
				var eyrRegex = new Regex(@"^\d{4}$");
				if (!eyrRegex.IsMatch(eyrString))
				{
					return false;
				}
				int eyrValue = int.Parse(eyrString);
				if (2020 <= eyrValue && eyrValue <= 2030)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool IsHgtValid()
		{
			if (fields.TryGetValue("hgt", out string? hgtString))
			{
				var cmRegex = new Regex(@"^(?<cm>\d+)cm$");
				var inRegex = new Regex(@"^(?<in>\d+)in$");
				if (cmRegex.IsMatch(hgtString))
				{
					int cmValue = int.Parse(cmRegex.Match(hgtString).Groups["cm"].Value);
					if (150 <= cmValue && cmValue <= 193)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else if (inRegex.IsMatch(hgtString))
				{
					int inValue = int.Parse(inRegex.Match(hgtString).Groups["in"].Value);
					if (59 <= inValue && inValue <= 76)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool IsHclValid()
		{
			if (fields.TryGetValue("hcl", out string? hclString))
			{
				var hclRegex = new Regex(@"^#{1}[0-9,a-f]{6}$");
				if (hclRegex.IsMatch(hclString))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private bool IsEclValid()
		{
			if (fields.TryGetValue("ecl", out string? eclString))
			{
				foreach (string thisEyeColor in eyeColors)
				{
					if (eclString == thisEyeColor)
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				return false;
			}
		}

		private bool IsPidValid()
		{
			if (fields.TryGetValue("pid", out string? pidString))
			{
				var pidRegex = new Regex(@"^[0-9]{9}$");
				if (pidRegex.IsMatch(pidString))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

	}
}
