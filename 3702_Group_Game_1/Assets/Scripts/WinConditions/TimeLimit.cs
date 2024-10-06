using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLimit : WinCondition
{
    public int timeLimit = 15;
    public TextMeshProUGUI timeText;

    private float timeRemaining;

    void Start()
    {
        timeRemaining = timeLimit;
        IsWinConditionMet = true;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            // Out of time
            IsWinConditionMet = false;      // Change the tick to a cross
            HasLost = true;                 // The player has lost - End of game trigger
        }
    }

    void UpdateUI()
    {
        // Round the time remaining up to the nearest second

        int timeToDisplay = Mathf.CeilToInt(timeRemaining);
        timeToDisplay = Mathf.Max(0, timeToDisplay); // Don't show less than 0 seconds remaining

        // Update the UI to show the highest range and range to get
        timeText.text = $"Time Limit\n{timeToDisplay} / {timeLimit}";
    }

    protected override void OnValidate()
    {
        // Call the base class's OnValidate method
        base.OnValidate();

        // Derived class validation logic
        UpdateUI();
    }

}
