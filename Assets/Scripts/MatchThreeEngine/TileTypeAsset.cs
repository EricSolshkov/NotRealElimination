using UnityEngine;
using UnityEditor;

namespace MatchEngine
{
    [CreateAssetMenu(menuName = "Match 3 Engine/Tile Type Asset")]
    public sealed class TileTypeAsset : ScriptableObject
    {
        new public string name;

        public Sprite sprite;
    }
}