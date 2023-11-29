namespace Dec18
{
	internal class PairNode
	{
		public bool isLeaf;
		public int value;
		public PairNode parent;
		public PairNode left;
		public PairNode right;
		public PairNode()
		{
			isLeaf = false;
			value = -1;
		}

		public PairNode(int value)
		{
			isLeaf = true;
			this.value = value;
		}

		public bool Explode(int level)
		{
			if (isLeaf)
			{
				return false;
			}
			else if (left.Explode(level + 1))
			{
				return true;
			}
			else if (right.Explode(level + 1))
			{
				return true;
			}
			else if (left.isLeaf && right.isLeaf && level >= 4)
			{
				if (!left.IsLeftmostNode())
				{
					PairNode childNode = this;
					PairNode addNode = childNode.parent;
					while (addNode.left == childNode)
					{
						childNode = childNode.parent;
						addNode = childNode.parent;
					}
					addNode.left.AddRight(left.value);
				}
				if (!right.IsRightmostNode())
				{
					PairNode childNode = this;
					PairNode addNode = childNode.parent;
					while (addNode.right == childNode )
					{
						childNode = childNode.parent;
						addNode = childNode.parent;
					}
					addNode.right.AddLeft(right.value);
				}				
				left = null;
				right = null;
				value = 0;
				isLeaf = true;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Split()
		{
			if (!isLeaf)
			{
				if (left.Split())
				{
					return true;
				}
				else
				{
					return right.Split();
				}
			}
			else
			{
				if (value >= 10)
				{
					if (value % 2 == 0)
					{
						left = new(value / 2);
						right = new(value / 2);
					}
					else
					{
						left = new(value / 2);
						right = new(value / 2 + 1);
					}
					left.parent = this;
					right.parent = this;
					isLeaf = false;
					value = -1;
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public void AddLeft(int newValue)
		{
			if (isLeaf)
			{
				value += newValue;
			}
			else if (left.isLeaf)
			{
				left.value += newValue;
			}
			else
			{
				left.AddLeft(newValue);
			}
		}

		public void AddRight(int newValue)
		{
			if (isLeaf)
			{
				value += newValue;
			}
			else if (right.isLeaf)
			{
				right.value += newValue;
			}
			else
			{
				right.AddRight(newValue);
			}
		}

		public bool IsLeftmostNode()
		{
			PairNode checkNode = this;
			while (checkNode.parent != null)
			{
				if (checkNode.parent.left != checkNode)
				{
					return false;
				}
				checkNode = checkNode.parent;
			}
			return true;
		}

		public bool IsRightmostNode()
		{
			PairNode checkNode = this;
			while (checkNode.parent != null)
			{
				if (checkNode.parent.right != checkNode)
				{
					return false;
				}
				checkNode = checkNode.parent;
			}
			return true;
		}

		public long GetMagnitude()
		{
			if (isLeaf)
			{
				return value;
			}
			else
			{
				return 3 * left.GetMagnitude() + 2 * right.GetMagnitude();
			}
		}

		public override string ToString()
		{
			if (isLeaf)
			{
				return value.ToString();
			}
			else
			{
				return "[" + left.ToString() + "," + right.ToString() + "]";
			}
		}


	}
}
