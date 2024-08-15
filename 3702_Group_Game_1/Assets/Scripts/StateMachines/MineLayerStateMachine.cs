using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MineLayerStateMachine : StateMachine
{
    [Tooltip("These are the mine prefabs that will be laid as a mine")]
    public GameObject[] minePrefabs;

    void Start()
    {
        // Create the States
        LayAMine layAMineState = new LayAMine(this);
        InefficientSearchForFood inefficientSearchForFoodState = new InefficientSearchForFood(this);
        MoveTowardsTarget moveTowardsTargetState = new MoveTowardsTarget(this);

        // Set the transitions for the states
        layAMineState.SetTransitions(inefficientSearchForFoodState);
        inefficientSearchForFoodState.SetTransitions(moveTowardsTargetState);
        moveTowardsTargetState.SetTransitions(layAMineState);

        // Sets the configuration for each state
        layAMineState.SetMinePrefabs(minePrefabs);  // The pool of mines to lay
        layAMineState.SetHealthThreshold(15);       // If the health is 15 or less, hunt for food
        layAMineState.SetMineLayingTime(3.0f);      // How long it takes to lay a mine

        // Set the default state
        defaultState = layAMineState;

        // Sets the initial state
        ChangeState(defaultState);
    }

}
