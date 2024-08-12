using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableRangeBoost : MonoBehaviour
{
    [SerializeField] private float rangeBoost = 3f;
    [SerializeField] private float lengthOfTime = 15f;

    private void OnTriggerEnter(Collider other)
    {
        Range range = other.GetComponent<Range>();
        // Can't be eaten by something that doesn't have health
        if (range == null) return;

        range.IncreaseRange(rangeBoost, lengthOfTime);
    }
}
