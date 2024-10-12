using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForIngredients : IState
{
    private StateMachine _stateMachine; // The state machine that this state is attached to

    public IState onFoundConsumable; // What state to change to when the consumable is found
    public IState onIngredientInInventory; // What state to change to when the ingredient is in the inventory

    // The higher the number, the higher the priority
    const float NORMAL_FOOD_PRIORITY = 0.5f;
    const float HIGH_FOOD_PRIORITY = 3f;

    // Consumable Priorities Array
    private float[] priorities = new float[10];
    private IngregientsCarrier carrier;

    public LookingForIngredients(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;

        // Building the prorities array
        priorities[0] = NORMAL_FOOD_PRIORITY;   // Food priority
        priorities[1] = 1.5f;                   // Speed priority
        priorities[2] = 0.75f;                  // Range priority
        priorities[3] = 2f;                     // Ingredient priority
        priorities[4] = 3f;                     // Gem priority

        // Go and find the IngredientsCarrier component and cache it
        carrier = _stateMachine.GetComponent<IngregientsCarrier>();
    }

    public void SetTransitions(params IState[] transitionsTo)
    {
        onFoundConsumable = transitionsTo[0];
        onIngredientInInventory = transitionsTo[1];
    }

    public void Tick()
    {
        // Check if the ingredient is in the inventory
        // Switch to delivering the ingredient
        if (carrier.GetIngredientsCarried() >= 1)
        {
            _stateMachine.ChangeState(onIngredientInInventory);
            return;
        }

        if (_stateMachine.health <= 10)
        {
            // If the health is low, prioritise health
            //Debug.Log("Food is now a priority");
            priorities[0] = HIGH_FOOD_PRIORITY;
        }
        else
        {
            priorities[0] = NORMAL_FOOD_PRIORITY;
        }

        // Build the closestItems Array
        GameObject[] closestItems = new GameObject[10];

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

        closestItems[3] = _stateMachine.gameState.FindClosestItem(
            _stateMachine.gameObject,
            _stateMachine.gameState.ingredients
        );

        closestItems[4] = _stateMachine.gameState.FindClosestItem(
            _stateMachine.gameObject,
            _stateMachine.gameState.gems
        );

        float closestDistance = float.MaxValue;
        GameObject closestItem = null;

        // Calculate with priority, which item to go for
        for (int i = 0; i < closestItems.Length; i++)
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
