using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchEngine
{
    public class Rule
    {
        public Rule() { ; }
    }

    public class OneWayReplace : Rule
    {
        public int obj1Text;
        public int obj2Text;
        public OneWayReplace(int _obj1, int _obj2)
        {
            obj1Text = _obj1;
            obj2Text = _obj2;
        }
    }

    public class TwoWayReplace : Rule
    {
        public int obj1Text;
        public int obj2Text;
        public TwoWayReplace(int _obj1, int _obj2)
        {
            obj1Text = _obj1;
            obj2Text = _obj2;
        }
    }

    public class Evaluation : Rule
    {
        public int objTextId;
        public int propTextId;
        public Evaluation(int _obj, int _prop)
        {
            objTextId = _obj;
            propTextId = _prop;
        }
    }

    public class CallFunction : Rule
    {
        public int funcTextId;
        public int propTextId;
        public CallFunction(int _func, int _prop)
        {
            funcTextId = _func;
            propTextId = _prop;
        }
    }
}