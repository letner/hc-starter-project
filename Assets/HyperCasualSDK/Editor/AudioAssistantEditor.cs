using System.Collections.Generic;
using UnityEditor;

namespace HyperCasualSDK.Editor
{
    [CustomEditor(typeof(AudioAssistant))]
    public class AudioAssistantEditor : UnityEditor.Editor
    {
        private SerializedProperty _soundEffectsProperty;

        private void OnEnable()
        {
            _soundEffectsProperty = serializedObject.FindProperty("soundEffects");
        }

        public override void OnInspectorGUI()
        {
            if (!CheckSoundEffectsUniqueness())
            {
                EditorGUILayout.HelpBox("List of sound effects must contain unique SoundEffectTypes!", MessageType.Error);
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(_soundEffectsProperty);
            serializedObject.ApplyModifiedProperties();
        }
        
        private bool CheckSoundEffectsUniqueness()
        {
            var soundEffects = ((AudioAssistant) serializedObject.targetObject).soundEffects;
            var soundEffectSet = new HashSet<SoundEffectType>();
            foreach (var effect in soundEffects)
            {
                soundEffectSet.Add(effect.type);
            }
            return soundEffects.Length == soundEffectSet.Count;
        }
    }
}
