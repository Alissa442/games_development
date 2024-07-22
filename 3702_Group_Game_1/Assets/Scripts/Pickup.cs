using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    private bool isPickedUp = false;
    public UnityEvent onPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        // If it's already been picked up, don't pick up again
        if (isPickedUp) return;

        isPickedUp = true;
        //Debug.Log("Picked up a pickup!");
        onPickedUp.Invoke();

        Destroy(gameObject);
    }

}
