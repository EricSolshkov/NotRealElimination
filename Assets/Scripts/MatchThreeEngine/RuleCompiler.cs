using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MatchEngine
{
    class Expression
    {
        bool isLeaf;
        TileData root;
        Expression lParam;
        Expression rParam;
        public Expression(TileData _root, Expression _lParam, Expression _rParam)
        {
            root = _root;
            lParam = _lParam;
            rParam = _rParam;
            isLeaf = false;
        }
        public Expression(TileData _root, Expression _rParam)
        {
            root = _root;
            rParam = _rParam;
            isLeaf = false;
        }

        public Expression(TileData _root) 
        {
            root = _root;
            isLeaf = true;
        }
    }
    

    public class RuleCompiler : MonoBehaviour
    {
        private TileData[,] matrix;
        private List<TileData> protoExpr;
        private List<Expression> ast;
        /// <summary>
        /// 根据规则语句结构匹配潜在的proto rules
        /// </summary>
        /// <param name="matrix">待匹配矩阵</param>
        public static void PreProcess(TileData[,] matrix, List<Rule> rules)
        {
            rules.Clear();
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            // find horizontal rules 
            for (var y = 0; y < height; ++y)
            {
                for (var x = 1; x < width-1; ++x) 
                {
                    // if tile is opType
                    if (matrix[x, y].logicType == LogicType.op)
                    {
                        // try match op with obj and prop like obj-op-prop
                        if (matrix[x - 1, y].logicType == LogicType.obj &&
                            matrix[x + 1, y].logicType == LogicType.prop)
                        {
                            rules.Add(new Evaluation(matrix[x - 1, y].text, matrix[x + 1, y].text));
                        }
                        // try match op with obj and obj like obj-op-obj
                        if (matrix[x - 1, y].logicType == LogicType.obj &&
                            matrix[x + 1, y].logicType == LogicType.obj)
                        {
                            rules.Add(new OneWayReplace(matrix[x - 1, y].text, matrix[x + 1, y].text));
                        }
                        // try match op with func and prop like func-op-obj
                        if (matrix[x - 1, y].logicType == LogicType.func &&
                            matrix[x + 1, y].logicType == LogicType.prop)
                        {
                            rules.Add(new CallFunction(matrix[x - 1, y].text, matrix[x + 1, y].text));
                        }
                    }
                }
            }
            // find vertical rules 
            for (var x = 0; x < width; ++x)
            {
                for (var y = 1; y < height - 1; ++y)
                {
                    // if tile is opType
                    if (matrix[x, y].logicType == LogicType.op)
                    {
                        // match obj-op-prop as evaluation
                        if (matrix[x, y - 1].logicType == LogicType.obj &&
                            matrix[x, y + 1].logicType == LogicType.prop)
                        {
                            rules.Add(new Evaluation(matrix[x, y - 1].text, matrix[x, y + 1].text));
                        }
                        // match obj-op-obj as one way replace
                        if (matrix[x, y - 1].logicType == LogicType.obj &&
                            matrix[x, y + 1].logicType == LogicType.obj)
                        {
                            rules.Add(new OneWayReplace(matrix[x, y - 1].text, matrix[x, y + 1].text));
                        }
                        // match func-op-obj as call function
                        if (matrix[x, y - 1].logicType == LogicType.func &&
                            matrix[x, y + 1].logicType == LogicType.prop)
                        {
                            rules.Add(new CallFunction(matrix[x, y - 1].text, matrix[x, y + 1].text));
                        }
                    }
                }
            }
        }

        public void Compile(List<Rule> rules)
        {

        }

        

        


    }
}