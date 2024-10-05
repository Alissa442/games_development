using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isGamePaused = false;
    public bool isGameWon = false;

    public List<WinCondition> winConditions = new List<WinCondition>();

    public GameObject winScreen;
    public GameObject loseScreen;
    public float delayBeforeLoadingNextScene = 4f;

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        // Check with each win condition objects to see if ALL win conditions are met
        foreach (WinCondition winCondition in winConditions)
        {
            // Ifany Win condition has declared the game lost, then the game is lost
            if (winCondition.HasLost)
            {
                ProcessGameHasBeenLost();
                return;
            }

            if (!winCondition.IsWinConditionMet)
            {
                isGameWon = false;
                return;
            }
        }

        // If all win conditions are met, then the game is won
        ProcessGameHasBeenWon();
    }

    // Win conditions register themselves with the WinConditionManager
    public void RegisterWinCondition(WinCondition winCondition)
    {
        winConditions.Add(winCondition);
    }

    private void ProcessGameHasBeenWon()
    {
        Debug.Log("Game has been won.");
        isGameWon = true;
        isGameOver = true;
        isGamePaused = true;

        // Display the win screen
        winScreen.SetActive(true);

        // Add Gems to PlayerGem in PlayerPrefs
        int currentCurrency = PlayerPrefs.GetInt("PlayerCurrency", 0);
        Rewards rewards = GetComponent<Rewards>();
        //Debug.Log("Existing Gems: " + currentCurrency + " reward gems: " + rewards.rewardAmount);
        currentCurrency += rewards.rewardAmount;
        PlayerPrefs.SetInt("PlayerCurrency", currentCurrency);
        PlayerPrefs.Save(); // Ensure the data is written to disk

        // Calls the coroutine LoadUpgradesSceneCoroutine
        StartCoroutine(LoadUpgradesSceneCoroutine());
    }

    private void ProcessGameHasBeenLost()
    {
        Debug.Log("Game has been lost.");
        isGameWon = false;
        isGameOver = true;
        isGamePaused = true;

        // Display the lose screen
        loseScreen.SetActive(true);

        // Calls the coroutine LoadUpgradesSceneCoroutine
        StartCoroutine(LoadUpgradesSceneCoroutine());
    }

    // Coroutine to load the Upgrades scene after a delay
    private IEnumerator LoadUpgradesSceneCoroutine()
    {
        // Wait before loading next scene
        yield return new WaitForSeconds(delayBeforeLoadingNextScene);

        // Load the Upgrades scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Upgrades");
    }
}
