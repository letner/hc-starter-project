using HyperCasualSDK.HelperComponents;
using UnityEditor;
using UnityEngine;

namespace HyperCasualSDK.Editor
{
    [CustomPropertyDrawer(typeof(AxisBounds))]
    public class AxisBoundsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent content)
        {
            EditorGUI.BeginProperty(position, content, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var labelRect = new Rect(position.x, position.y, 150, position.height);
            var minFieldRect = new Rect(position.x + 155, position.y, 70, position.height);
            var macFieldRect = new Rect(position.x + 240, position.y, 70, position.height);

            EditorGUI.LabelField(labelRect, "Axis Bounds");
            EditorGUI.PropertyField(minFieldRect, property.FindPropertyRelative("min"), GUIContent.none);
            EditorGUI.PropertyField(macFieldRect, property.FindPropertyRelative("max"), GUIContent.none);
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();

        }
    }
}
