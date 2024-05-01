namespace Day19;

internal class ElfCircle
{
	private readonly Elf? firstElf;
	private readonly Elf? firstElfToRemove;
	private int size;

	public ElfCircle(int size)
	{
		firstElf = new(1);
		Elf lastElf = firstElf;
		for (int i = 2; i <= size; i++)
		{
			Elf newElf = new (i);
			if (i == size / 2  + 1)
			{
				firstElfToRemove = newElf;
			}
			newElf.Previous = lastElf;
			lastElf.Next = newElf;			
			lastElf = newElf;			
		}
		firstElf.Previous = lastElf;
		lastElf.Next = firstElf;
		this.size = size;
	}

	public int RunGame()
	{
		Elf? currentElf = firstElf;
		while (size > 1)
		{
			Elf elfToRemove = currentElf!.Next!;
			currentElf.PresentCount += elfToRemove.PresentCount;
			elfToRemove.Previous!.Next = elfToRemove.Next;
			elfToRemove.Next!.Previous = elfToRemove.Previous;
			currentElf = currentElf.Next;
			size--;
		}
		return currentElf!.ID;
	}

	public int RunGame2()
	{
		Elf? currentElf = firstElf;
		Elf elfToRemove = firstElfToRemove!;
		while (size > 1)
		{
			currentElf!.PresentCount += elfToRemove.PresentCount;
			elfToRemove.Previous!.Next = elfToRemove.Next;
			elfToRemove.Next!.Previous = elfToRemove.Previous;
			currentElf = currentElf.Next;
			if (size % 2 == 0)
			{
				elfToRemove = elfToRemove.Next!;
			}
			else
			{
				elfToRemove = elfToRemove.Next!.Next!;
			}
			size--;
		}
		return currentElf!.ID;
	}

	private class Elf
	{
		public readonly int ID; 
		public int PresentCount { get; set; } = 1;
		public Elf? Previous { get; set; } = null;
		public Elf? Next { get; set; } = null;

		public Elf(int id)
		{
			ID = id;
		}
		
	}
}