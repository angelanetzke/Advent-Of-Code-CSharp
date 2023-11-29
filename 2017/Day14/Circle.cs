namespace Day14;

using System.Text;
internal class Circle
{
	private Node head;
	private Node? current;
	private int skipSize = 0;
	private int[] standardLengths = new int[] { 17, 31, 73, 47, 23 };

	public Circle(int size)
	{
		head = new Node(0);
		current = head;
		var cursor = head;
		for (int i = 1; i < size; i++)
		{
			var newNode = new Node(i);
			cursor.next = newNode;
			newNode.previous = cursor;
			cursor = newNode;
		}
		cursor.next = head;
		head.previous = cursor;
	}

	public int Execute(string[] lengths)
	{
		foreach (string thisLengthString in lengths)
		{	
			var thisLength = thisLengthString.Length > 0 ? int.Parse(thisLengthString) : 0;
			var start = current;
			var end = current;
			for (int i = 1; i <= thisLength - 1 && end != null; i++)
			{
				end = end.next;
			}
			for (int i = 1; i <= thisLength / 2 && start != null && end != null; i++)
			{
				var temp = start.value;
				start.value = end.value;
				end.value = temp;
				start = start.next;
				end = end.previous;
			}
			for (int i = 1; i <= thisLength + skipSize && current != null; i++)
			{
				current = current.next;
			}
			skipSize++;
		}
		
		if (head.next == null)
		{
			return -1;
		}
		return head.value * head.next.value;
	}

	public string Execute(string inputText)
	{
		var lengthList = new List<int>();
		foreach (char thisChar in inputText)
		{
			lengthList.Add((int)thisChar);
		}
		lengthList.AddRange(standardLengths);
		var lengths = lengthList.Select(x => x.ToString()).ToArray();
		for (int i = 1; i <= 64; i++)
		{
			Execute(lengths);
		}
		var builder = new StringBuilder();
		var cursor = head;
		for (int block = 1; block <= 16 && cursor != null; block++)
		{
			var thisBlockValue = cursor.value;
			cursor = cursor.next;
			for (int element = 2; element <= 16 && cursor != null; element++)
			{
				thisBlockValue ^= cursor.value;
				cursor = cursor.next;
			}
			builder.Append(thisBlockValue.ToString("x2"));
		}
		return builder.ToString();
	}

	private class Node
	{
		public Node? previous { set; get; }
		public Node? next { set; get; }
		public int value { set; get; }

		public Node (int value)
		{
			this.value = value;
		}		
	}
}