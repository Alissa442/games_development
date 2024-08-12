using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpeedBoost : MonoBehaviour
{
    [SerializeField] private float speedBoost = 3f;
    [SerializeField] private float lengthOfTime = 15f;

    private void OnTriggerEnter(Collider other)
    {
        Speed speed = other.GetComponent<Speed>();
        // Can't be eaten by something that doesn't have health
        if (speed == null) return;

        speed.IncreaseSpeed(speedBoost, lengthOfTime);
    }
}
