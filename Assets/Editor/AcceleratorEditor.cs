using UnityEditor;

namespace Gameplay
{
    [CustomEditor(typeof(Accelerator))]
    public class AcceleratorEditor : Editor
    {
        private SerializedProperty direction;
        private SerializedProperty position;
        private SerializedProperty force;
        private SerializedProperty offset;
        private SerializedProperty accelerationType;

        private void OnEnable()
        {
            direction = serializedObject.FindProperty("direction");
            position = serializedObject.FindProperty("position");
            force = serializedObject.FindProperty("force");
            offset = serializedObject.FindProperty("triggerCenter");
            accelerationType = serializedObject.FindProperty("accelerationType");
        }
    
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(direction);
            EditorGUILayout.PropertyField(force);
            EditorGUILayout.PropertyField(accelerationType);
        
            direction.vector2Value = direction.vector2Value.normalized;
        
            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            Handles.DrawLine(position.vector2Value + offset.vector2Value, position.vector2Value + offset.vector2Value + direction.vector2Value);
        }
    }
}