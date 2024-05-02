using System.Text;

namespace Day21;

internal class Password
{
	private Node? head;
	private static readonly Dictionary<int, (string, int)> reverseRotate = new()
	{
		{ 1, ("left", 1) },
		{ 3, ("left", 2) },
		{ 5, ("left", 3) },
		{ 7, ("left", 4) },
		{ 2, ("right", 1) },
		{ 4, ("right", 2) },
		{ 0, ("left", 1) } 
	};
	
	public Password(string password)
	{
		head = new Node() { Value = password[0] };
		Node last = head;
		for (int i = 1; i < password.Length; i++)
		{
			Node newNode = new () { Value = password[i] };
			last.Next = newNode;
			newNode.Previous = last;
			last = newNode;
		}
		last.Next = head;
		head.Previous = last;
	}

	public string Scramble(string[] steps)
	{
		foreach (string thisStep in steps)
		{
			string[] parts = thisStep.Split(' ');
			switch (parts[0])
			{
				case "swap":
					if (parts[1] == "position")
					{
						SwapPosition(int.Parse(parts[2]), int.Parse(parts[5]));
					}
					else
					{
						SwapLetter(parts[2][0], parts[5][0]);
					}
					break;
				case "rotate":
					if (parts[1] == "left" || parts[1] == "right")
					{
						Rotate(parts[1], int.Parse(parts[2]));
					}
					else
					{
						int index = FindIndex(parts[6][0]);
						int distance = index >= 4 ? index + 2 : index + 1;
						Rotate("right", distance);
					}
					break;
				case "reverse":
					int start = int.Parse(parts[2]);
					int end = int.Parse(parts[4]);
					for (int i = start, j = end; i < j; i++, j--)
					{
						SwapPosition(i, j);
					}
					break;
				case "move":
					Move(int.Parse(parts[2]), int.Parse(parts[5]));
					break;					
			}
		}
		return ToString();
	}

	public string Unscramble(string[] steps)
	{
		for (int stepIndex = steps.Length - 1; stepIndex >= 0; stepIndex--)
		{
			string[] parts = steps[stepIndex].Split(' ');
			switch (parts[0])
			{
				case "swap":
					if (parts[1] == "position")
					{
						SwapPosition(int.Parse(parts[2]), int.Parse(parts[5]));
					}
					else
					{
						SwapLetter(parts[2][0], parts[5][0]);
					}
					break;
				case "rotate":
					if (parts[1] == "left")
					{
						Rotate("right", int.Parse(parts[2]));
					}
					else if (parts[1] == "right")
					{
						Rotate("left", int.Parse(parts[2]));
					}
					else
					{
						int index = FindIndex(parts[6][0]);
						if (index == 6)
						{
							break;
						}
						(string direction, int distance) = reverseRotate[index];
						Rotate(direction, distance);
					}
					break;
				case "reverse":
					int start = int.Parse(parts[2]);
					int end = int.Parse(parts[4]);
					for (int i = start, j = end; i < j; i++, j--)
					{
						SwapPosition(i, j);
					}
					break;
				case "move":
					Move(int.Parse(parts[5]), int.Parse(parts[2]));
					break;				
			}
		}
		return ToString();
	}

	private Node? FindNode(char searchValue)
	{
		Node cursor = head!;
		do
		{
			if (cursor.Value == searchValue)
			{
				return cursor;
			}
			cursor = cursor.Next!;
		} while (cursor != head);
		return null;
	}

	private int FindIndex(char searchValue)
	{
		Node cursor = head!;
		int index = 0;
		do
		{
			if (cursor.Value == searchValue)
			{
				return index;
			}
			cursor = cursor.Next!;
			index++;
		} while (cursor != head);
		return -1;
	}

	private void SwapPosition(int first, int second)
	{
		Node firstCursor = head!;
		for (int i = 1; i <= first; i++)
		{
			firstCursor = firstCursor.Next!;
		}
		Node secondCursor = head!;
		for (int i = 1; i <= second; i++)
		{
			secondCursor = secondCursor.Next!;
		}
		char temp = firstCursor.Value;
		firstCursor.Value = secondCursor.Value;
		secondCursor.Value = temp;
	}

	private void SwapLetter(char first, char second)
	{
		Node firstCursor = FindNode(first)!;
		Node secondCursor = FindNode(second)!;
		firstCursor.Value = second;
		secondCursor.Value = first;
	}

	private void Rotate(string direction, int steps)
	{
		if (direction == "left")
		{
			for (int i = 1; i <= steps; i++)
			{
				head = head!.Next!;
			}
		}
		else
		{
			for (int i = 1; i <= steps; i++)
			{
				head = head!.Previous!;
			}
		}	
	}

	private void Move(int toMoveIndex, int insertionPointIndex)
	{
		Node toMoveNode = head!;
		for (int i = 1; i <= toMoveIndex; i++)
		{
			toMoveNode = toMoveNode.Next!;
		}
		Node insertionPointNode = head!;		
		for (int i = 1; i <= insertionPointIndex; i++)
		{
			insertionPointNode = insertionPointNode.Next!;
		}
		if (toMoveIndex == 0)
		{
			head = head!.Next!;
		}
		if (insertionPointIndex == 0)
		{
			head = toMoveNode;
		}
		Node toMovePrevious = toMoveNode.Previous!;
		Node toMoveNext = toMoveNode.Next!;
		toMovePrevious.Next = toMoveNext;
		toMoveNext.Previous = toMovePrevious;
		Node insertionPointNext = insertionPointNode.Next!;
		Node insertionPointPrevious = insertionPointNode.Previous!;
		if (toMoveIndex < insertionPointIndex)
		{
			insertionPointNode.Next = toMoveNode;
			insertionPointNext.Previous = toMoveNode;			
			toMoveNode.Previous = insertionPointNode;
			toMoveNode.Next = insertionPointNext;
		}
		else
		{
			insertionPointNode.Previous = toMoveNode;
			insertionPointPrevious.Next = toMoveNode;
			toMoveNode.Previous = insertionPointPrevious;
			toMoveNode.Next = insertionPointNode;				
		}
	}

	public override string ToString()
	{
		StringBuilder builder = new ();
		Node cursor = head!;
		do
		{
			builder.Append(cursor.Value);
			cursor = cursor.Next!;
		} while (cursor != head);
		return builder.ToString();
	}

	private class Node
	{
		public char Value { get; set; }
		public Node? Next { get; set; }
		public Node? Previous { get; set; }		
	}
}