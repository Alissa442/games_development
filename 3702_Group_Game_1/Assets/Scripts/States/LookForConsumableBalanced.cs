using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LookForConsumableBalanced : IState
{
    private StateMachine _stateMachine; // The state machine that this state is attached to

    public IState onFoundConsumable; // What state to change to when the consumable is found

    // The higher the number, the higher the priority
    const float NORMAL_FOOD_PRIORITY = 0.5f;
    const float HIGH_FOOD_PRIORITY = 3f;

    // Consumable Priorities Array
    private float[] priorities = new float[3];

    public LookForConsumableBalanced(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;

        // Building the prorities array
        priorities[0] = NORMAL_FOOD_PRIORITY;   // Food priority
        priorities[1] = 1.5f;                   // Speed priority
        priorities[2] = 1f;                     // Range priority
    }

    public void SetTransitions(params IState[] transitionsTo)
    {
        onFoundConsumable = transitionsTo[0];
    }

    public void Tick()
    {
        if (_stateMachine.health <= 10)
        {
            // If the health is low, prioritise health
            //Debug.Log("Food is now a priority");
            priorities[0] = HIGH_FOOD_PRIORITY;
        }
        else
            priorities[0] = NORMAL_FOOD_PRIORITY;

        // Build the closestItems Array
        GameObject[] closestItems = new GameObject[3];

        // Use the helper function in gameState to find the closest food
        closestItems[0] = _stateMachine.gameState.FindClosestItem(
            _stateMachine.gameObject,
            _stateMachine.gameState.food
        );

        // Use the helper function in gameState to find the closest speed
        closestItems[1] = _stateMachine.gameState.FindClosestItem(
            _stateMachine.gameObject,
            _stateMachine.gameState.speedBoosts
        );

        // Use the helper function in gameState to find the closest range
        closestItems[2] = _stateMachine.gameState.FindClosestItem(
            _stateMachine.gameObject,
            _stateMachine.gameState.rangeBoosts
        );

        float closestDistance = float.MaxValue;
        GameObject closestItem = null;

        // Calculate with priority, which item to go for
        for(int i = 0; i < closestItems.Length; i++)
        {
            if (closestItems[i] == null)
                continue;

            float weightedRange = GetWeightedRange(closestItems[i], priorities[i]);

            if (weightedRange < closestDistance)
            {
                closestDistance = weightedRange;
                closestItem = closestItems[i];
            }
        }

        if (closestItem == null)
        {
            // Set animation to idling
            _stateMachine.animator.SetBool("isAttacking", false);
            _stateMachine.animator.SetBool("isCasting", false);
            _stateMachine.animator.SetBool("isRunning", false);
            _stateMachine.animator.SetBool("isIdle", true);

            return;
        }

        // Found the next target
        _stateMachine.target = closestItem;
        _stateMachine.ChangeState(onFoundConsumable);

    }

    private float GetWeightedRange(GameObject obj, float weight)
    {
        float distance = Vector3.Distance(_stateMachine.gameObject.transform.position, obj.transform.position);
        // If distance is basically 0, return infinity
        if (Mathf.Approximately(distance, 0))
            return float.MaxValue;

        return distance * (1 / weight);
    }

}
