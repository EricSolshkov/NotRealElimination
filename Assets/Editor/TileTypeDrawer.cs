using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MatchEngine
{
    [CustomPropertyDrawer(typeof(TileTypeAsset))]
    public class TileTypeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 120;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var nameRect = new Rect(position)
                {
                    height = EditorGUIUtility.singleLineHeight
                };
                Rect iconRect = new Rect(position)
                {
                    //x = position.x + position.width-64,
                    y = position.y + EditorGUIUtility.singleLineHeight+15,
                    width = 64,
                    height = 64
                };

                



                //
                var _property = new SerializedObject(property.objectReferenceValue);
                SerializedProperty iconProperty = _property.FindProperty("sprite");
                SerializedProperty nameProperty = _property.FindProperty("name");

                //EditorGUI.TextField(nameRect, property.objectReferenceValue.name);
                EditorGUI.TextField(nameRect, "name", nameProperty.stringValue);
                var spriteBox = EditorGUI.ObjectField(iconRect,iconProperty.objectReferenceValue, typeof(Sprite), false);
                //EditorGUI.ObjectField(iconRect,iconProperty.objectReferenceValue, typeof(Sprite), false);
                //EditorGUI.ObjectField(iconRect, property.objectReferenceValue.sprite, typeof(Texture), false);

                
                

            }
        }
    }
}