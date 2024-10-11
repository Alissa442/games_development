using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngregientsCarrier : MonoBehaviour
{
    public int ingredientsCarried = 0;

    public int GetIngredientsCarried()
    {
        return ingredientsCarried;
    }

    public void AddIngredientsCarried(int amount)
    {
        ingredientsCarried += amount;
    }

}
