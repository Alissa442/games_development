using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : IState
{
    public IState onTargetGone; // What state to change to when the target is gone
    private StateMachine _stateMachine; // The state machine that this state is attached to

    /// <summary>
    /// Constructor for the MoveTowardsTarget state
    /// </summary>
    /// <param name="stateMachine">The state machine that this is connected to</param>
    /// <param name="onTargetGone">Which state should it change to when the target becomes null</param>
    public MoveTowardsTarget(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Tick()
    {
        if (_stateMachine.target == null)
        {
            // If there is no target, stop moving
            _stateMachine._agent.SetDestination(_stateMachine.transform.position); // Stop moving if there is no target
            // Set state to the state that we've been told to change to.
            if (onTargetGone != null)
                _stateMachine.ChangeState(onTargetGone);
            else
                // If that state is null, set the state to the default state
                _stateMachine.ChangeState(_stateMachine.defaultState);
            return;
        }

        // If there is a target, tell the navmesh agent to move towards it
        _stateMachine._agent.SetDestination(_stateMachine.target.transform.position);

        _stateMachine.animator.SetBool("isIdle", false);
        _stateMachine.animator.SetBool("isAttacking", false);
        _stateMachine.animator.SetBool("isCasting", false);
        _stateMachine.animator.SetBool("isRunning", true);
    }

    /// <summary>
    /// Set the transitions for the state
    /// Only one transition is allowed for this state
    /// </summary>
    /// <param name="transitionsTo">What to transition to when the target no longer exists</param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        this.onTargetGone = transitionsTo[0];
    }
}
