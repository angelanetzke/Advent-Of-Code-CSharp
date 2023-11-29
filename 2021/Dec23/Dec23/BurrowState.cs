using System;
using System.Collections.Generic;
using System.Text;

namespace Dec23
{
	internal class BurrowState
	{
		private Dictionary<int, char> spaces;
		private static readonly Dictionary<int, Dictionary<int, int>> costs = new()
		{
			[0] = new()
			{
				[7] = 3,
				[8] = 5,
				[9] = 7,
				[10] = 9,
				[11] = 4,
				[12] = 6,
				[13] = 8,
				[14] = 10
			},
			[1] = new()
			{
				[7] = 2,
				[8] = 4,
				[9] = 6,
				[10] = 8,
				[11] = 3,
				[12] = 5,
				[13] = 7,
				[14] = 9
			},
			[2] = new()
			{
				[7] = 2,
				[8] = 2,
				[9] = 4,
				[10] = 6,
				[11] = 3,
				[12] = 3,
				[13] = 5,
				[14] = 7
			},
			[3] = new()
			{
				[7] = 4,
				[8] = 2,
				[9] = 2,
				[10] = 4,
				[11] = 5,
				[12] = 3,
				[13] = 3,
				[14] = 5
			},
			[4] = new()
			{
				[7] = 6,
				[8] = 4,
				[9] = 2,
				[10] = 2,
				[11] = 7,
				[12] = 5,
				[13] = 3,
				[14] = 3
			},
			[5] = new()
			{
				[7] = 8,
				[8] = 6,
				[9] = 4,
				[10] = 2,
				[11] = 9,
				[12] = 7,
				[13] = 5,
				[14] = 3
			},
			[6] = new()
			{
				[7] = 9,
				[8] = 7,
				[9] = 5,
				[10] = 3,
				[11] = 10,
				[12] = 8,
				[13] = 6,
				[14] = 4
			},
			[7] = new()
			{
				[0] = 3,
				[1] = 2,
				[2] = 2,
				[3] = 4,
				[4] = 6,
				[5] = 8,
				[6] = 9
			},
			[8] = new()
			{
				[0] = 5,
				[1] = 4,
				[2] = 2,
				[3] = 2,
				[4] = 4,
				[5] = 6,
				[6] = 7
			},
			[9] = new()
			{
				[0] = 7,
				[1] = 6,
				[2] = 4,
				[3] = 2,
				[4] = 2,
				[5] = 4,
				[6] = 5
			},
			[10] = new()
			{
				[0] = 9,
				[1] = 8,
				[2] = 6,
				[3] = 4,
				[4] = 2,
				[5] = 2,
				[6] = 3
			},
			[11] = new()
			{
				[0] = 4,
				[1] = 3,
				[2] = 3,
				[3] = 5,
				[4] = 7,
				[5] = 9,
				[6] = 10
			},
			[12] = new()
			{
				[0] = 6,
				[1] = 5,
				[2] = 3,
				[3] = 3,
				[4] = 5,
				[5] = 7,
				[6] = 8
			},
			[13] = new()
			{
				[0] = 8,
				[1] = 7,
				[2] = 5,
				[3] = 3,
				[4] = 3,
				[5] = 5,
				[6] = 6
			},
			[14] = new()
			{
				[0] = 10,
				[1] = 9,
				[2] = 7,
				[3] = 5,
				[4] = 3,
				[5] = 3,
				[6] = 4
			}
		};
		private static readonly Dictionary<char, List<int>> legalMoves = new()
		{
			['A'] = new() { 0, 1, 2, 3, 4, 5, 6, 7, 11 },
			['B'] = new() { 0, 1, 2, 3, 4, 5, 6, 8, 12 },
			['C'] = new() { 0, 1, 2, 3, 4, 5, 6, 9, 13 },
			['D'] = new() { 0, 1, 2, 3, 4, 5, 6, 10, 14 }
		};

		public BurrowState()
		{
			spaces = new()
			{
				[0] = '.',
				[1] = '.',
				[2] = '.',
				[3] = '.',
				[4] = '.',
				[5] = '.',
				[6] = '.',
				[7] = '.',
				[8] = '.',
				[9] = '.',
				[10] = '.',
				[11] = '.',
				[12] = '.',
				[13] = '.',
				[14] = '.'
			};
		}

