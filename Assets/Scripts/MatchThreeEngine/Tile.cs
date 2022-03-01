using UnityEngine;
using UnityEngine.UI;

namespace MatchEngine
{

    public sealed class Tile : MonoBehaviour
	{
		public int x;
		public int y;

		public Image icon;
		public Image text;

		public Sprite[] tileBackgrounds;

		public Button button;

		private TileTypeAsset _tile;
		private TextTypeAsset _text;

		public TileTypeAsset TileAsset
		{
			get => _tile;

			set
			{
				if (_tile == value) return;

				_tile = value;

				icon.sprite = _tile.sprite;
			}
		}
		public TextTypeAsset TextAsset
		{
			get => _text;

			set
			{
				if (_text == value) return;

				_text = value;

				text.sprite = _text.sprite;
			}
		}

		public TileData Data => new TileData(x, y, _tile.name, _text.text, _text.logicType);
	}
}
