using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePoison : MonoBehaviour
{
    [SerializeField] private float healthValue = 20f;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        // Can't be eaten by something that doesn't have health
        if (health == null) return;

        health.TakeDamage(healthValue);
    }
}
