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
		private TileTypeAsset _text;

		public TileTypeAsset Type
		{
			get => _tile;

			set
			{
				if (_tile == value) return;

				_tile = value;

				icon.sprite = _tile.sprite;
			}
		}
		public TileTypeAsset Text
		{
			get => _text;

			set
			{
				if (_text == value) return;

				_text = value;

				text.sprite = _text.sprite;
			}
		}

		public TileData Data => new TileData(x, y, _tile.id, _text.id, _text.type);
	}
}
