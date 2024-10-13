using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpeedBoost : Consumable
{
    [SerializeField] private float speedBoost = 3f;
    [SerializeField] private float lengthOfTime = 15f;
    bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return; // Prevents double picking up of the consumable

        Speed speed = other.GetComponent<Speed>();
        // Can't be eaten by something that doesn't have health
        if (speed == null) return;

        speed.IncreaseSpeed(speedBoost, lengthOfTime);

        // Trigger the event that this consumable has been picked up
        gameGlobalEvents.onConsumablePickedUp.Invoke(gameObject);

        // Trigger the event that this food has been picked up by whom
        gameGlobalEvents.onConsumablePickedUpByWhom.Invoke(gameObject, other.gameObject);

        isPickedUp = true;
    }

    protected override void Start()
    {
        base.Start();

        // Set the consumable name
        consumableName = "Speed";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onSpeedBoostSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onSpeedBoostPickedUp.Invoke(gameObject);
    }
}
