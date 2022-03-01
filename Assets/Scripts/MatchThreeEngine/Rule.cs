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
        public Text obj1Text;
        public Text obj2Text;
        public OneWayReplace(Text _obj1, Text _obj2)
        {
            obj1Text = _obj1;
            obj2Text = _obj2;
        }
    }

    public class TwoWayReplace : Rule
    {
        public Text obj1Text;
        public Text obj2Text;
        public TwoWayReplace(Text _obj1, Text _obj2)
        {
            obj1Text = _obj1;
            obj2Text = _obj2;
        }
    }

    public class Evaluation : Rule
    {
        public Text objTextId;
        public Text propTextId;
        public Evaluation(Text _obj, Text _prop)
        {
            objTextId = _obj;
            propTextId = _prop;
        }
    }

    public class CallFunction : Rule
    {
        public Text funcTextId;
        public Text propTextId;
        public CallFunction(Text _func, Text _prop)
        {
            funcTextId = _func;
            propTextId = _prop;
        }
    }
}