		public BurrowState Copy()
		{
			BurrowState newBurrowState = new();
			foreach (int thisSpace in spaces.Keys)
			{
				newBurrowState.SetCharacter(thisSpace, spaces[thisSpace]);
			}
			return newBurrowState;
		}

		public char GetCharacter(int space)
		{
			return spaces[space];
		}

		public void SetCharacter(int space, char character)
		{
			spaces[space] = character;
		}

		public int GetCost(char character, int from, int to)
		{
			if (GetCharacter(to) != '.')
			{
				return -1;
			}
			if (from == to)
			{
				return -1;
			}
			if ((to == 7 || to == 11) && !CanMoveIntoRoom('A'))
			{
				return -1;
			}
			if ((to == 8 || to == 12) && !CanMoveIntoRoom('B'))
			{
				return -1;
			}
			if ((to == 9 || to == 13) && !CanMoveIntoRoom('C'))
			{
				return -1;
			}
			if ((to == 10 || to == 14) && !CanMoveIntoRoom('D'))
			{
				return -1;
			}
			if (to == 7 && !IsFarthestBack(7))
			{
				return -1;
			}
			if (to == 8 && !IsFarthestBack(8))
			{
				return -1;
			}
			if (to == 9 && !IsFarthestBack(9))
			{
				return -1;
			}
			if (to == 10 && !IsFarthestBack(10))
			{
				return -1;
			}
			if (!HasClearPath(from, to))
			{
				return -1;
			}
			if (character == 'A')
			{
				if (from == 7 && GetCharacter(11) == 'A')
				{
					return -1;
				}
				if (from == 11)
				{
					return -1;
				}
			}
			if (character == 'B')
			{
				if (from == 8 && GetCharacter(12) == 'B')
				{
					return -1;
				}
				if (from == 12)
				{
					return -1;
				}
			}
			if (character == 'C')
			{
				if (from == 9 && GetCharacter(13) == 'C')
				{
					return -1;
				}
				if (from == 13)
				{
					return -1;
				}
			}
			if (character == 'D')
			{
				if (from == 10 && GetCharacter(14) == 'D')
				{
					return -1;
				}
				if (from == 14)
				{
					return -1;
				}
			}
			if (costs.TryGetValue(from, out Dictionary<int, int> costMap))
			{
				if (costMap.TryGetValue(to, out int cost))
				{
					if (character == 'A')
					{
						return cost;
					}
					else if (character == 'B')
					{
						return 10 * cost;
					}
					else if (character == 'C')
					{
						return 100 * cost;
					}
					else
					{
						return 1000 * cost;
					}
				}
			}
			return -1;
		}

		private bool CanMoveIntoRoom(char roomChar)
		{
			return roomChar switch
			{
				'A' => (GetCharacter(7) == '.' && GetCharacter(11) == 'A')
					|| (GetCharacter(7) == '.' && GetCharacter(11) == '.'),
				'B' => (GetCharacter(8) == '.' && GetCharacter(12) == 'B')
					|| (GetCharacter(8) == '.' && GetCharacter(12) == '.'),
				'C' => (GetCharacter(9) == '.' && GetCharacter(13) == 'C')
					|| (GetCharacter(9) == '.' && GetCharacter(13) == '.'),
				'D' => (GetCharacter(10) == '.' && GetCharacter(14) == 'D')
					|| (GetCharacter(10) == '.' && GetCharacter(14) == '.'),
				_ => false
			};
		}

		private bool IsFarthestBack(int space)
		{
			return space switch
			{
				7 => GetCharacter(11) == 'A',
				8 => GetCharacter(12) == 'B',
				9 => GetCharacter(13) == 'C',
				10 => GetCharacter(14) == 'D',
				_ => false
			};
		}

