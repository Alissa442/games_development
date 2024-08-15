using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom property drawer for the ReadOnly attribute
/// So we limit the designer from being able to alter the attribute.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Disable the GUI to make the field read-only
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true; // Re-enable the GUI
    }
}
