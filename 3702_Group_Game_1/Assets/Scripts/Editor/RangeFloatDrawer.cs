using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangeFloatAttribute))]
public class RangeFloatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Cast the attribute to use its data
        RangeFloatAttribute range = attribute as RangeFloatAttribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.Slider(position, property, range.Min, range.Max, label);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use RangeFloat with float.");
        }
    }
}
