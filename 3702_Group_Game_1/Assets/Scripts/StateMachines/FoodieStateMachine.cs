using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Simple State Machine that will look for food and move towards it
/// It will only chase food. If there is no food, it will sit and stare at the wall
/// </summary>
public class FoodieStateMachine : StateMachine
{

    void Start()
    {
        // Create the States
        MoveTowardsTarget moveTowardsState = new MoveTowardsTarget(this);
        InefficientSearchForFood inefficientSearchForFoodState = new InefficientSearchForFood(this);

        // Set the transitions for the states
        moveTowardsState.SetTransitions(inefficientSearchForFoodState);
        inefficientSearchForFoodState.SetTransitions(moveTowardsState);

        // Set the default state
        defaultState = inefficientSearchForFoodState;

        // Starts in look for food state
        ChangeState(defaultState);
    }

}
