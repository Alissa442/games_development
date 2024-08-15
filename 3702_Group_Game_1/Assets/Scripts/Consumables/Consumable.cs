using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    public string consumableName;
    public GameGlobalEventsSO gameGlobalEvents;

    protected virtual void Awake()
    {
        if (gameGlobalEvents == null)
            gameGlobalEvents = Resources.Load<GameGlobalEventsSO>("GameGlobalEvents");
    }

    protected virtual void Start()
    {
        // Invoke the event that listens for new consumables
        gameGlobalEvents.onConsumableSpawned.Invoke(gameObject);
    }

    protected virtual void OnDestroy()
    {
        // Invoke the event that listens for consumables that have been destroyed
        gameGlobalEvents.onConsumablePickedUp.Invoke(gameObject);
    }
}
