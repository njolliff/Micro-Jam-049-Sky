using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CloudLootTable))]
public class CloudLootTableEditor : Editor
{
    private SerializedProperty entriesProp, normalCloudPrefabProp, fastCloudPrefabProp, slowCloudPrefabProp, bouncyCloudTableProp;

    private void OnEnable()
    {
        entriesProp = serializedObject.FindProperty("entries");
        bouncyCloudTableProp = serializedObject.FindProperty("bouncyCloudTable");
        normalCloudPrefabProp = serializedObject.FindProperty("normalCloudPrefab");
        fastCloudPrefabProp = serializedObject.FindProperty("fastCloudPrefab");
        slowCloudPrefabProp = serializedObject.FindProperty("slowCloudPrefab");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(entriesProp, new GUIContent("Entries"), true);
        EditorGUILayout.PropertyField(bouncyCloudTableProp, new GUIContent("Bouncy Cloud Table"), false);
        EditorGUILayout.PropertyField(normalCloudPrefabProp, new GUIContent("Normal Cloud Prefab"), false);
        EditorGUILayout.PropertyField(fastCloudPrefabProp, new GUIContent("Fast Cloud Prefab"), false);
        EditorGUILayout.PropertyField(slowCloudPrefabProp, new GUIContent("Slow Cloud Prefab"), false);

        serializedObject.ApplyModifiedProperties();
    }
}
