namespace MatchEngine
{
	public class TileData
	{
		public readonly int X;
		public readonly int Y;

		public readonly int TypeId;
		public readonly int TextId;

		public TileData(int x, int y, int typeId, int textId)
		{
			X = x;
			Y = y;

			TypeId = typeId;
			TextId = textId;
		}
	}
}
