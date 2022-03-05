using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MatchEngine
{
    [CustomPropertyDrawer(typeof(TileTypeAsset))]
    public class TileTypeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {


                //设置属性名宽度
                EditorGUIUtility.labelWidth = 60;
                position.height = EditorGUIUtility.singleLineHeight;
                
                position.height = 80;

                EditorGUI.Foldout(position, false, ".?") ;
                //
                Rect iconRect = new Rect(position)
                {
                    width = 64,
                    height = 64
                };

                var nameRect = new Rect(position)
                {
                    width = position.width - 80,
                    x = position.x + 80
                };



                //
                var _property = new SerializedObject(property.objectReferenceValue);
                SerializedProperty iconProperty = _property.FindProperty("sprite");
                SerializedProperty nameProperty = _property.FindProperty("name");


                //EditorGUI.TextField(nameRect, property.objectReferenceValue.name);
                EditorGUI.TextField(nameRect, "name", nameProperty.stringValue);
                EditorGUI.ObjectField(iconRect, iconProperty, typeof(Texture));
                //EditorGUI.ObjectField(iconRect, property.objectReferenceValue.sprite, typeof(Texture), false);
                
                

            }
        }
    }
}