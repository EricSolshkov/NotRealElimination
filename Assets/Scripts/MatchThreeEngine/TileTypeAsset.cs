using UnityEngine;

namespace MatchEngine
{
    [CreateAssetMenu(menuName = "Match 3 Engine/Tile Type Asset")]
    public sealed class TileTypeAsset : ScriptableObject
    {
        new public TileName name;
        public Sprite sprite;
    }
}