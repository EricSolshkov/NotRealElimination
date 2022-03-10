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
            //����
            //serializedObject.Update();
            //�Զ����ֻ����б�
            //Ӧ��
            //serializedObject.ApplyModifiedProperties();
            
            DrawDefaultInspector();
            AssetManager AM = (AssetManager)target;
            if (GUILayout.Button("��������"))
            {
                AM.CheckSceneSetting();
            }
        }

    }
}