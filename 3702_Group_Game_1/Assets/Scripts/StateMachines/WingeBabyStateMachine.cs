using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingeBabyStateMachine : StateMachine
{
    private GameGlobalEventsSO _gameGlobalEvents;

    void Start()
    {
        // Create the States
        LookForConsumableBalanced lookForConsumableBalancedState = new LookForConsumableBalanced(this);
        Tantrum tantrumState = new Tantrum(this);
        WingeMoveTowardsTarget wingeMoveTowardsTarget = new WingeMoveTowardsTarget(this);

        // Set the transitions for the states
        lookForConsumableBalancedState.SetTransitions(wingeMoveTowardsTarget);
        wingeMoveTowardsTarget.SetTransitions(lookForConsumableBalancedState, tantrumState);
        tantrumState.SetTransitions(lookForConsumableBalancedState);

        // Set the default state
        defaultState = lookForConsumableBalancedState;

        // Starts in look for consumables
        ChangeState(defaultState);
    }

}
