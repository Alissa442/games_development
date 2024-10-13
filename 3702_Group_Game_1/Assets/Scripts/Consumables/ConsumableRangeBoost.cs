using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableRangeBoost : Consumable
{
    [SerializeField] private float rangeBoost = 3f;
    [SerializeField] private float lengthOfTime = 15f;
    bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return; // Prevents double picking up of the consumable

        Range range = other.GetComponent<Range>();
        // Can't be eaten by something that doesn't have health
        if (range == null) return;

        range.IncreaseRange(rangeBoost, lengthOfTime);

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
        consumableName = "Range";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onRangeBoostSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onRangeBoostPickedUp.Invoke(gameObject);
    }
}
