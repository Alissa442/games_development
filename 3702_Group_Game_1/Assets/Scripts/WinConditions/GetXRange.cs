using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetXRange : WinCondition
{
    public float rangeToGet = 4;
    public TextMeshProUGUI rangeText;

    private GameObject player;
    private Range playerRange;

    private float highestRange = 1f;

    void Start()
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player");
        playerRange = player.GetComponent<Range>();

    }

    void Update()
    {
        CheckPlayersRange();
    }

    private void CheckPlayersRange()
    {
        if (playerRange.range > highestRange)
        {
            highestRange = playerRange.range;
            UpdateUI();
        }

        if (highestRange >= rangeToGet)
        {
            IsWinConditionMet = true;
        }
    }

    private void UpdateUI()
    {
        // Update the UI to show the highest range and range to get
        rangeText.text = $"Get Range\n{highestRange} / {rangeToGet}";
    }

    protected override void OnValidate()
    {
        // Call the base class's OnValidate method
        base.OnValidate();

        // Derived class validation logic
        UpdateUI();
    }

}
