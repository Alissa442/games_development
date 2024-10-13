using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverIngredientsState : IState
{
    private StateMachine _stateMachine; // The state machine that this state is attached to
    private GameObject deliveryLocation; // The location that deliveries are made to
    private IState onDeliveryMade; // What state to change to when the delivery is made

    private bool isDelivering = false;

    public DeliverIngredientsState(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Tick()
    {
        if (!isDelivering)
        {
            Debug.Log("Starting delivery");
            _stateMachine._agent.SetDestination(deliveryLocation.transform.position);
            isDelivering = true;

            // Set animation to running
            _stateMachine.animator.SetBool("isIdle", false);
            _stateMachine.animator.SetBool("isAttacking", false);
            _stateMachine.animator.SetBool("isCasting", false);
            _stateMachine.animator.SetBool("isRunning", true);
        }
        else
        {
            // If we've made it to the delivery destination, change state
            if (HasReachedDestination())
            {
                Debug.Log("Delivery Made. Switching to searching.");
                isDelivering = false;
                _stateMachine.ChangeState(onDeliveryMade);
            }
        }
    }

    public void SetDeliveryLocation(GameObject deliveryLocation)
    {
        this.deliveryLocation = deliveryLocation;
    }

    /// <summary>
    /// Set the transitions for the state
    /// Only one transition is allowed for this state
    /// </summary>
    /// <param name="transitionsTo">What to transition to when the target no longer exists</param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        this.onDeliveryMade = transitionsTo[0];
    }

    private bool HasReachedDestination()
    {
        //Debug.Log("Checking if delivery has been made");
        // If distance between the agent and the delivery location is less than 1f, then the delivery has been made
        if (Vector3.Distance(_stateMachine.transform.position, deliveryLocation.transform.position) < 1f)
        {
            return true;
        }

        return false;
    }

}
