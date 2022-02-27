﻿using System.Collections.Generic;

namespace MatchEngine
{
    public static class TileDataMatrixUtility
    {
        public static void Swap(int x1, int y1, int x2, int y2, TileData[,] tiles)
        {
            var tile1 = tiles[x1, y1];

            tiles[x1, y1] = tiles[x2, y2];

            tiles[x2, y2] = tile1;
        }


        public static (TileData[], TileData[]) GetTileConnections(int originX, int originY, TileData[,] tiles)
        {
            var origin = tiles[originX, originY];

            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            var horizontalConnections = new List<TileData>();
            var verticalConnections = new List<TileData>();

            // Find type connections;
            for (var x = originX - 1; x >= 0; x--)
            {
                var other = tiles[x, originY];

                if (other.TypeId == origin.TypeId) horizontalConnections.Add(other);
                else break;
            }

            for (var x = originX + 1; x < width; x++)
            {
                var other = tiles[x, originY];

                if (other.TypeId == origin.TypeId) horizontalConnections.Add(other);
                else break;
            }

            for (var y = originY - 1; y >= 0; y--)
            {
                var other = tiles[originX, y];

                if (other.TypeId == origin.TypeId) verticalConnections.Add(other);
                else break;
            }

            for (var y = originY + 1; y < height; y++)
            {
                var other = tiles[originX, y];

                if (other.TypeId == origin.TypeId) verticalConnections.Add(other);
                else break;
            }

            return (horizontalConnections.ToArray(), verticalConnections.ToArray());
        }

        public static (TileData[], TileData[]) GetTextConnections(int originX, int originY, TileData[,] tiles)
        {
            var origin = tiles[originX, originY];

            var width = tiles.GetLength(0);
            var height = tiles.GetLength(1);

            var horizontalConnections = new List<TileData>();
            var verticalConnections = new List<TileData>();

            // Find text connections
            for (var x = originX - 1; x >= 0; x--)
            {
                var other = tiles[x, originY];

                if (other.TextId == origin.TextId) horizontalConnections.Add(other);
                else break;
            }

            for (var x = originX + 1; x < width; x++)
            {
                var other = tiles[x, originY];

                if (other.TextId == origin.TextId) horizontalConnections.Add(other);
                else break;
            }

            for (var y = originY - 1; y >= 0; y--)
            {
                var other = tiles[originX, y];

                if (other.TextId == origin.TextId) verticalConnections.Add(other);
                else break;
            }

            for (var y = originY + 1; y < height; y++)
            {
                var other = tiles[originX, y];

                if (other.TextId == origin.TextId) verticalConnections.Add(other);
                else break;
            }

            return (horizontalConnections.ToArray(), verticalConnections.ToArray());
        }
        
        /*public static Match FindBestMatch(TileData[,] tiles)
        {
            var bestMatch = default(Match);

            for (var y = 0; y < tiles.GetLength(1); y++)
            {
                for (var x = 0; x < tiles.GetLength(0); x++)
                {
                    var tile = tiles[x, y];
                }
            }
            return bestMatch;
        }*/

        /*public static List<Match> FindAllMatches(TileData[,] tiles)
        {
            var matches = new List<Match>(); 

            for (var y = 0; y < tiles.GetLength(1); y++)
            {
                bool isConnectionDetected = false;
                for (var x = 0; x < tiles.GetLength(0); x++)
                {
                    var tile = tiles[x, y];
                    if (!isConnectionDetected)
                    {

                    }

                }
            }
            return matches;
        }*/

        public static Match FindBestMatch(TileData[,] tiles)
        {
            var bestMatch = default(Match);

            for (var y = 0; y < tiles.GetLength(1); y++)
            {
                for (var x = 0; x < tiles.GetLength(0); x++)
                {
                    var tile = tiles[x, y];

                    var (h, v) = GetTileConnections(x, y, tiles);
                    var (j, b) = GetTextConnections(x, y, tiles);

                    var tileMatch = new Match(tile, h, v);
                    var textMatch = new Match(tile, j, b);

                    if (tileMatch.Score > -1 || textMatch.Score > -1)
                    {
                        if (bestMatch != null)
                        {
                            if ((tileMatch.Score > textMatch.Score ? tileMatch.Score : textMatch.Score) > bestMatch.Score)
                                bestMatch = tileMatch.Score > textMatch.Score ? tileMatch : textMatch;
                        }
                        else
                        {
                            bestMatch = tileMatch.Score > textMatch.Score ? tileMatch : textMatch;
                        }
                    }
                    /*if (tileMatch.Score > -1)
                    {
                        if (bestMatch != null)
                        {
                            if (tileMatch.Score > bestMatch.Score)
                                bestMatch = tileMatch;
                        }
                        else
                        {
                            bestMatch = tileMatch;
                        }
                    }*/
                }
            }

            return bestMatch;
        }

