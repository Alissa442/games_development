using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefIsRaging : IState
{
    private StateMachine _stateMachine;
    private ChefStateMachine _chefStateMachine;

    private float nextRageTime = 0f;

    private IState transitionState;

    private Health[] healths; // All the enemies and players who have a health component

    public ChefIsRaging(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        this._chefStateMachine = (ChefStateMachine)stateMachine;
    }

    /// <summary>
    /// Sets the transitions for when the tantrum is done
    /// Please remember to set the Tantrum duration by called SetTantrumDuration(tantrumDuration)
    /// </summary>
    /// <param name="transitionsTo">What state to change to when the tantrum is done.</param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        this.transitionState = transitionsTo[0]; // All the cool kids are doing this, right?
    }

    public void Tick()
    {
        // It won't transition out... once raging, always raging

        if (Time.time <= nextRageTime)
        {
            return;
        }

        // Time to rage again

        // Play animation
        Debug.Log("Raging - pew pew pew");

        // Set the next time to rage
        nextRageTime = Time.time + 1f;

        // Find all the health components in the scene
        healths = GameObject.FindObjectsOfType<Health>();

        // Loop through all the health components and damage them
        foreach (Health health in healths)
        {
            if (health != null)
                health.TakeUnavoidableDamage(_chefStateMachine.rageDamagePerSecond);
        }
    }

}
