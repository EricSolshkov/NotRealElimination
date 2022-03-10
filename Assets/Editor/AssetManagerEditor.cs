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
            //创建ReorderableList 参数：
            //arg1:序列化物体，arg2:序列化数据，arg3:可否拖动，arg4:是否显示标题，arg5:是否显示添加按钮，arg6:是否显示添加按钮
            _tileTypeList = new ReorderableList(serializedObject, serializedObject.FindProperty("tileTypeContainer")
                , true, true, true, true);

            //自定义列表名称
            _tileTypeList.drawHeaderCallback = (Rect rect) =>
            {
                GUI.Label(rect, "Tile Types");
            };

            //定义元素的高度
            _tileTypeList.elementHeight = 80;

            //自定义绘制列表元素
            /*_tileTypeList.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                //根据index获取对应元素 
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

            //当删除元素时候的回调函数，实现删除元素时，有提示框跳出
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
            //更新
            serializedObject.Update();
            //自动布局绘制列表
            _tileTypeList.DoLayoutList();
            //应用
            serializedObject.ApplyModifiedProperties();
            
            DrawDefaultInspector();
            AssetManager AM = (AssetManager)target;
            if (GUILayout.Button("创建对象"))
            {
                AM.CheckSceneSetting();
            }
        }

    }
}