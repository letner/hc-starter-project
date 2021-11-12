using UnityEditor;
using UnityEngine;

namespace Runner
{
    [CustomEditor(typeof(Locomotion))]
    public class AxisBoundsEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (!Application.isPlaying)
            {
                var locomotion = (Locomotion) target;

                EditorGUI.BeginChangeCheck();
                var position = locomotion.transform.position;
                var minPosition = new Vector3(position.x + locomotion.xBounds.min, position.y, position.z);
                var min = Handles.PositionHandle(minPosition, Quaternion.identity);
                var maxPosition = new Vector3(position.x + locomotion.xBounds.max, position.y, position.z);
                var max = Handles.PositionHandle(maxPosition, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Locomotion's xBounds");
                    locomotion.xBounds.min = min.x - position.x;
                    locomotion.xBounds.max = max.x - position.x;
                }
            }
        }
    }
}
