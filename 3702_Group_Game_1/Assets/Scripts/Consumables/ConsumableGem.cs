using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableGem : Consumable
{
    [SerializeField] private int gemValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player
        if (!other.CompareTag("Player")) return;

        // Increase the players gem/currency count

        // Get the players gem count from the PlayerPrefs
        int currentGemCount = PlayerPrefs.GetInt("PlayerCurrency", 0);
        currentGemCount += gemValue;

        // Save the new gem count to the PlayerPrefs
        PlayerPrefs.SetInt("PlayerCurrency", currentGemCount);
        PlayerPrefs.Save(); // Ensure the data is written to disk

        // Trigger the event that this consumable has been picked up
        gameGlobalEvents.onConsumablePickedUp.Invoke(gameObject);

        // Trigger the event that this food has been picked up by whom
        gameGlobalEvents.onConsumablePickedUpByWhom.Invoke(gameObject, other.gameObject);
    }

    protected override void Start()
    {
        base.Start();

        // Set the consumable name
        consumableName = "Gem";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onGemSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onGemPickedUp.Invoke(gameObject);
    }
}
