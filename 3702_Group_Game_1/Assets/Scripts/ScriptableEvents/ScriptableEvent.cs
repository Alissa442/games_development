using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ScriptableEvent<T> : ScriptableObject
{
    public UnityEvent<T> onChanged;

    /// <summary>
    /// Raise the event with the given object
    /// </summary>
    /// <param name="obj"></param>
    public void Raise(T obj)
    {
        onChanged.Invoke(obj);
    }

    /// <summary>
    /// Register the your listener to this ScriptableEvent
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterListener(UnityAction<T> listener)
    {
        onChanged.AddListener(listener);
    }

    public void UnregisterListener(UnityAction<T> listener)
    {
        onChanged.RemoveListener(listener);
    }

    private void OnDisable()
    {
        onChanged.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        onChanged.RemoveAllListeners();
    }

}
