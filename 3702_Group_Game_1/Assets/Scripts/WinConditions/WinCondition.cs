using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinCondition : MonoBehaviour
{
    [Tooltip("The uiPanel connected to this win condition.")]
    public GameObject uiPanel;
    [Tooltip("The UI element to show when the win condition is met.")]
    public GameObject uiWinConditionTrue;
    [Tooltip("The UI element to show when the win condition is not met.")]
    public GameObject uiWinConditionFalse;

    [Tooltip("If the win condition for this win condition type is met, set this to true.")]
    private bool isWinConditionMet = false;
    [Tooltip("If the player has lost and can never win this condition, set this to true.")]
    private bool hasLost = false;

    public bool IsWinConditionMet
    {
        get { return isWinConditionMet; }
        set { 
            isWinConditionMet = value;
            if (isWinConditionMet)
            {
                uiWinConditionTrue.SetActive(true);
                uiWinConditionFalse.SetActive(false);
            }
            else
            {
                uiWinConditionTrue.SetActive(false);
                uiWinConditionFalse.SetActive(true);
            }
        }
    }

    public bool HasLost
    {
        get { return hasLost; }
        set 
        { 
            hasLost = value;
            if (hasLost)
            {
                uiWinConditionTrue.SetActive(false);
                uiWinConditionFalse.SetActive(true);
            }
            else
            {
                uiWinConditionTrue.SetActive(true);
                uiWinConditionFalse.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        // If enabled at the start of game play, register this win condition with WinConditionManager
        WinConditionManager winConditionManager = GetComponent<WinConditionManager>(); // It will be on the same object

        // Register this win condition with the WinConditionManager
        if (winConditionManager != null)
            winConditionManager.RegisterWinCondition(this);
    }

    // This method is called when the script is loaded or a value is changed in the Inspector
    protected virtual void OnValidate()
    {
        // If this script is enabled, show it in the UI panel, otherwise hide it
        if (uiPanel == null)
            Debug.LogError("uiPanel is not assigned in the inspector!");
        else
        {
            if (this.enabled)
                uiPanel.SetActive(true);
            else
                uiPanel.SetActive(false);
        }
    }
}
