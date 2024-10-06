using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectXPoison : WinCondition
{
    public int poisonToCollect = 4;
    private int poisonCollected = 0;
    public TextMeshProUGUI poisonText;

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
        ConsumablePoison poison = consumable.GetComponent<ConsumablePoison>();
        if (poison == null)
        {
            //Debug.Log("Not poison");
            return;
        }

        // If so, increment the food collected count
        poisonCollected++;
        if (poisonCollected >= poisonToCollect)
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
        poisonText.text = $"Eat Poison \n{poisonCollected} / {poisonToCollect}";
    }

}
