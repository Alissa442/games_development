using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectXFood : WinCondition
{
    public int foodToCollect = 4;
    private int foodCollected = 0;
    public TextMeshProUGUI foodText;

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

        // Check it was a food that was picked up
        ConsumableFood food = consumable.GetComponent<ConsumableFood>();
        if (food == null)
        {
            //Debug.Log("Not food");
            return;
        }

        // If so, increment the food collected count
        foodCollected++;
        if (foodCollected >= foodToCollect)
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
        foodText.text = $"Eat Food \n{foodCollected} / {foodToCollect}";
    }

}
