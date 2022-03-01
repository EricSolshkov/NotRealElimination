using UnityEngine;

namespace MatchEngine
{
    [CreateAssetMenu(menuName = "Match 3 Engine/Text Type Asset")]
    public sealed class TextTypeAsset : ScriptableObject
    {
        public Text text;

        public LogicType logicType;

        public Sprite sprite;

    }
}
