using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableEventObject))]

public class ScriptableEventEditor : Editor
{
    private int selectedGameObjectIndex = 0;
    private List<GameObject> sceneGameObjects = new List<GameObject>();
    private string[] gameObjectNames;

    private void OnEnable()
    {
        // Populate the list of GameObjects in the scene
        sceneGameObjects.Clear();
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            sceneGameObjects.Add(go);
        }

        // Create an array of GameObject names for the dropdown
        gameObjectNames = new string[sceneGameObjects.Count];
        for (int i = 0; i < sceneGameObjects.Count; i++)
        {
            gameObjectNames[i] = sceneGameObjects[i].name;
        }
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Draw the default inspector
        DrawDefaultInspector();

        // Draw the GameObject dropdown list
        selectedGameObjectIndex = EditorGUILayout.Popup("Select GameObject", selectedGameObjectIndex, gameObjectNames);

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();

        ScriptableEventObject myScript = (ScriptableEventObject)target;
        if (GUILayout.Button("Raise Event"))
        {
            if (sceneGameObjects.Count > 0)
            {
                myScript.Raise(sceneGameObjects[selectedGameObjectIndex]);
            }
        }
    }
}
