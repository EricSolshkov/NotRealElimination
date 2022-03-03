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
        crush,
        and,
        not
    }
    public enum LogicType
    {
        pure,
        obj,
        op,
        prop,
        prep,
        func,
        conj,
        cond,
        rule
    }

    public class TileData
    {
        public readonly int X;
        public readonly int Y;

        public readonly string tileName;
        public readonly string text;
        public readonly LogicType logicType;

        public TileData(int x, int y, string _tile, string _text, LogicType _type)
        {
            X = x;
            Y = y;

            tileName = _tile;
            text = _text;
            logicType = _type;
        }
    }
}
