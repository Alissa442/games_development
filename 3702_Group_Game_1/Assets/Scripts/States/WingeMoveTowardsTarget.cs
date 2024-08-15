using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingeMoveTowardsTarget : IState
{
    public IState onTargetStolen; // What state to change to when someone else reached the target first
    public IState onReachedTarget; // What state to change to when the target is reached
    private StateMachine _stateMachine; // The state machine that this state is attached to

    private int stolenInARow = 0; // How many in a row have been stolen from the NPC

    private int iQuitThreshold = 10;
    private int tantrumThreshold = 3;

    public WingeMoveTowardsTarget(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    /// <summary>
    /// Transition states for this state
    /// First transition is when the target is reached
    /// Second transition is when the target is stolen
    /// </summary>
    /// <param name="transitionsTo"></param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        onReachedTarget = transitionsTo[0];
        onTargetStolen = transitionsTo[1];
    }

    public void SetThresholds(int iQuit, int tantrum)
    {
        iQuitThreshold = iQuit;
        tantrumThreshold = tantrum;
    }

    public void Tick()
    {
        if (_stateMachine.target != null)
        {
            // If there is a target, tell the navmesh agent to move towards it
            _stateMachine._agent.SetDestination(_stateMachine.target.transform.position);
            return;
        }

        // Target is gone.

        // Check if the Winge got it themselves
        if (_stateMachine.gameObject == _stateMachine.whoPickedUpLastConsumable)
        {
            // haha, I got it! Move on to the next target like a trooper
            Debug.Log("Haha... I got it.");
            stolenInARow = 0;       // Reset the losing streak
            ChangeToNextTarget();
            return;
        }

        // If they've lost y in a row, they'll get upset and leave the game
        if (stolenInARow >= 10)
        {
            Debug.Log("That's it. Screw you all. I quit!");

            // Find the I Quit component and run the Invoke
            // This will destroy this character
            _stateMachine.GetComponent<IQuit>().Invoke();

            return;
        }

        // If they've lost x in a row, they'll get upset and winge.
        if (stolenInARow > 3)
        {
            // If the Winger is upset, they will winge every time it's stolen from them
            stolenInARow++; // Increment the stolen in a row
            HaveAWinge();
            return;
        }

        Debug.Log("Damn... you beat me to it.");

        stolenInARow++;
        ChangeToNextTarget();
    }

    private void HaveAWinge()
    {
        // It was stolen! Waah waah
        _stateMachine._agent.SetDestination(_stateMachine.transform.position); // Stop moving if there is no target
        _stateMachine.ChangeState(onTargetStolen);
    }

    private void ChangeToNextTarget()
    {
        // haha, I got it! Move on to the next target
        _stateMachine._agent.SetDestination(_stateMachine.transform.position); // Stop moving if there is no target
        _stateMachine.ChangeState(onReachedTarget);
    }

}
