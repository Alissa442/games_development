using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePrefabs", menuName = "ScriptableObjects/Game Prefab List")]

public class GamePrefabsSO : ScriptableObject
{
    public GameObject worldSpaceCanvas;
    public GameObject healthUIPrefab;

    public GameObject player;

    public GameObject food;
    public GameObject poison;
    public GameObject rangeBoost;
    public GameObject speedBoost;

}
