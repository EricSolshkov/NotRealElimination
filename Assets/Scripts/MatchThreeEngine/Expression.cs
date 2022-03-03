namespace MatchEngine
{
    public class Expression
    {
        bool isLeaf;
        LogicType logicType;
        TileData root;
        Expression lParam;
        Expression rParam;
        public Expression(TileData _root, Expression _lParam, Expression _rParam)
        {
            root = _root;
            lParam = _lParam;
            rParam = _rParam;
            isLeaf = false;
            switch (_root.text)
            {
                case "is":
                    if (_lParam.logicType == LogicType.obj &&
                        _rParam.logicType == LogicType.obj)
                    {
                        logicType = LogicType.rule;
                    }
                    else if (_lParam.logicType == LogicType.obj &&
                             _rParam.logicType == LogicType.prop)
                    {
                        logicType = LogicType.rule;
                    }
                    else if (_lParam.logicType == LogicType.func &&
                             _rParam.logicType == LogicType.prop)
                    {
                        logicType = LogicType.rule;
                    }
                    break;
                case "and":
                    if (_lParam.logicType == LogicType.obj &&
                        _rParam.logicType == LogicType.obj)
                    {
                        logicType = LogicType.obj;
                    }
                    else if (_lParam.logicType == LogicType.prop &&
                             _rParam.logicType == LogicType.prop)
                    {
                        logicType = LogicType.prop;
                    }
                    else if (_lParam.logicType == LogicType.cond &&
                             _rParam.logicType == LogicType.cond)
                    {
                        logicType = LogicType.cond;
                    }
                    else if (_lParam.logicType == LogicType.func &&
                             _rParam.logicType == LogicType.func)
                    {
                        logicType = LogicType.func;
                    }
                    break;
            }
        }
        public Expression(TileData _root, Expression _rParam)
        {
            root = _root;
            lParam = null;
            rParam = _rParam;
            isLeaf = false;
            if (_root.text == "not")
            {
                if (_rParam.logicType == LogicType.obj)
                {
                    logicType = LogicType.obj;
                }
                else if (_rParam.logicType == LogicType.prop)
                {
                    logicType = LogicType.prop;
                }
            }
            else if (_root.logicType == LogicType.obj)
            {
                if (_rParam.logicType == LogicType.cond)
                {
                    logicType = LogicType.obj;
                }
            }

        }

        public Expression(TileData _root)
        {
            root = _root;
            logicType = _root.logicType;
            lParam = null;
            rParam = null;
            isLeaf = true;
        }
    }
}