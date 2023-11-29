using System.Text.RegularExpressions;

namespace Dec16
{
	internal class TicketCollection
	{
		private readonly List<string> rules = new();
		private readonly List<Ticket> allTickets = new();
		private readonly List<Ticket> validTickets = new();
		public TicketCollection(string[] allLines)
		{
			var ruleRegex = new Regex(@"^[a-z ]+: \d+-\d+ or \d+-\d+$");
			var ticketRegex = new Regex(@"^[\d+,]+$");
			foreach (string thisLine in allLines)
			{
				if (ruleRegex.IsMatch(thisLine))
				{
					rules.Add(thisLine);
				}
				if (ticketRegex.IsMatch(thisLine))
				{
					var newTicket = new Ticket(thisLine, rules);
					allTickets.Add(newTicket);
					if (newTicket.GetErrorRate() == 0)
					{
						validTickets.Add(newTicket);
					}
				}
			}
		}

		public int GetErrorRate()
		{
			return allTickets.Sum(thisTicket => thisTicket.GetErrorRate());
		}

		public long GetDepartureProduct()
		{
			List<string> fieldNames = new();
			foreach (string thisRule in rules)
			{
				fieldNames.Add(thisRule.Split(':')[0]);
			}
			var possible = new List<List<string>>();
			for (int i = 0; i < fieldNames.Count; i++)
			{
				possible.Add(new List<string>(fieldNames));
			}
			while(possible.Count(thisList => thisList.Count == 1) < possible.Count)
			{				
				for (int fieldNumber = 0; fieldNumber < fieldNames.Count; fieldNumber++)
				{
					for (int ruleNumber = 0; ruleNumber < rules.Count; ruleNumber++)
					{
						foreach (Ticket thisTicket in validTickets)
						{
							string thisFieldName = rules[ruleNumber].Split(':')[0];
							if (!Ticket.Validate(thisTicket.GetValue(fieldNumber), rules[ruleNumber]))
							{
								possible[fieldNumber].Remove(thisFieldName);
								break;
							}							
						}
					}
				}
				for (int i = 0; i < possible.Count; i++)
				{
					if (possible[i].Count == 1)
					{
						for (int j = 0; j < possible.Count; j++)
						{
							if (i != j)
							{
								possible[j].Remove(possible[i][0]);
							}
						}
					}
				}
			}
			long product = 1L;
			for (int i = 0; i < possible.Count; i++)
			{
				if (possible[i][0].StartsWith("departure"))
				{
					product *= allTickets[0].GetValue(i);
				}
			}
			return product;
		}

	}
}
