using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public Button playButton;
    public Button unlockButton;
    public Button levelButton;
    public TextMeshProUGUI levelText;
    public int level;
    public string levelName;
    public string sceneName;
    private bool isUnlocked = false;

    void OnEnable()
    {
        // Register with the plus and minus buttons
        playButton.onClick.AddListener(OnPlayButtonClicked);
        unlockButton.onClick.AddListener(OnUnlockButtonClicked);
        levelButton.onClick.AddListener(OnLevelButtonClicked);

        UpdateUI();
    }

    private void OnDisable()
    {
        // Unregister with the plus and minus buttons
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        unlockButton.onClick.RemoveListener(OnUnlockButtonClicked);
        levelButton.onClick.RemoveListener(OnLevelButtonClicked);
    }

    public void OnLevelButtonClicked()
    {
        if (isUnlocked)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // Load the Upgrades scene
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Upgrades"); // Load the Upgrades scene
    }

    public void OnPlayButtonClicked()
    {
        // Load the Upgrades scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void OnUnlockButtonClicked()
    {
        // Load the Upgrades scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Upgrades");
    }

    private void OnValidate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Get the unlocked level for the player from the player prefs
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        // If the player has unlocked this level, show the play button otherwise show the unlock button
        if (level <= unlockedLevels)
        {
            unlockButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            isUnlocked = true;
        }
        else
        {
            unlockButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            isUnlocked = false;
        }

        levelText.text = levelName;
        this.name = levelName + " Level";
    }
}
