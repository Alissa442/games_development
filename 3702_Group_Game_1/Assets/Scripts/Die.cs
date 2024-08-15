using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the death script for all entities in the game
/// When an enemy or player dies, this script will be called
/// </summary>
public class Die : MonoBehaviour
{
    public bool isDead = false;
    public float timeToDie = 2f; // How long before you kill it so you can play animations

    public UnityEvent onDeath;

    // The game objects that will spawn when this entity dies
    // Typically this is the effects of the entity dying
    // Visual or sound effects
    public GameObject[] spawnsOnDeath;

    /// <summary>
    /// Invoke this method when an entity dies
    /// </summary>
    public void Invoke()
    {
        // Don't do anything if the entity is already dead
        if (isDead)
            return;

        // Invoke the appropriate event
        onDeath.Invoke();

        // Spawn the effects of the entity dying
        foreach (GameObject spawn in spawnsOnDeath)
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
        }

        // Set it to dead so it can't die again
        isDead = true;

        if (timeToDie <= 0)
            ItsDeadJim();
        else
        {
            Debug.Log("I'm dying");
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(timeToDie);
        ItsDeadJim();
    }

    /// <summary>
    /// Finally we kill it. Put it out of its misery
    /// </summary>
    private void ItsDeadJim()
    {
        Debug.Log("I'm dead");
        Destroy(gameObject);
    }

}
