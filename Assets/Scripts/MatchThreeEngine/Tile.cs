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

		private TileTypeAsset _type;
		private TileTypeAsset _text;

		public TileTypeAsset Type
		{
			get => _type;

			set
			{
				if (_type == value) return;

				_type = value;

				icon.sprite = _type.sprite;
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

		public TileData Data => new TileData(x, y, _type.id, _text.id);
	}
}
