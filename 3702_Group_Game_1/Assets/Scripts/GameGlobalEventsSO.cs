using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameGlobalEvents", menuName = "ScriptableObjects/Game Global Events List")]
public class GameGlobalEventsSO : ScriptableObject
{
    public UnityEvent onPlayerDeath = new UnityEvent();

    public UnityEvent<GameObject> onEnemySpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onEnemyDied = new UnityEvent<GameObject>();

    public UnityEvent<GameObject> onConsumableSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onConsumablePickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onFoodSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onPoisonSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onRangeBoostSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onSpeedBoostSpawned = new UnityEvent<GameObject>();

}
