using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefStateMachine : StateMachine
{
    [Tooltip("These are the enemies/player who supplied the ingredients")]
    public Queue<GameObject> suppliers = new Queue<GameObject>();
    [Tooltip("The spawner to use to spawn new food item")]
    public Spawner spawner;
    [Tooltip("How long it takes to turn ingredients into food")]
    public int cookingTime = 3;
    public float timeBeforeRage = 120.0f;
    public int supplierLifeReward = 10;
    public int rageDamagePerSecond = 5;

    public int currentIngredientsToCook = 0;
    public bool isEnraged = false;

    public float startedTime = 0.0f;

    void Start()
    {
        // Create the States
        Cooking cookingState = new Cooking(this);
        ChefIsRaging chefIsRagingState = new ChefIsRaging(this);
        ChefIsWaiting chefIsWaitingState = new ChefIsWaiting(this);

        // Set the transitions for the States
        cookingState.SetTransitions(chefIsWaitingState);
        chefIsRagingState.SetTransitions(chefIsWaitingState);
        chefIsWaitingState.SetTransitions(cookingState, chefIsRagingState);

        // Set the default state
        defaultState = chefIsWaitingState;

        // Sets the initial state
        ChangeState(defaultState);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Entry by: " + other.gameObject.name);

        // Checking if the object has an IngredientsCarrier component
        IngregientsCarrier carrier = other.GetComponent<IngregientsCarrier>();
        if (carrier == null)
        {
            return;
        }

        int amount = carrier.GetIngredientsCarried();
        if (amount > 0)
        {
            // Add the supplier to the queue
            for (int i = 0; i < amount; i++)
            {
                suppliers.Enqueue(other.gameObject);

                // Register supplier has delivered event
                gameGlobalEvents.onConsumableDeliveredUpByWhom.Invoke(other.gameObject);
            }
            currentIngredientsToCook += amount;
            carrier.RemoveIngredientsAmount(amount);
        }
    }

}
