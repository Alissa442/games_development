using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMine : Consumable
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float damage = 10f;
    bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return; // Prevents double picking up of the consumable

        // Check if the other object is the player or enemy
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy")) return;

        // Smack all things in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hitCollider in hitColliders)
        {
            Health health = hitCollider.GetComponent<Health>();
            // If it has health, go kablooie
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Doing damage to " + hitCollider.name);
            }
        }

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
        consumableName = "Mine";

        // Trigger the event that this food has spawned
        gameGlobalEvents.onMineSpawned.Invoke(gameObject);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Trigger the event that this food has been picked up
        gameGlobalEvents.onMinePickedUp.Invoke(gameObject);
    }
}
