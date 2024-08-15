using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class MethodButtonAttribute : PropertyAttribute
{
    public string ButtonLabel { get; private set; }

    public MethodButtonAttribute(string buttonLabel = null)
    {
        ButtonLabel = buttonLabel;
    }
}
