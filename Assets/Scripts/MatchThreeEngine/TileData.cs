namespace MatchEngine
{
	public enum TileName
    {
		hydro,
		pyro,
		dendro,
		photo,
		skoto
    }
	public enum Text
	{
		nullText,
		isText,
		down,
		left,
		right,
		up,
		num3,
		num4,
		num5,
		crush
	}
	public enum LogicType
	{
		pure,
		obj,
		op,
		prop,
		prep,
		func
	}

	public class TileData
	{
		public readonly int X;
		public readonly int Y;

		public readonly TileName tileName;
		public readonly Text text;
		public readonly LogicType logicType;

		public TileData(int x, int y, TileName typeId, Text textId, LogicType _type)
		{
			X = x;
			Y = y;

			tileName = typeId;
			text = textId;
			logicType = _type;
		}
	}
}
