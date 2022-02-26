namespace MatchEngine
{
	public enum MatchType
	{
		tile,
		text,
		both
	};
	public sealed class Match
	{
		

		public readonly int TypeId;

		public readonly int TextId;

		public readonly int Score;

		public readonly TileData[] Tiles;

		public readonly MatchType matchType;

		public Match(TileData origin, TileData[] horizontal, TileData[] vertical)
		{
			TypeId = origin.TypeId;

			TextId = origin.TextId;

			if (horizontal.Length >= 2 && vertical.Length >= 2)
			{
				Tiles = new TileData[horizontal.Length + vertical.Length + 1];

				Tiles[0] = origin;

				horizontal.CopyTo(Tiles, 1);

				vertical.CopyTo(Tiles, horizontal.Length + 1);
			}
			else if (horizontal.Length >= 2)
			{
				Tiles = new TileData[horizontal.Length + 1];

				Tiles[0] = origin;

				horizontal.CopyTo(Tiles, 1);
			}
			else if (vertical.Length >= 2)
			{
				Tiles = new TileData[vertical.Length + 1];

				Tiles[0] = origin;

				vertical.CopyTo(Tiles, 1);
			}
			else Tiles = null;

			Score = Tiles?.Length ?? -1;
		}
	}
}
