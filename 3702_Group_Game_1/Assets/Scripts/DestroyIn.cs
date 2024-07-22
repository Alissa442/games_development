using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyIn : MonoBehaviour
{
    private GameObject _obj;

    public void DestroyWithDelay(float delayTime) {
        _obj = gameObject;
        Invoke("DestroyObject", delayTime);
    }

    // Call this method to destroy an object immediately in the editor
    public void DestroyObject()
    {
#if UNITY_EDITOR
        // Use DestroyImmediate when in the Unity Editor and not in play mode
        if (!Application.isPlaying)
        {
            //Undo.RecordObject(obj, "Destroy object");
            DestroyImmediate(_obj);
        }
        else
        {
            // Use Destroy as usual during play mode
            Destroy(_obj);
        }
#else
        // Fallback to Destroy if somehow this code is executed outside the Unity Editor
        Destroy(_obj);
#endif
    }

}
