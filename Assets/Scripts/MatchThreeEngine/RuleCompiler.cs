using System.Collections.Generic;
using UnityEngine;

namespace MatchEngine
{
    


    public class RuleCompiler : MonoBehaviour
    {
        private static TileData[,] matrix;
        private static List<List<Expression>> protoExprs;
        private static List<Expression> ast;
        /*/// <summary>
        /// 根据规则语句结构匹配潜在的proto rules
        /// </summary>
        /// <param name="matrix">待匹配矩阵</param>
        /*public static void PreProcess(TileData[,] matrix, List<Rule> rules)
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
        }*/
        /// <summary>
        /// 将所有长度超过3的连续text方块构造为Expression数组
        /// </summary>
        public static void PreProcess()
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            protoExprs = new List<List<Expression>>();
            // find horizontal protoExpr
            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var protoExpr = new List<Expression>();
                    while (x < width && matrix[x, y].logicType != LogicType.pure)
                    {
                        protoExpr.Add(new Expression(matrix[x, y]));
                        ++x;
                    }
                    if (protoExpr.Count >= 3) protoExprs.Add(protoExpr);
                }
            }
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
                {
                    var protoExpr = new List<Expression>();
                    while (y < height && matrix[x, y].logicType != LogicType.pure)
                    {
                        protoExpr.Add(new Expression(matrix[x, y]));
                        ++y;
                    }
                    if (protoExpr.Count >= 3) protoExprs.Add(protoExpr);
                }
            }

        }
        /// <summary>
        /// 根据Expression数组尝试生成满足词性结构的AST
        /// </summary>
        private static void GenerateAST()
        {
            foreach(var proto in protoExprs)
            {
                
                
            }
            // 当无法构成AST时
            //if (true) Debug.Log("Syntax Error - Invalid syntax: " +proto[index].text+"("+ proto[index].logicType+") followed by invalid logic type." );

        }
        private static void Compile()
        {

        }
        public static void Compile(TileData[,] _matrix, List<Expression> rules)
        {
            matrix = _matrix;
            PreProcess();
            GenerateAST();
            Compile();
        }







    }
}