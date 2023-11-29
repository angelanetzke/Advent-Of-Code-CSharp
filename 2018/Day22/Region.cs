namespace Day22;

internal class Region : IComparable<Region>
{
	private enum RegionType { Rocky, Wet, Narrow };
	private enum ToolType { Torch, CimbingGear, Neither };
	private static readonly Dictionary<RegionType, List<ToolType>> validTools = new()
	{
		{ RegionType.Rocky, new List<ToolType>() {ToolType.CimbingGear, ToolType.Torch} },
		{ RegionType.Wet, new List<ToolType>() {ToolType.CimbingGear, ToolType.Neither} },
		{ RegionType.Narrow, new List<ToolType>() {ToolType.Torch, ToolType.Neither} }
	};
	private static readonly (int, int)[] offsets 
		= { (-1, 0), (1, 0), (0, -1), (0, 1) };
	private readonly int toolSwitchingTime = 7;
	private int distanceToHere = int.MaxValue;
	public readonly (int x, int y) location;
	private readonly RegionType regionType;
	private readonly ToolType toolType;

	public Region() : this((0, 0), ToolType.Torch)
	{	
		distanceToHere = 0;
	}

	private Region((int x, int y) location, ToolType toolType)
	{
		this.location = location;
		this.toolType = toolType;
		regionType = (RegionType)(Geology.GetErosionLevel(location.x, location.y) % 3);
	}

	public int GetDistanceToHere()
	{
		return distanceToHere;
	}

	public List<Region> GetNeighbors()
	{
		var neighborList = new List<Region>();
		var altToolNeighbor = new Region(location, GetAlternateTool(regionType, toolType));
		altToolNeighbor.distanceToHere = distanceToHere + toolSwitchingTime;
		neighborList.Add(altToolNeighbor);
		foreach ((int x, int y) thisOffset in offsets)
		{
			(int x, int y) neighborLocation = (location.x + thisOffset.x, location.y + thisOffset.y);
			if (neighborLocation.x < 0 || neighborLocation.y < 0)
			{
				continue;
			}
			var neighborRegionType = (RegionType)(Geology.GetErosionLevel(neighborLocation.x, neighborLocation.y) % 3);
			if (validTools[neighborRegionType].Contains(toolType))
			{
				var thisNeighbor = new Region(neighborLocation, toolType);
				thisNeighbor.distanceToHere = distanceToHere + 1;
				neighborList.Add(thisNeighbor);
			}
		}
		return neighborList;
	}

	private static ToolType GetAlternateTool(RegionType regionType, ToolType currentToolType)
	{
		return validTools[regionType].Where(x => x != currentToolType).First();
	}

	public bool IsEnd()
	{
		return location == Geology.targetLocation && toolType == ToolType.Torch;
	}

	public int CompareTo(Region? other)
	{
		if (other == null)
		{
			return -1;
		}
		return distanceToHere.CompareTo(other.distanceToHere);
	}

	public override bool Equals(object? obj)
	{
		if (obj != null && obj is Region other)
		{
			return toolType == other.toolType && location == other.location;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (toolType.GetHashCode() + location.GetHashCode()).GetHashCode();
	}
}