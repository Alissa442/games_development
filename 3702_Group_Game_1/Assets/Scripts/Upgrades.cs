using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Upgrades : MonoBehaviour
{
    public Button plusButton;
    public Button minusButton;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI abilityText;
    public string playerPrefKey;
    public string abilityName;
    public string tooltipText;
    public int currentLevel; // Should be private
    public int maxLevel = 20;

    void OnEnable()
    {
        // Get the current level from the player prefs
        currentLevel = PlayerPrefs.GetInt(playerPrefKey, 0);

        // Update the UI
        UpdateTheUI();

        // Register with the plus and minus buttons
        plusButton.onClick.AddListener(OnPlusButtonClicked);
        minusButton.onClick.AddListener(OnMinusButtonClicked);
    }

    private void OnDisable()
    {
        // Unregister with the plus and minus buttons
        plusButton.onClick.RemoveListener(OnPlusButtonClicked);
        minusButton.onClick.RemoveListener(OnMinusButtonClicked);
    }

    public void OnPlusButtonClicked()
    {
        //Debug.Log("Plus Clicked");
        // Load the current currency from the player prefs
        int currentCurrency = PlayerPrefs.GetInt("PlayerCurrency", 0);

        // If the player is too poor to spend, ignore the click
        if (currentCurrency < 1)
            return;

        // If the player has reached the max level, ignore the click
        if (currentLevel >= maxLevel)
            return;

        currentCurrency -= 1;
        currentLevel += 1;

        PlayerPrefs.SetInt("PlayerCurrency", currentCurrency);
        PlayerPrefs.SetInt(playerPrefKey, currentLevel);
        PlayerPrefs.Save(); // Ensure the data is written to disk

        UpdateTheUI();
    }

    public void OnMinusButtonClicked()
    {
        //Debug.Log("Minus Clicked");
        // Don't let them go below level 0
        if (currentLevel < 1)
            return;

        int currentCurrency = PlayerPrefs.GetInt("PlayerCurrency", 0);
        currentLevel -= 1;
        currentCurrency += 1;

        PlayerPrefs.SetInt("PlayerCurrency", currentCurrency);
        PlayerPrefs.SetInt(playerPrefKey, currentLevel);
        PlayerPrefs.Save(); // Ensure the data is written to disk

        UpdateTheUI();
    }

    private void UpdateTheUI()
    {
        //Debug.Log("Level: " + currentLevel + " Currency: " + PlayerPrefs.GetInt("PlayerCurrency", 0));
        levelText.text = currentLevel.ToString();
        currencyText.text = PlayerPrefs.GetInt("PlayerCurrency", 0).ToString();
    }

    // This method is called when the script is loaded or a value is changed in the Inspector
    private void OnValidate()
    {
        abilityText.text = abilityName;
        this.name = abilityName + " Upgrade";
    }

}
