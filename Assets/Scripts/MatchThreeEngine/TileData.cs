namespace MatchEngine
{
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

		public readonly int TileId;
		public readonly int TextId;
		public readonly LogicType Type;

		public TileData(int x, int y, int typeId, int textId, LogicType _type)
		{
			X = x;
			Y = y;

			TileId = typeId;
			TextId = textId;
			Type = _type;
		}
	}
}
