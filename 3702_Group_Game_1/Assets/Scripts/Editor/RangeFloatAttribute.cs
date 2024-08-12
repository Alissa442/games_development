using UnityEngine;

public class RangeFloatAttribute : PropertyAttribute
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public RangeFloatAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}
