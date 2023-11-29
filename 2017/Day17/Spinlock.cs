namespace Day17;

internal class Spinlock
{
	private static readonly int maxValue = 2017;
	public static int Execute(int steps)
	{
		var cursor = new Node(0);
		cursor.next = cursor;
		cursor.previous = cursor;
		for (int nextValue = 1; nextValue <= maxValue; nextValue++)
		{
			for (int i = 1; i <= steps && cursor.next != null; i++)
			{
				cursor = cursor.next;
			}
			if (cursor.next != null)
			{
				var newNode = new Node(nextValue);
				var oldNext = cursor.next;
				cursor.next = newNode;
				oldNext.previous = newNode;
				newNode.previous = cursor;
				newNode.next = oldNext;
				cursor = newNode;
			}
		}
		return cursor.next == null? -1 : cursor.next.value;
	}

	private class Node
	{
		public int value { set; get; }
		public Node? previous { set; get; }
		public Node? next { set; get; }

		public Node(int value)
		{
			this.value = value;
		}
	}
}