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
    [Tooltip("How long before the entity dies. If 0, it will die immediately. If greater than 0, it will die after that time.")]
    public float timeToDie = 0f;

    [Tooltip("The event that will be invoked when this entity dies.")]
    public UnityEvent onDeath;

    [Tooltip("The game objects that will spawn when this entity dies. Typically this is the effects of the entity dying. Visual or sound effects.")]
    public GameObject[] spawnsOnDeath;

    [Tooltip("If true, the object will be returned to the object pool when it dies. Not yet enabled.")]
    public bool useObjectPool = false;

    [Tooltip("Is this dead? Read only attribute")]
    [ReadOnly]
    public bool isDead = false;

    /// <summary>
    /// Invoke this method when an entity dies
    /// </summary>
    [MethodButton("Invoke Death")]
    public void Invoke()
    {
        // Don't do anything if the entity is already dead
        if (isDead)
            return;

        // Invoke the death event to call the appropriate methods.
        onDeath.Invoke();

        SpawnDeathEffects();

        // Set it to dead so it can't die again
        isDead = true;

        if (timeToDie <= 0)
            ItsDeadJim();
        else
        {
            //Debug.Log("I'm dying");
            StartCoroutine(DieCoroutine());
        }
    }

    /// <summary>
    /// Spawn the Death Effects
    /// </summary>
    [MethodButton("Spawn Death Effects")]
    public void SpawnDeathEffects()
    {
        foreach (GameObject spawn in spawnsOnDeath)
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
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
        //Debug.Log("I'm dead");
        Destroy(gameObject); // Of course we should be using an object pool
    }

    /// <summary>
    /// Resetting in case we want to use the same entity again
    /// in a object pool
    /// </summary>
    private void OnEnable()
    {
        isDead = false;
    }

    /// <summary>
    /// Resetting in case we want to use the same entity again
    /// for instance in an object pool
    /// </summary>
    private void OnDisable()
    {
        isDead = false;
        StopAllCoroutines();
    }
}
