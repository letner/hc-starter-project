using System;
using UnityEditor;
using UnityEngine;

namespace HyperCasualSDK.Editor
{
    [CustomPropertyDrawer(typeof(SoundEffect))]
    public class SoundEffectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent content)
        {
            EditorGUI.BeginProperty(position, content, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var audioClipWidth = Math.Max(90, position.width * .6f);
            var typeWidth = Math.Min(100, position.width - audioClipWidth);
            var soundEffectTypeRect = new Rect(position.x + 15, position.y, typeWidth, position.height);
            var audioClipRect = new Rect(position.width - audioClipWidth + 18, position.y, audioClipWidth, position.height);

            EditorGUI.PropertyField(soundEffectTypeRect, property.FindPropertyRelative("type"), GUIContent.none);
            EditorGUI.PropertyField(audioClipRect, property.FindPropertyRelative("audioClip"), GUIContent.none);
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
