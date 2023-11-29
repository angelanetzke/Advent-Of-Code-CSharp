namespace Day13
{
	internal class Cart
	{
		private int x;
		private int y;
		private enum CartDirection { N, E, S, W };
		private CartDirection currentDirection = CartDirection.N;		
		private enum TurnDirection { LEFT, STRAIGHT, RIGHT };
		private TurnDirection nextTurn;
		public bool isRemoved { set; get; } = false;
		private static readonly Dictionary<CartDirection, (int, int)> cartMovement = new ()
		{
			{ CartDirection.N, (0, -1) },
			{ CartDirection.E, (1, 0) },
			{ CartDirection.S, (0, 1) },
			{ CartDirection.W, (-1, 0) }
		};
		private static readonly Dictionary<(CartDirection, TurnDirection), CartDirection> turnResult = new()
		{
			{ (CartDirection.N, TurnDirection.LEFT), CartDirection.W },
			{ (CartDirection.N, TurnDirection.STRAIGHT), CartDirection.N },
			{ (CartDirection.N, TurnDirection.RIGHT), CartDirection.E },
			{ (CartDirection.E, TurnDirection.LEFT), CartDirection.N },
			{ (CartDirection.E, TurnDirection.STRAIGHT), CartDirection.E },
			{ (CartDirection.E, TurnDirection.RIGHT), CartDirection.S },
			{ (CartDirection.S, TurnDirection.LEFT), CartDirection.E },
			{ (CartDirection.S, TurnDirection.STRAIGHT), CartDirection.S },
			{ (CartDirection.S, TurnDirection.RIGHT), CartDirection.W },
			{ (CartDirection.W, TurnDirection.LEFT), CartDirection.S },
			{ (CartDirection.W, TurnDirection.STRAIGHT), CartDirection.W },
			{ (CartDirection.W, TurnDirection.RIGHT), CartDirection.N }
		};
		private static readonly Dictionary<(CartDirection, char), CartDirection> curveResult = new()
		{
			{ (CartDirection.N, '/'), CartDirection.E },
			{ (CartDirection.N, '\\'), CartDirection.W },
			{ (CartDirection.E, '/'), CartDirection.N },
			{ (CartDirection.E, '\\'), CartDirection.S },
			{ (CartDirection.S, '/'), CartDirection.W },
			{ (CartDirection.S, '\\'), CartDirection.E },
			{ (CartDirection.W, '/'), CartDirection.S },
			{ (CartDirection.W, '\\'), CartDirection.N }
		};
		private static readonly Dictionary<char, CartDirection> charToDirection = new()
		{
			{ '^', CartDirection.N },
			{ '>', CartDirection.E },
			{ 'v', CartDirection.S },
			{ '<', CartDirection.W }
		};		

		public Cart(int x, int y, char directionChar)
		{
			this.x = x;
			this.y = y;
			currentDirection = charToDirection[directionChar];
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public void Update(string[] map)
		{
			var thisMovement = cartMovement[currentDirection];
			x += thisMovement.Item1;
			y += thisMovement.Item2;
			if (map[y][x] == '+')
			{
				currentDirection = turnResult[(currentDirection, nextTurn)];
				nextTurn = (TurnDirection)(((int)nextTurn + 1) % 3);
			}
			if (map[y][x] == '/' || map[y][x] == '\\')
			{
				currentDirection = curveResult[(currentDirection, map[y][x])];
			}
		}

		public string GetLocationString()
		{
			return y.ToString("D4") + x.ToString("D4");
		}
		
	}
}