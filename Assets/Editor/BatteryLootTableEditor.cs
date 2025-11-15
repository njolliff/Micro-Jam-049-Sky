using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LootTable))]
public class BatteryLootTableEditor : Editor
{
    private SerializedProperty entriesProp;

    private void OnEnable()
    {
        entriesProp = serializedObject.FindProperty("entries");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(entriesProp, new GUIContent("Entries"), true);

        serializedObject.ApplyModifiedProperties();
    }
}
