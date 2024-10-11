using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectXIngredients : WinCondition
{
    public int amountToCollect = 2;
    private int itemsCollected = 0;
    public TextMeshProUGUI uiText;

    private GameObject player;

    private GameGlobalEventsSO _gameGlobalEvents;

    void Start()
    {
        _gameGlobalEvents = GameStateSO.Instance.gameGlobalEvents;

        // Register with the food collect script the event of food collection
        _gameGlobalEvents.onConsumablePickedUpByWhom.AddListener(OnConsumableCollected);


        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");

        UpdateUI();
    }

    private void OnDestroy()
    {
        // Unregister with the food collect script the event of food collection
        _gameGlobalEvents.onConsumablePickedUpByWhom.RemoveListener(OnConsumableCollected);
    }

    public void OnConsumableCollected(GameObject consumable, GameObject whoPickedItUp)
    {
        // Check the food was collected by the player
        if (whoPickedItUp != player)
        {
            //Debug.Log("Not picked up by player");
            return;
        }

        // Check it was a ingredient that was picked up
        ConsumableIngredient item = consumable.GetComponent<ConsumableIngredient>();
        if (item == null)
        {
            //Debug.Log("Not Ingredient");
            return;
        }

        // If so, increment the ingredient collected count
        itemsCollected++;
        if (itemsCollected >= amountToCollect)
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
        uiText.text = $"Get Items\n{itemsCollected} / {amountToCollect}";
    }
}
