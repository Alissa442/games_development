using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableFreeze : Consumable
{
    [SerializeField] private float freezeTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player or enemy
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy")) return;

        // Get all Speed objects from all the enemies and players
        Speed[] speeds = FindObjectsOfType<Speed>();

        // For each object in speed that isn't the other.gameObject
        foreach (Speed speed in speeds)
        {
            if (speed.gameObject != other.gameObject)
            {
                // Freeze the object
                speed.Freeze(freezeTime);
            }
        }

        // Trigger the event that this consumable has been picked up
        gameGlobalEvents.onConsumablePickedUp.Invoke(gameObject);

        // Trigger the event that this food has been picked up by whom
        gameGlobalEvents.onConsumablePickedUpByWhom.Invoke(gameObject, other.gameObject);
    }

    protected override void Start()
    {
        base.Start();

        // Set the consumable name
        consumableName = "Freeze";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onFreezeSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onFreezePickedUp.Invoke(gameObject);
    }
}
