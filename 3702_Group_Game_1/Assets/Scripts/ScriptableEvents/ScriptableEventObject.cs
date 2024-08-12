using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewScriptableEventObject", menuName = "ScriptableObjects/Scriptable Event - GameObject")]
public class ScriptableEventObject : ScriptableEvent<GameObject>
{
    //[Tooltip("Used only when pressing Raise Event button")]
    //[SerializeField] public GameObject _gameObject;
}
