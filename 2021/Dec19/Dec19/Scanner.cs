using System.Collections.Generic;

namespace Dec19
{
	internal class Scanner
	{
		private readonly List<Beacon> beaconList;
		private int x;
		private int y;
		private int z;

		public Scanner()
		{
			beaconList = new();
			x = 0;
			y = 0;
			z = 0;
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public int GetZ()
		{
			return z;
		}

		public void AddBeacon(string coordinatesString)
		{
			string[] coordinatesArray = coordinatesString.Split(",");
			int x = int.Parse(coordinatesArray[0]);
			int y = int.Parse(coordinatesArray[1]);
			int z = int.Parse(coordinatesArray[2]);
			beaconList.Add(new Beacon(x, y, z));
		}

		public void CalculateDistances()
		{
			for (int i = 0; i < beaconList.Count; i++)
			{
				for (int j = 0; j < beaconList.Count; j++)
				{
					if (i != j)
					{
						beaconList[i].AddDistance(beaconList[j]);
					}
				}
			}
		}

		public int CountMatchingBeacons(Scanner other)
		{
			return GetMatchingBeacons(other).Count;
		}

		public List<BeaconPair> GetMatchingBeacons(Scanner other)
		{
			List<BeaconPair> matches = new();
			foreach (Beacon beacon1 in beaconList)
			{
				foreach (Beacon beacon2 in other.beaconList)
				{
					if (beacon1.CountDistanceMatches(beacon2) >= 11)
					{
						matches.Add(new BeaconPair(beacon1, beacon2));
					}
				}
			}
			return matches;
		}

		public int GetBeaconCount()
		{
			return beaconList.Count;
		}

		public void Translate(int xDifference, int yDifference, int zDifference, int rotationOption)
		{
			int[] newValues = Rotate(x, y, z, rotationOption);
			x = newValues[0] - xDifference;
			y = newValues[1] - yDifference;
			z = newValues[2] - zDifference;
			foreach (Beacon thisBeacon in beaconList)
			{
				thisBeacon.Translate(xDifference, yDifference, zDifference, rotationOption);
			}
		}

		public bool TranslateOther(Scanner other)
		{
			List<BeaconPair> matches = GetMatchingBeacons(other);
			if (matches.Count < 12)
			{
				return false;
			}
			else
			{
				Beacon b0A = matches[0].beacon1;
				Beacon b0B = matches[0].beacon2;
				Beacon b1A = matches[1].beacon1;
				Beacon b1B = matches[1].beacon2;
				bool rotationFound;
				int xDifference = 0;
				int yDifference = 0;
				int zDifference = 0;
				int option = 0;
				do
				{
					rotationFound = true;
					int[] newValues1 = Rotate(b0B.GetX(), b0B.GetY(), b0B.GetZ(), option);
					int[] newValues2 = Rotate(b1B.GetX(), b1B.GetY(), b1B.GetZ(), option);
					if (newValues1[0] - b0A.GetX() == newValues2[0] - b1A.GetX())
					{
						xDifference = newValues1[0] - b0A.GetX();
					}
					else
					{
						rotationFound = false;
					}
					if (newValues1[1] - b0A.GetY() == newValues2[1] - b1A.GetY())
					{
						yDifference = newValues1[1] - b0A.GetY();
					}
					else
					{
						rotationFound = false;
					}
					if (newValues1[2] - b0A.GetZ() == newValues2[2] - b1A.GetZ())
					{
						zDifference = newValues1[2] - b0A.GetZ();
					}
					else
					{
						rotationFound = false;
					}
					if (!rotationFound)
					{
						option++;
					}
				} while (!rotationFound && option <= 23);
				other.Translate(xDifference, yDifference, zDifference, option);
				return true;
			}
		}

		public static int[] Rotate(int x, int y, int z, int option)
		{
			return option switch
			{
				1 => new int[] { x, -z, y },
				2 => new int[] { x, -y, -z },
				3 => new int[] { x, z, -y },
				4 => new int[] { -x, -y, z },
				5 => new int[] { -x, -z, -y },
				6 => new int[] { -x, y, -z },
				7 => new int[] { -x, z, y },
				8 => new int[] { -z, x, -y },
				9 => new int[] { y, x, -z },
				10 => new int[] { z, x, y },
				11 => new int[] { -y, x, z },
				12 => new int[] { z, -x, -y },
				13 => new int[] { y, -x, z },
				14 => new int[] { -z, -x, y },
				15 => new int[] { -y, -x, -z },
				16 => new int[] { -y, -z, x },
				17 => new int[] { z, -y, x },
				18 => new int[] { y, z, x },
				19 => new int[] { -z, y, x },
				20 => new int[] { z, y, -x },
				21 => new int[] { -y, z, -x },
				22 => new int[] { -z, -y, -x },
				23 => new int[] { y, -z, -x },
				_ => new int[] { x, y, z }
			};
		}

	}


}
