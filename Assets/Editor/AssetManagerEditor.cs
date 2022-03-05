using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace MatchEngine
{
    [CustomEditor(typeof(AssetManager))]
    public class AssetManagerEditor : Editor
    {
        private ReorderableList _tileTypeList;

        private void OnEnable()
        {
            //����ReorderableList ������
            //arg1:���л����壬arg2:���л����ݣ�arg3:�ɷ��϶���arg4:�Ƿ���ʾ���⣬arg5:�Ƿ���ʾ��Ӱ�ť��arg6:�Ƿ���ʾ��Ӱ�ť
            _tileTypeList = new ReorderableList(serializedObject, serializedObject.FindProperty("tileTypeContainer")
                , true, true, true, true);

            //�Զ����б�����
            _tileTypeList.drawHeaderCallback = (Rect rect) =>
            {
                GUI.Label(rect, "Tile Types");
            };

            //����Ԫ�صĸ߶�
            _tileTypeList.elementHeight = 80;

            //�Զ�������б�Ԫ��
            /*_tileTypeList.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                //����index��ȡ��ӦԪ�� 
                SerializedProperty item = _tileTypeList.serializedProperty.GetArrayElementAtIndex(index);
                var assetItem = new SerializedObject(item.objectReferenceValue);
                rect.height -= 4;
                rect.y += 2;

                /*EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    assetItem.FindProperty("name"),
                    new GUIContent("Element " + index));
                EditorGUI.TextField(
                    new Rect(rect.x+64, rect.y, 128, EditorGUIUtility.singleLineHeight),
                    "name",
                    assetItem.FindProperty("name").stringValue
                    );
                EditorGUI.ObjectField(
                    new Rect(rect.x + 256, rect.y, 64, 64),
                    assetItem.FindProperty("sprite"),
                    typeof(Texture), GUIContent.none) ;
            };*/

            //��ɾ��Ԫ��ʱ��Ļص�������ʵ��ɾ��Ԫ��ʱ������ʾ������
            _tileTypeList.onRemoveCallback = (ReorderableList list) =>
            {
                if (EditorUtility.DisplayDialog("Warnning", "Do you want to remove this element?", "Remove", "Cancel"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(list);
                }
            };
        }


        public override void OnInspectorGUI()
        {
            //����
            serializedObject.Update();
            //�Զ����ֻ����б�
            _tileTypeList.DoLayoutList();
            //Ӧ��
            serializedObject.ApplyModifiedProperties();
            
            DrawDefaultInspector();
            AssetManager AM = (AssetManager)target;
            if (GUILayout.Button("��������"))
            {
                AM.CheckSceneSetting();
            }
        }

    }
}