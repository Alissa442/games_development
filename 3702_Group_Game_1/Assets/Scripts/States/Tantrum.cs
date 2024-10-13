using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tantrum : IState
{
    private StateMachine _stateMachine;
    public float tantrumDuration;
    public IState onTantrumDone;

    private bool isTantruming = false;
    private float tantrumEndTime;

    public Tantrum(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        this.tantrumDuration = 3f; // Sets the default time in case it's not set
    }

    /// <summary>
    /// Sets the duration of the tantrum
    /// </summary>
    /// <param name="tantrumDuration">Duration of the Tantrum</param>
    public void SetTantrumDuration(float tantrumDuration)
    {
        this.tantrumDuration = tantrumDuration;
    }

    /// <summary>
    /// Sets the transitions for when the tantrum is done
    /// Please remember to set the Tantrum duration by called SetTantrumDuration(tantrumDuration)
    /// </summary>
    /// <param name="transitionsTo">What state to change to when the tantrum is done.</param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        this.onTantrumDone = transitionsTo[0];
    }

    public void Tick()
    {
        // Start the tantrum
        if (!isTantruming)
        {
            isTantruming = true;
            tantrumEndTime = Time.time + tantrumDuration;

            Debug.Log("Hey, that was mine. Starting Tantrum.");
            // TODO Start Tantrum animation            
            
            // Set animation to idling
            _stateMachine.animator.SetBool("isAttacking", false);
            _stateMachine.animator.SetBool("isCasting", false);
            _stateMachine.animator.SetBool("isRunning", false);
            _stateMachine.animator.SetBool("isIdle", false);
            _stateMachine.animator.SetBool("isTantrum", true);
        }

        if (tantrumEndTime < Time.time)
        {
            // Set the tantrum to be over (false)
            isTantruming = false;

            Debug.Log("Tantrum Over");
            // TODO Stop the tantrum animation
            _stateMachine.animator.SetBool("isTantrum", false);
            _stateMachine.animator.SetBool("isIdle", true);

            // Change state to onTantrumDone now that the tantrum is over.
            _stateMachine.currentState = onTantrumDone;
        }
    }
}
