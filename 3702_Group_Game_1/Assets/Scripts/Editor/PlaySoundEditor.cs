using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(PlaySound))]
public class PlaySoundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws the default inspector

        PlaySound myScript = (PlaySound)target;
        if (GUILayout.Button("Play Sound"))
        {
            myScript.Play();
        }
    }
}
