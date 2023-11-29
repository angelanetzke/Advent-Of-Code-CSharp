using System.Text.RegularExpressions;

namespace Dec18
{
	internal class PairTree
	{
		private readonly PairNode root;

		public PairTree()
		{
			root = new();
		}
		public PairTree(string pairString) : this(pairString, null)
		{ }

		public PairTree(string pairString, PairNode parent)
		{
			Regex numberRegex = new(@"^\d+$");
			if (numberRegex.IsMatch(pairString))
			{
				root = new(int.Parse(pairString));
				root.parent = parent;
			}
			else
			{
				root = new();
				root.parent = parent;
				if (pairString.StartsWith('[') && pairString.EndsWith(']'))
				{
					pairString = pairString.Substring(1, pairString.Length - 2);
				}				
				int index = GetSplitIndex(pairString);
				string leftString = pairString.Substring(0, index);
				root.left = (new PairTree(leftString, root)).GetRoot();
				string rightString = pairString.Substring(index + 1);
				root.right = (new PairTree(rightString, root)).GetRoot();
			}
		}

		public PairNode GetRoot()
		{
			return root;
		}

		private static int GetSplitIndex(string oldString)
		{
			int index = 0;
			int openBrackets = 0;
			char thisChar;
			do
			{
				if (oldString[index] == '[')
				{
					openBrackets++;
				}
				else if (oldString[index] == ']'){
					openBrackets--;
				}
				index++;
				thisChar = oldString[index];
								
			} while (openBrackets > 0 || thisChar != ',');
			return index;
		}

		public PairTree Add(PairTree other)
		{
			PairTree newTree = new();
			PairNode newRoot = newTree.GetRoot();
			newRoot.left = GetRoot();
			newRoot.left.parent = newRoot;
			newRoot.right = other.GetRoot();
			newRoot.right.parent = newRoot;
			return newTree;
		}

		public bool Explode()
		{
			return root.Explode(0);
		}

		public bool Split()
		{
			return root.Split();
		}

		public long GetMagnitude()
		{
			return root.GetMagnitude();
		}

		public override string ToString()
		{
			return root.ToString();
		}


	}
}
