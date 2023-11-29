namespace Day16;

using System.Text;

internal class DanceLine
{
	private Dancer head;

	public DanceLine(int size)
	{
		head = new Dancer('a');
		var current = head;
		for (int i = 2; i <= size; i++)
		{
			var newDancer = new Dancer((char)('a' + (i - 1)));
			current.next = newDancer;
			newDancer.previous = current;
			current = newDancer;
		}
		head.previous = current;
		current.next = head;
	}

	public void Dance(string text)
	{
		var steps = text.Split(',');
		foreach (string thisStep in steps)
		{
			switch (thisStep[0])
			{
				case 's':
					Spin(thisStep.Substring(1));
					break;
				case 'x':
					Exchange(thisStep.Substring(1));
					break;
				case 'p':
					Partner(thisStep.Substring(1));
					break;		
			}
		}
	}

	private void Spin(string stepData)
	{
		var x = int.Parse(stepData);
		for (int i = 1; i <= x && head.previous != null; i++)
		{
			head = head.previous;
		}
	}

	private void Exchange(string stepData)
	{
		var positionA = int.Parse(stepData.Split('/')[0]);		
		var cursorA = head;
		for (int i = 1; i <= positionA && cursorA.next != null; i++)
		{
			cursorA = cursorA.next;
		}
		var positionB = int.Parse(stepData.Split('/')[1]);
		var cursorB = head;
		for (int i = 1; i <= positionB && cursorB.next != null; i++)
		{
			cursorB = cursorB.next;
		}
		var temp = cursorA.value;
		cursorA.value = cursorB.value;
		cursorB.value = temp;
	}

	private void Partner(string stepData)
	{
		var valueA = stepData.Split('/')[0][0];
		var cursorA = head;
		while (cursorA.next != null && cursorA.value != valueA)
		{
			cursorA = cursorA.next;
		}
		var valueB = stepData.Split('/')[1][0];
		var cursorB = head;
		while (cursorB.next != null && cursorB.value != valueB)
		{
			cursorB = cursorB.next;
		}
		var temp = cursorA.value;
		cursorA.value = cursorB.value;
		cursorB.value = temp;
	}

	public override string ToString()
	{
		var builder = new StringBuilder();
		var cursor = head;
		do
		{
			builder.Append(cursor.value);
			cursor = cursor.next;
		} while (cursor != null && cursor != head);
		return builder.ToString();
	}

	private class Dancer
	{
		public char value { set; get; }
		public Dancer? previous { set; get; }
		public Dancer? next { set; get; }

		public Dancer (char name)
		{
			this.value = name;
		}
	}
}