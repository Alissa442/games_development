using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It searches for the nearest good in the scene and sets the target to it
/// It will not search for another food if one spawns closer
/// </summary>
public class InefficientSearchForFood : IState
{
    private StateMachine _stateMachine;
    public IState onTargetFound;

    public InefficientSearchForFood (StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Tick()
    {

        // Check for food. If there is no food the foodie is not interested
        // return
        if (_stateMachine.gameState.food.Count == 0)
        {
            _stateMachine.target = null; // Defensive programming just in case

            // Do nothing. Just stare at the wall
            return;
        }

        // Use the helper function in gameState to find the closest food
        GameObject closestFood = _stateMachine.gameState.FindClosestItem(
            _stateMachine.gameObject,
            _stateMachine.gameState.food
        );

        // If there is no food, abort.
        if (closestFood == null)
        {
            return;
        }

        // Set the destination to the closest food
        _stateMachine.target = closestFood;
        if (onTargetFound != null)
            _stateMachine.currentState = onTargetFound;
        else
            // If that state is null, set the state to the default state
            _stateMachine.currentState = _stateMachine.defaultState;
    }

    /// <summary>
    /// Set the state that will be changed to when a food is found
    /// 
    /// </summary>
    /// <param name="transitionsTo"></param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        onTargetFound = transitionsTo[0];
    }
}
