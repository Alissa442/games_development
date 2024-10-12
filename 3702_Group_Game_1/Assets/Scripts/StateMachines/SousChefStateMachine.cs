using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SousChefStateMachine : StateMachine
{
    public GameObject deliveryLocation;
    public ChefStateMachine chefStateMachine;
    public IngregientsCarrier carrier;

    //public Spawner foodSpawner;
    //public Spawner poisonSpawner;

    //public bool isChefRaging = false;

    void Start()
    {

        // Creates the States
        DeliverIngredientsState deliverIngredientsState = new DeliverIngredientsState(this);
        MoveTowardsTarget moveTowardsTargetState = new MoveTowardsTarget(this);
        LookingForIngredients lookingForIngredientsState = new LookingForIngredients(this);

        // Set the transitions for the states
        deliverIngredientsState.SetTransitions(lookingForIngredientsState);
        lookingForIngredientsState.SetTransitions(moveTowardsTargetState, deliverIngredientsState);
        moveTowardsTargetState.SetTransitions(lookingForIngredientsState);

        // Set the delivery location for the DeliverIngredients state
        deliveryLocation = GameObject.Find("DeliveryArea"); // Find object called DeliveryArea
        deliverIngredientsState.SetDeliveryLocation(deliveryLocation);

        // Set the default state
        defaultState = lookingForIngredientsState;

        // Sets the initial state
        ChangeState(defaultState);
    }

}
