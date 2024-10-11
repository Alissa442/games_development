using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliverXIngredients : WinCondition
{
    public int ingredientsToDeliver = 2;
    private int ingredientsDelivered = 0;
    public TextMeshProUGUI foodText;

    private GameObject player;

    private GameGlobalEventsSO _gameGlobalEvents;

    void Start()
    {
        _gameGlobalEvents = GameStateSO.Instance.gameGlobalEvents;

        // Register with the ingredient collect script the event of ingredient delivery
        _gameGlobalEvents.onConsumableDeliveredUpByWhom.AddListener(OnConsumableDelivered);


        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");

        UpdateUI();
    }

    private void OnDestroy()
    {
        // Unregister with the ingredient collect script the event of ingredient delivery
        _gameGlobalEvents.onConsumableDeliveredUpByWhom.RemoveListener(OnConsumableDelivered);
    }

    public void OnConsumableDelivered(GameObject consumable, GameObject whoPickedItUp)
    {
        // Check the ingredient was collected by the player
        if (whoPickedItUp != player)
        {
            //Debug.Log("Not picked up by player");
            return;
        }

        // Check it was a Ingredient that was picked up
        ConsumableIngredient item = consumable.GetComponent<ConsumableIngredient>();
        if (item == null)
        {
            //Debug.Log("Not ingredient");
            return;
        }

        // If so, increment the ingredient collected count
        ingredientsDelivered++;
        if (ingredientsDelivered >= ingredientsToDeliver)
        {
            IsWinConditionMet = true;
        }

        UpdateUI();
    }

    protected override void OnValidate()
    {
        // Call the base class's OnValidate method
        base.OnValidate();

        // Derived class validation logic
        UpdateUI();
    }

    public void UpdateUI()
    {
        foodText.text = $"Ingredient Delivered \n{ingredientsDelivered} / {ingredientsToDeliver}";
    }

}
