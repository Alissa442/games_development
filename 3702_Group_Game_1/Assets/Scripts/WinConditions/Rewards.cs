using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    public int rewardAmount = 3;
    public TextMeshProUGUI rewardText;

    private void Start()
    {
        if (rewardText == null)
        {
            Debug.LogError("Reward Text is not assigned in the inspector!");
        }
        else
        {
            //Debug.Log("Reward Amount: " + rewardAmount);
            // Apply IncreasedRewards research from the playerprefs
            int increaseRewardsLevels = PlayerPrefs.GetInt("IncreaseRewards", 0);
            rewardAmount += Mathf.FloorToInt(increaseRewardsLevels * 0.1f);
            //Debug.Log("Reward Amount: " + rewardAmount);

            UpdateRewardText();
        }
    }

    // This method is called when the script is loaded or a value is changed in the Inspector
    private void OnValidate()
    {
        if (rewardText == null)
        {
            Debug.LogError("Reward Text is not assigned in the inspector!");
        }
        else
        {
            UpdateRewardText();
        }
    }

    private void UpdateRewardText()
    {
        rewardText.text = "x" + rewardAmount.ToString();
    }
}
