using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableIngredient : Consumable
{
    [SerializeField] private int ingredientNumber = 1;
    bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return; // Prevents double picking up of the consumable

        IngregientsCarrier carrier = other.GetComponent<IngregientsCarrier>();
        // Can't be eaten by something that doesn't have health
        if (carrier == null) return;

        carrier.AddIngredientsCarried(ingredientNumber);

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
        consumableName = "Ingredient";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onIngredientSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onIngredientPickedUp.Invoke(gameObject);
    }
}
