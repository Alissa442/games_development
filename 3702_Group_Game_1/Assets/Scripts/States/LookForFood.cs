using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForFood : IState
{
    public void Tick(StateMachine stateMachine)
    {
        // Find all objects of type ConsumableFood in the scene
        ConsumableFood[] foods = GameObject.FindObjectsOfType<ConsumableFood>();

        // If there are no foods, return
        if (foods.Length == 0)
        {
            return;
        }

        // Find the closest food
        Transform closestFood = null;
        float closestDistance = Mathf.Infinity;
        Vector3 agentPosition = stateMachine._agent.transform.position;

        foreach (var food in foods)
        {
            float distance = Vector3.Distance(agentPosition, food.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestFood = food.transform;
            }
        }

        // Set the destination to the closest food
        if (closestFood != null)
        {
            stateMachine._agent.SetDestination(closestFood.position);

            // Change the state to MoveTowardsTarget
            stateMachine.currentState = new MoveTowardsTarget();
        }
    }
}
