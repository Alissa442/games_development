using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : IState
{
    private StateMachine _stateMachine;
    private ChefStateMachine _chefStateMachine;
    private bool isTantruming = false;
    private float tantrumEndTime;

    private IState waitingState;

    private bool isCooking = false;
    private float cookingEndTime;

    /// <summary>
    /// Sets the transitions for when the tantrum is done
    /// Please remember to set the Tantrum duration by called SetTantrumDuration(tantrumDuration)
    /// </summary>
    /// <param name="transitionsTo">What state to change to when the tantrum is done.</param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        this.waitingState = transitionsTo[0];
    }

    public Cooking(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        this._chefStateMachine = (ChefStateMachine)stateMachine;
    }

    public void Tick()
    {
        // Keep cooking until there are no more ingredients
        if (_chefStateMachine.currentIngredientsToCook <= 0)
        {
            //Debug.Log("Changing to Waiting State");
            // Transition to the next state
            _stateMachine.currentState = waitingState;
        }

        if (!isCooking && _chefStateMachine.currentIngredientsToCook > 0)
        {
            isCooking = true;
            cookingEndTime = Time.time + _chefStateMachine.cookingTime;
            Debug.Log($"Starting to cook {_chefStateMachine.currentIngredientsToCook} ingredients availabble.");
            // TODO Start cooking animation
        }

        if (isCooking && Time.time >= cookingEndTime && _chefStateMachine.currentIngredientsToCook > 0) {
            // TODO Update cooking animation

            isCooking = false;
            Debug.Log("Finished cooking. Dishing Up.");

            // Spawn a food item
            _chefStateMachine.spawner.ForceSpawn();

            // Remove the ingredients from the queue
            _chefStateMachine.currentIngredientsToCook--;

            // dequeue the supplier
            var supplier = _chefStateMachine.suppliers.Dequeue();

            // Reward the supplier with extra life
            supplier.GetComponent<Health>().Heal(_chefStateMachine.supplierLifeReward);
        }

    }

}
