namespace Day5;

internal class Instructions
{
	private Node? current;

	public Instructions(string[] jumps)
	{
		var head = new Node(int.Parse(jumps[0]));
		current = head;
		for (int i = 1; i < jumps.Length; i++)
		{
			var nextNode = new Node(int.Parse(jumps[i]));
			current.next = nextNode;
			nextNode.previous = current;
			current = nextNode;
		}
		current = head;
	}

	public int Execute(bool isPart2)
	{
		int stepCount = 0;
		while (current != null)
		{
			var previous = current;
			int jumpCount = current.value;
			if (jumpCount < 0)
			{
				for (int i = 1; i <= Math.Abs(jumpCount) && current != null; i++)
				{
					current = current.previous;
				}
			}
			else
			{
				for (int i = 1; i <= jumpCount && current != null; i++)
				{
					current = current.next;
				}
			}
			stepCount++;
			if (isPart2 && jumpCount >= 3)
			{
				previous.Decrement();
			}
			else
			{
				previous.Increment();
			}
			
		}
		return stepCount;
	}

	private class Node
	{
		public int value { set; get; }
		public Node? previous { set; get; } = null;
		public Node? next { set; get; } = null;

		public Node (int value)
		{
			this.value = value;
		}

		public void Increment()
		{
			value++;
		}

		public void Decrement()
		{
			value--;
		}
	}
}

