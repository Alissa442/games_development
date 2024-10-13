using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefIsWaiting : IState
{
    private StateMachine _stateMachine;
    private ChefStateMachine _chefStateMachine;

    private IState cookingState;
    private IState rageState;

    public ChefIsWaiting(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        this._chefStateMachine = (ChefStateMachine)stateMachine;
    }

    /// <summary>
    /// Sets the transitions for when the tantrum is done
    /// Please remember to set the Tantrum duration by called SetTantrumDuration(tantrumDuration)
    /// </summary>
    /// <param name="transitionsTo">What state to change to when the tantrum is done.</param>
    public void SetTransitions(params IState[] transitionsTo)
    {
        this.cookingState = transitionsTo[0];
        this.rageState = transitionsTo[1];
    }

    public void SetCookingState(IState cookingState)
    {
        this.cookingState = cookingState;
    }

    public void SetRageState(IState rageState)
    {
        this.rageState = rageState;
    }

    public void Tick()
    {
        if (_chefStateMachine.currentIngredientsToCook > 0)
        {
            //Debug.Log("Changing to cooking State");
            // Change state to cooking
            _stateMachine.currentState = cookingState;
            return;
        }

        if (Time.time >= _chefStateMachine.timeBeforeRage)
        {
            //Debug.Log("Changing to rage State");
            _chefStateMachine.isEnraged = true;
            // Change state to rage
            _stateMachine.currentState = rageState;
            return;
        }

        // Say random things every few seconds
        if (Time.time % 5 == 0)
        {
            _stateMachine.animator.SetBool("isAttacking", false);
            _stateMachine.animator.SetBool("isCasting", false);
            _stateMachine.animator.SetBool("isRunning", false);
            _stateMachine.animator.SetBool("isIdle", true);

            SayRandomThings();
        }

    }

    private void SayRandomThings()
    {
        string[] randomThingsToSay = new string[]
        {
            "I am bored doing nothing! I want to cook!",
            "Bring me ingredients to cook with!",
            "Don't you want to eat?",
            "Hurry up, the customers are waiting!",
            "Time is money, let's go!",
            "Chop-chop, let's get this done!",
            "We can't keep them waiting, speed it up!",
            "This isn't a vacation, get moving!",
            "We need to pick up the pace!",
            "Let's hustle, people are hungry!"
        };

        int randomIndex = Random.Range(0, randomThingsToSay.Length);
        Debug.Log(randomThingsToSay[randomIndex]);
    }

}
