﻿using UnityEngine;

namespace MatchEngine
{
	[CreateAssetMenu(menuName = "Match 3 Engine/Tile Type Asset")]
	public sealed class TileTypeAsset : ScriptableObject
	{
		public int id;

		public LogicType type;

		public Sprite sprite;

	}
}
