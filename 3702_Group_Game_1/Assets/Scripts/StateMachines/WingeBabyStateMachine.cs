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
        // Set the nerd rage threshold to 10 and the tantrum threshold to 3
        wingeMoveTowardsTarget.SetThresholds(10, 3);
        tantrumState.SetTransitions(lookForConsumableBalancedState);
        // Set the tantrum duration to 3 seconds
        tantrumState.SetTantrumDuration(3.0f);

        // Set the default state
        defaultState = lookForConsumableBalancedState;

        // Starts in look for consumables
        ChangeState(defaultState);
    }

}
