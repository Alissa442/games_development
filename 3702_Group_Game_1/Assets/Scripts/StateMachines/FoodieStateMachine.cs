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
        currentState = defaultState;
    }

    ///// <summary>
    ///// When new food is spawned, check if the new food is closer
    ///// </summary>
    ///// <param name="newFood"></param>
    //void NewFood(GameObject newFood)
    //{
    //    // If there is no target, set the new food as the target
    //    if (target == null)
    //    {
    //        target = newFood;
    //        _agent.SetDestination(target.transform.position);
    //        return;
    //    }

    //    // If for some reason the new food is the same as the target, just return
    //    if (newFood == target)
    //        return;

    //    // If the new food is closer switch targets
    //    if (Vector3.Distance(transform.position, newFood.transform.position) < Vector3.Distance(transform.position, target.transform.position))
    //    {
    //        target = newFood;
    //        _agent.SetDestination(target.transform.position);
    //    }
    //}

    //private void OnDestroy()
    //{
    //    // Deregister the event to listen for new food
    //    gameGlobalEvents.onFoodSpawned.RemoveListener(NewFood);
    //}

}
