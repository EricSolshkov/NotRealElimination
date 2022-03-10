using UnityEngine;
using UnityEditor;

namespace MatchEngine
{
    [CreateAssetMenu(menuName = "Match 3 Engine/Tile Type Asset")]
    public sealed class TileTypeAsset : ScriptableObject
    {
        [SerializeField] new public string name;

        [SerializeField] public Sprite sprite;
    }
}