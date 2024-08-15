using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class MethodButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MonoBehaviour monoBehaviour = (MonoBehaviour)target;
        Type type = monoBehaviour.GetType();

        // Iterate through all methods in the MonoBehaviour
        foreach (var method in type.GetMethods())
        {
            // Check if the method has the MethodButton attribute
            var attributes = method.GetCustomAttributes(typeof(MethodButtonAttribute), true);
            if (attributes.Length > 0)
            {
                var attribute = (MethodButtonAttribute)attributes[0];
                string buttonLabel = string.IsNullOrEmpty(attribute.ButtonLabel) ? method.Name : attribute.ButtonLabel;

                if (GUILayout.Button(buttonLabel))
                {
                    method.Invoke(monoBehaviour, null);
                }
            }
        }
    }
}
