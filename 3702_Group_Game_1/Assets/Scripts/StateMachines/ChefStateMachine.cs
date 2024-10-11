using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefStateMachine : StateMachine
{
    [Tooltip("These are the enemies/player who supplied the ingredients")]
    public Queue<GameObject> suppliers = new Queue<GameObject>();
    [Tooltip("How long it takes to turn ingredients into food")]
    public int cookingTime = 3;
    public float timeBeforeRage = 120.0f;

    public int currentIngredientsToCook = 0;
    public bool isEnraged = false;

    void Start()
    {
        
    }

}
