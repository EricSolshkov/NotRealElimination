using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchEngine
{
    public enum TextLogicType
    {
        pure,
        obj,
        op,
        prop,
        prep,
        func
    }
    public class RuleCompiler : MonoBehaviour
    {
        public TileData[] isTiles;
        public Rule[] rules;

        public List<TileTypeAsset> pureTypes;
        public List<TileTypeAsset> objTypes;
        public List<TileTypeAsset> opTypes;
        public List<TileTypeAsset> propTypes;
        public List<TileTypeAsset> prepTypes;
        public List<TileTypeAsset> funcTypes;

        private Rule[] protoRules;

        /// <summary>
        /// 根据规则语句结构匹配潜在的proto rules
        /// </summary>
        /// <param name="matrix">待匹配矩阵</param>
        public void PreProcess(TileData[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            // find horizontal rules 
            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x) 
                {
                   // if tile is opType
                   //   try match op with obj and prop like obj-op-prop
                   //   try match op with obj and obj like obj-op-obj
                   //   try match op with func and prop like func-op-obj
                   // if tile is funcType
                   //   try match func with 

                }
            }
            // find vertical rules 
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y) ;
            }
        }

        public void Compile(TileData[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);

            
        }

        public TextLogicType LogicType(TileData tile)
        {
            foreach (var tileType in pureTypes) if (tileType.id == tile.TextId) return TextLogicType.pure;
            foreach (var tileType in objTypes) if (tileType.id == tile.TextId) return TextLogicType.obj;
            foreach (var tileType in opTypes) if (tileType.id == tile.TextId) return TextLogicType.op;
            foreach (var tileType in propTypes) if (tileType.id == tile.TextId) return TextLogicType.prop;
            foreach (var tileType in prepTypes) if (tileType.id == tile.TextId) return TextLogicType.prep;
            foreach (var tileType in funcTypes) if (tileType.id == tile.TextId) return TextLogicType.func;
            return TextLogicType.pure;
        }

        public bool isLogicType(TileData tile)
        {
            foreach (var tileType in pureTypes) if (tileType.id == tile.TextId) return false;
            foreach (var tileType in objTypes) if (tileType.id == tile.TextId) return true;
            foreach (var tileType in opTypes) if (tileType.id == tile.TextId) return true;
            foreach (var tileType in propTypes) if (tileType.id == tile.TextId) return true;
            foreach (var tileType in prepTypes) if (tileType.id == tile.TextId) return true;
            foreach (var tileType in funcTypes) if (tileType.id == tile.TextId) return true;
            return false;
        }


    }
}