		private bool HasClearPath(int from, int to)
		{
			switch(from)
			{
				case 0:
					if (to == 7 || to == 11)
					{
						return GetCharacter(1) == '.';
					}
					else if (to == 8 || to == 12)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.';
					}
					else if (to == 9 || to == 13)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(3) == '.';
					}
					else if (to == 10 || to == 14)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					break;
				case 1:
					if (to == 7 || to == 11)
					{
						return true;
					}
					else if (to == 8 || to == 12)
					{
						return GetCharacter(2) == '.';
					}
					else if (to == 9 || to == 13)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.';
					}
					else if (to == 10 || to == 14)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					break;
				case 2:
					if (to == 7 || to == 11)
					{
						return true;
					}
					else if (to == 8 || to == 12)
					{
						return true;
					}
					else if (to == 9 || to == 13)
					{
						return GetCharacter(3) == '.';
					}
					else if (to == 10 || to == 14)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					break;
				case 3:
					if (to == 7 || to == 11)
					{
						return GetCharacter(2) == '.';
					}
					else if (to == 8 || to == 12)
					{
						return true;
					}
					else if (to == 9 || to == 13)
					{
						return true;
					}
					else if (to == 10 || to == 14)
					{
						return GetCharacter(4) == '.';
					}
					break;
				case 4:
					if (to == 7 || to == 11)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.';
					}
					else if (to == 8 || to == 12)
					{
						return GetCharacter(3) == '.';
					}
					else if (to == 9 || to == 13)
					{
						return true;
					}
					else if (to == 10 || to == 14)
					{
						return true;
					}
					break;
				case 5:
					if (to == 7 || to == 11)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 8 || to == 12)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 9 || to == 13)
					{
						return GetCharacter(4) == '.';
					}
					else if (to == 10 || to == 14)
					{
						return true;
					}
					break;
				case 6:
					if (to == 7 || to == 11)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(5) == '.';
					}
					else if (to == 8 || to == 12)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(5) == '.';
					}
					else if (to == 9 || to == 13)
					{
						return GetCharacter(4) == '.'
							&& GetCharacter(5) == '.';
					}
					else if (to == 10 || to == 14)
					{
						return GetCharacter(5) == '.';
					}
					break;
				case 7:
					if (to == 0)
					{
						return GetCharacter(1) == '.';
					}
					else if (to == 1)
					{
						return true;
					}
					else if (to == 2)
					{
						return true;
					}
					else if (to == 3)
					{
						return GetCharacter(2) == '.';
					}
					else if (to == 4)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.';
					}
					else if (to == 5)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(5) == '.';
					}
					break;
				case 8:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(2) == '.';
					}
					else if (to == 2)
					{
						return true;
					}
					else if (to == 3)
					{
						return true;
					}
					else if (to == 4)
					{
						return GetCharacter(3) == '.';
					}
					else if (to == 5)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(5) == '.';
					}
					break;
				case 9:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(3) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.';
					}
					else if (to == 2)
					{
						return GetCharacter(3) == '.';
					}
					else if (to == 3)
					{
						return true;
					}
					else if (to == 4)
					{
						return true;
					}
					else if (to == 5)
					{
						return GetCharacter(4) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(4) == '.'
							&& GetCharacter(5) == '.';
					}
					break;
				case 10:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 2)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.';
					}
					else if (to == 3)
					{
						return GetCharacter(4) == '.';
					}
					else if (to == 4)
					{
						return true;
					}
					else if (to == 5)
					{
						return true;
					}
					else if (to == 6)
					{
						return GetCharacter(5) == '.';
					}
					break;
				case 11:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(7) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(7) == '.';
					}
					else if (to == 2)
					{
						return GetCharacter(7) == '.';
					}
					else if (to == 3)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(7) == '.';
					}
					else if (to == 4)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(7) == '.';
					}
					else if (to == 5)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(7) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(5) == '.'
							&& GetCharacter(7) == '.';
					}
					break;
				case 12:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(8) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(8) == '.';
					}
					else if (to == 2)
					{
						return GetCharacter(8) == '.';
					}
					else if (to == 3)
					{
						return GetCharacter(8) == '.';
					}
					else if (to == 4)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(8) == '.';
					}
					else if (to == 5)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(8) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(5) == '.'
							&& GetCharacter(8) == '.';
					}
					break;
				case 13:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(9) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(9) == '.';
					}
					else if (to == 2)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(9) == '.';
					}
					else if (to == 3)
					{
						return GetCharacter(9) == '.';
					}
					else if (to == 4)
					{
						return GetCharacter(9) == '.';
					}
					else if (to == 5)
					{
						return GetCharacter(4) == '.'
							&& GetCharacter(9) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(4) == '.'
							&& GetCharacter(5) == '.'
							&& GetCharacter(9) == '.';
					}
					break;
				case 14:
					if (to == 0)
					{
						return GetCharacter(1) == '.'
							&& GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(10) == '.';
					}
					else if (to == 1)
					{
						return GetCharacter(2) == '.'
							&& GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(10) == '.';
					}
					else if (to == 2)
					{
						return GetCharacter(3) == '.'
							&& GetCharacter(4) == '.'
							&& GetCharacter(10) == '.';
					}
					else if (to == 3)
					{
						return GetCharacter(4) == '.'
							&& GetCharacter(10) == '.';
					}
					else if (to == 4)
					{
						return GetCharacter(10) == '.';
					}
					else if (to == 5)
					{
						return GetCharacter(10) == '.';
					}
					else if (to == 6)
					{
						return GetCharacter(5) == '.'
							&& GetCharacter(10) == '.';
					}
					break;
			}
			return false;
		}

		public bool IsComplete()
		{
			if (GetCharacter(7) != 'A' || GetCharacter(11) != 'A')
			{
				return false;
			}
			if (GetCharacter(8) != 'B' || GetCharacter(12) != 'B')
			{
				return false;
			}
			if (GetCharacter(9) != 'C' || GetCharacter(13) != 'C')
			{
				return false;
			}
			if (GetCharacter(10) != 'D' || GetCharacter(14) != 'D')
			{
				return false;
			}
			return true;
		}

		public Dictionary<int, int> GetNextSpaces(int space, char character)
		{
			if (IsComplete())
			{
				return null;
			}
			else
			{
				Dictionary<int, int> nextSpaces = new();
				foreach (int thisLegalMove in legalMoves[character])
				{
					int costToNext = GetCost(character, space, thisLegalMove);
					if (costToNext > -1)
					{
						nextSpaces[thisLegalMove] = costToNext;
					}
				}
				return nextSpaces;
			}
		}

		public Dictionary<BurrowState, int> GetNext()
		{
			Dictionary<BurrowState, int> nextStates = new();
			List<char> currentCharacters = new();
			List<int> currentSpaces = new();
			foreach (int thisSpace in spaces.Keys)
			{
				if (spaces[thisSpace] != '.')
				{
					currentCharacters.Add(spaces[thisSpace]);
					currentSpaces.Add(thisSpace);
				}
			}
			for (int i = 0; i < currentCharacters.Count; i++)
			{
				Dictionary<int, int> nextSpacesForThisCharacter
					= GetNextSpaces(currentSpaces[i], currentCharacters[i]);
				if (nextSpacesForThisCharacter != null)
				{
					foreach (int thisNextSpace in nextSpacesForThisCharacter.Keys)
					{
						BurrowState newBurrowState = this.Copy();
						newBurrowState.SetCharacter(currentSpaces[i], '.');
						newBurrowState.SetCharacter(thisNextSpace, currentCharacters[i]);
						nextStates[newBurrowState] = nextSpacesForThisCharacter[thisNextSpace];
					}
				}
			}
			return nextStates;
		}

		public override bool Equals(object obj)
		{
			if (obj is BurrowState other)
			{
				foreach (int thisKey in spaces.Keys)
				{
					if (spaces[thisKey] != other.spaces[thisKey])
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = 17;
			foreach (int thisKey in spaces.Keys)
			{
				hashCode = HashCode.Combine(hashCode, spaces[thisKey]);
			}
			return hashCode;
		}

		public override string ToString()
		{
			StringBuilder builder = new();
			builder.Append("#############\n");
			builder.Append('#');
			builder.Append(GetCharacter(0));
			builder.Append(GetCharacter(1));
			builder.Append('.');
			builder.Append(GetCharacter(2));
			builder.Append('.');
			builder.Append(GetCharacter(3));
			builder.Append('.');
			builder.Append(GetCharacter(4));
			builder.Append('.');
			builder.Append(GetCharacter(5));
			builder.Append(GetCharacter(6));
			builder.Append("#\n");
			builder.Append("###");
			builder.Append(GetCharacter(7));
			builder.Append('#');
			builder.Append(GetCharacter(8));
			builder.Append('#');
			builder.Append(GetCharacter(9));
			builder.Append('#');
			builder.Append(GetCharacter(10));
			builder.Append("###\n");
			builder.Append("  #");
			builder.Append(GetCharacter(11));
			builder.Append('#');
			builder.Append(GetCharacter(12));
			builder.Append('#');
			builder.Append(GetCharacter(13));
			builder.Append('#');
			builder.Append(GetCharacter(14));
			builder.Append("#\n");
			builder.Append("  #########\n");
			return builder.ToString();
		}


	}
}
