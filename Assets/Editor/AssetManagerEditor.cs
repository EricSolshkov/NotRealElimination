using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace MatchEngine
{
    [CustomEditor(typeof(AssetManager))]
    public class AssetManagerEditor : Editor
    {
        
        
        private void OnEnable()
        {
            
        }


        public override void OnInspectorGUI()
        {
            //更新
            //serializedObject.Update();
            //自动布局绘制列表
            //应用
            //serializedObject.ApplyModifiedProperties();
            
            DrawDefaultInspector();
            AssetManager AM = (AssetManager)target;
            if (GUILayout.Button("创建对象"))
            {
                AM.CheckSceneSetting();
            }
        }

    }
}