        public static List<Match> FindAllMatches(TileData[,] tiles)
        {
            var matches = new List<Match>();
            var height = tiles.GetLength(1);
            var width = tiles.GetLength(0);

            //Find horizontal tile matches;
            var c = new LoopCounter("Find horizontal tile matches");
            for (var y = 0; y < height && c.Count; y++)
            {
                for (var x = 0; x < width && c.Count; x++)
                {
                    var tile = tiles[x, y];

                    var (h, v) = GetTileConnections(x, y, tiles);

                    var tileMatch = new Match(tile, h, new TileData[0]);

                    if (tileMatch.Score > -1)
                    {
                        x = tileMatch.rightBound;

                        matches.Add(tileMatch);
                    }
                    var a = c.Count;
                }
            }
            // Find vertical tile matches
            c = new LoopCounter("Find vertical tile matches");
            for (var x = 0; x < width && c.Count; x++)
            {
                for (var y = 0; y < height && c.Count; y++)
                {
                    var tile = tiles[x, y];

                    var (h, v) = GetTileConnections(x, y, tiles);

                    var tileMatch = new Match(tile, new TileData[0], v);

                    if (tileMatch.Score > -1)
                    {
                        y = tileMatch.bottomBound;

                        matches.Add(tileMatch);
                    }
                }
            }

            //Find horizontal text matches;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var tile = tiles[x, y];

                    var (h, v) = GetTextConnections(x, y, tiles);

                    var textMatch = new Match(tile, h, new TileData[0]);

                    if (textMatch.Score > -1)
                    {
                        x = textMatch.rightBound;

                        matches.Add(textMatch);
                    }
                }
            }
            // Find vertical text matches
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var tile = tiles[x, y];

                    var (h, v) = GetTextConnections(x, y, tiles);

                    var textMatch = new Match(tile, new TileData[0], v);

                    if (textMatch.Score > -1)
                    {
                        y = textMatch.bottomBound;

                        matches.Add(textMatch);
                    }
                }
            }
            return matches;
        }

        private static (int, int) GetDirectionOffset(byte direction) => direction switch
        {
            0 => (-1, 0),
            1 => (0, -1),
            2 => (1, 0),
            3 => (0, 1),

            _ => (0, 0),
        };

        public static Move FindMove(TileData[,] tiles)
        {
            var tilesCopy = (TileData[,])tiles.Clone();

            var width = tilesCopy.GetLength(0);
            var height = tilesCopy.GetLength(1);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    for (byte d = 0; d <= 3; d++)
                    {
                        var (offsetX, offsetY) = GetDirectionOffset(d);

                        var x2 = x + offsetX;
                        var y2 = y + offsetY;

                        if (x2 < 0 || x2 > width - 1 || y2 < 0 || y2 > height - 1) continue;

                        Swap(x, y, x2, y2, tilesCopy);

                        if (FindBestMatch(tilesCopy) != null) return new Move(x, y, x2, y2);

                        Swap(x2, y2, x, y, tilesCopy);
                    }
                }
            }

            return null;
        }

        public static Move FindBestMove(TileData[,] tiles)
        {
            var tilesCopy = (TileData[,])tiles.Clone();

            var width = tilesCopy.GetLength(0);
            var height = tilesCopy.GetLength(1);

            var bestScore = int.MinValue;

            var bestMove = default(Move);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    for (byte d = 0; d <= 3; d++)
                    {
                        var (offsetX, offsetY) = GetDirectionOffset(d);

                        var x2 = x + offsetX;
                        var y2 = y + offsetY;

                        if (x2 < 0 || x2 > width - 1 || y2 < 0 || y2 > height - 1) continue;

                        Swap(x, y, x2, y2, tilesCopy);

                        var match = FindBestMatch(tilesCopy);

                        if (match != null && match.Score > bestScore)
                        {
                            bestMove = new Move(x, y, x2, y2);

                            bestScore = match.Score;
                        }

                        Swap(x, y, x2, y2, tilesCopy);
                    }
                }
            }

            return bestMove;
        }
    }
}
