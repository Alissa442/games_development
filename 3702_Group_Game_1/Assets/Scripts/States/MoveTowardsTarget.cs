using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : IState
{
    public void Tick(StateMachine stateMachine)
    {
        // If there is no target, setState to null which will revert to the State Machines default state
        if (stateMachine.target == null)
        {
            stateMachine._agent.SetDestination(stateMachine.transform.position); // Stop moving if there is no target
            stateMachine.currentState = stateMachine.defaultState;
            return;
        }

        // If there is a target, move towards it
        stateMachine._agent.SetDestination(stateMachine.target.transform.position);
    }
}
