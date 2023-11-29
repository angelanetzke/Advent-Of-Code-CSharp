namespace Dec19
{
	internal class BeaconPair
	{
		public Beacon beacon1;
		public Beacon beacon2;

		public BeaconPair(Beacon beacon1, Beacon beacon2)
		{
			this.beacon1 = beacon1;
			this.beacon2 = beacon2;
		}

		public override string ToString()
		{
			return beacon1.ToString() + " " + beacon2.ToString();
		}
	}



}
