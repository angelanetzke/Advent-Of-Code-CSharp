using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec22
{
	internal class Cuboid
	{
		private readonly int minX;
		private readonly int maxX;
		private readonly int minY;
		private readonly int maxY;
		private readonly int minZ;
		private readonly int maxZ;

		public Cuboid(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
		{
			this.minX = minX;
			this.maxX = maxX;
			this.minY = minY;
			this.maxY = maxY;
			this.minZ = minZ;
			this.maxZ = maxZ;
		}

		public bool Intersects(Cuboid other)
		{
			if (maxX < other.minX) {
				return false;
			}
			else if (minX > other.maxX) {
				return false;
			}
			else if (maxY < other.minY)
			{
				return false;
			}
			else if (minY > other.maxY)
			{
				return false;
			}
			else if (maxZ < other.minZ)
			{
				return false;
			}
			else if (minZ > other.maxZ)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public Cuboid GetIntersection(Cuboid other)
		{
			int startX = Math.Max(minX, other.minX);
			int endX = Math.Min(maxX, other.maxX);
			int startY = Math.Max(minY, other.minY);
			int endY = Math.Min(maxY, other.maxY);
			int startZ = Math.Max(minZ, other.minZ);
			int endZ = Math.Min(maxZ, other.maxZ);
			return new Cuboid(startX, endX, startY, endY, startZ, endZ);
		}

		public long CountCubes()
		{
			return (long)(maxX - minX + 1) * (maxY - minY + 1) * (maxZ - minZ + 1);
		}

		public override bool Equals(object obj)
		{
			return obj is Cuboid cuboid &&
						 minX == cuboid.minX &&
						 maxX == cuboid.maxX &&
						 minY == cuboid.minY &&
						 maxY == cuboid.maxY &&
						 minZ == cuboid.minZ &&
						 maxZ == cuboid.maxZ;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(minX, maxX, minY, maxY, minZ, maxZ);
		}


	}
}
