using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableFood : Consumable
{
    [SerializeField] private float healthValue = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit other: " + other.name);
        Health health = other.GetComponent<Health>();
        // Can't be eaten by something that doesn't have health
        if (health == null)
        {
            Debug.Log("Doesn't have health component: " + other.name);
            return;
        }

        health.Heal(healthValue);

        // Trigger the event that this consumable has been picked up
        gameGlobalEvents.onConsumablePickedUp.Invoke(gameObject);

        // Trigger the event that this food has been picked up by whom
        gameGlobalEvents.onConsumablePickedUpByWhom.Invoke(gameObject, other.gameObject);
    }

    protected override void Start()
    {
        base.Start();

        // Set the consumable name
        consumableName = "Food";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onFoodSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onFoodPickedUp.Invoke(gameObject);
    }
}
