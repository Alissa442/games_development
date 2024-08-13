using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableFood : MonoBehaviour
{
    public GameGlobalEventsSO gameGlobalEvents;
    [SerializeField] private float healthValue = 20f;
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        // Can't be eaten by something that doesn't have health
        if (health == null) return;

        health.Heal(healthValue);        
    }

    private void Awake()
    {
        if (gameGlobalEvents == null)
            gameGlobalEvents = Resources.Load<GameGlobalEventsSO>("GameGlobalEvents");

        // Trigger the event that this consumable has spawned
        gameGlobalEvents.onConsumableSpawned.Invoke(gameObject);
        // Trigger the event that this food has spawned
        gameGlobalEvents.onFoodSpawned.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        // Trigger the event that this consumable has been picked up
        gameGlobalEvents.onConsumablePickedUp.Invoke(gameObject);
    }
}
