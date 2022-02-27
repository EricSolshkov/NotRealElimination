﻿namespace MatchEngine
{
    public sealed class Match
    {


        public readonly int TypeId;

        public readonly int TextId;

        public readonly int Score;

        public readonly TileData[] Tiles;

        public readonly bool tileMatchFlag = false;

        public readonly bool textMatchFlag = false;

        public readonly int rightBound;

        public readonly int leftBound;

        public readonly int topBound;

        public readonly int bottomBound;


        public Match(TileData origin, TileData[] horizontal, TileData[] vertical)
        {
            TypeId = origin.TypeId;

            TextId = origin.TextId;

            rightBound = origin.X;

            leftBound = origin.X;

            topBound = origin.Y;

            bottomBound = origin.Y;

            foreach (var tile in horizontal)
            {
                rightBound = tile.X > rightBound ? tile.X : rightBound;
                leftBound = tile.X < leftBound ? tile.X : leftBound;
            }
            foreach (var tile in vertical)
            {
                topBound = tile.Y < topBound ? tile.Y : topBound;
                bottomBound = tile.Y > bottomBound ? tile.Y : bottomBound;
            }

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

            if (Tiles != null)
            {
                tileMatchFlag = true;
                textMatchFlag = true;

                foreach (var tile in Tiles)
                {
                    if (tile.TypeId != Tiles[0].TypeId) tileMatchFlag = false;
                    if (tile.TextId != Tiles[0].TextId) textMatchFlag = false;
                }

            }
            Score = Tiles?.Length ?? -1;
        }

    }
}
