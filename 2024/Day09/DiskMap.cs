namespace Day09;

internal class DiskMap
{
	private readonly Node head;
	private readonly Node tail;

	public DiskMap(string data)
	{
		head = new ();
		Node cursor = head;
		tail = cursor;
		bool isFile = true;
		int position = 0;
		int fileNumber = 0;
		for (int i = 0; i < data.Length; i++)
		{
			cursor.StartPosition = position;
			int size = int.Parse(data[i].ToString());
			cursor.Size = size;
			cursor.StartPosition = position;
			if (isFile)
			{
				cursor.FileNumber = fileNumber;
				fileNumber++;
			}
			else
			{
				cursor.FileNumber = null;
			}
			if (i < data.Length - 1)
			{
				cursor.Next = new()
				{
					Previous = cursor,
					Next = null
				};
				cursor = cursor.Next;
				tail = cursor;
			}
			position += size;
			isFile = !isFile;
		}
	}

	public void Sort()
	{
		Node fileToMoveCursor = tail;
		while (fileToMoveCursor != head)
		{
			if (fileToMoveCursor.FileNumber == null)
			{
				fileToMoveCursor = fileToMoveCursor.Previous!;
				continue;
			}
			Node newLocationCursor = head;
			while (newLocationCursor != fileToMoveCursor)
			{
				if (newLocationCursor.FileNumber != null)
				{
					newLocationCursor = newLocationCursor.Next!;
					continue;
				}
				if (fileToMoveCursor.Size == newLocationCursor.Size)
				{
					newLocationCursor.FileNumber = fileToMoveCursor.FileNumber;
					fileToMoveCursor.FileNumber = null;
					break;
				}
				else if (fileToMoveCursor.Size < newLocationCursor.Size)
				{
					int insertedFileSize = fileToMoveCursor.Size;
					int remainingEmptySize = newLocationCursor.Size - insertedFileSize;
					Node A = newLocationCursor;
					Node B = new ();
					Node C = A.Next!;
					A.Next = B;
					B.Previous = A;
					B.Next = C;
					C.Previous = B;
					B.StartPosition = A.StartPosition + insertedFileSize;
					A.Size = insertedFileSize;
					B.Size = remainingEmptySize;
					A.FileNumber = fileToMoveCursor.FileNumber;
					fileToMoveCursor.FileNumber = null;
					B.FileNumber = null;
					break;					
				}
				newLocationCursor = newLocationCursor.Next!;
			}
			fileToMoveCursor = fileToMoveCursor.Previous!;
		}
	}

	public long GetCheckSum()
	{
		long checkSum = 0L;
		Node cursor = head;
		while (cursor != null && cursor.Next != null)
		{
			if (cursor.FileNumber == null)
			{
				cursor = cursor.Next;
				continue;
			}
			for (int i = 0; i < cursor.Size; i++)
			{
				checkSum += (long)((cursor.StartPosition + i) * cursor.FileNumber);	
			}
			cursor = cursor.Next;
		}
		return checkSum;
	}

	private class Node
	{
		public int StartPosition {set; get;}
		public int Size {set; get;}
		public int? FileNumber {set; get;}
		public Node? Previous {set; get;}
		public Node? Next {set; get;}
	}
}