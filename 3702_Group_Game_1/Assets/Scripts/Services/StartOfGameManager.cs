using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartOfGameManager : MonoBehaviour
{
    public string levelName = "Level 1";
    [TextArea(3, 10)]
    public string tooltipText = "Ok fattie, I know you're new to this so listen up.\r\nEach round you have objectives you can see them on the right.\r\nThis round, you gotta eat 2 foods.\r\nYou think you can handle that?.";
    public int timeToHideTooltip = 4;
    public int timeToWait = 6;

    public GameObject tooltipPanel;
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownTextUI;
    public TextMeshProUGUI tooltipTextUI;
    public TextMeshProUGUI tooltipLabelUI;

    public GameObject[] enemies;
    public GameObject player;

    [SerializeField]
    private float timeLeft;

    void Start()
    {
        timeLeft = timeToWait;

        // Find all the enemies and the player
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");

        // Stop everyone from moving and stop their health ticking down
        player.GetComponent<Speed>().setCannotMove();
        player.GetComponent<HealthTicker>().isCurrentlyTicking = false;

        // Let the enemies Move and stop their health ticking down
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Speed>().setCannotMove();
            enemy.GetComponent<HealthTicker>().isCurrentlyTicking = false;
        }

        // Set the text of the tooltips
        tooltipTextUI.text = tooltipText;
        tooltipLabelUI.text = levelName;
        setCountdownText();

        // Make sure we show the tooltip panels
        tooltipPanel.SetActive(true);
        countdownPanel.SetActive(true);

        // Start the coroutines
        StartCoroutine(StartGame());
        StartCoroutine(HideTooltip());

    }

    private IEnumerator HideTooltip()
    {
        yield return new WaitForSeconds(timeToHideTooltip);

        // Hide the tooltip panels
        tooltipPanel.SetActive(false);
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(timeToWait);
        // Hide the countdown Panel
        countdownPanel.SetActive(false);

        // Let the player move and start ticking down their life
        if (player != null)
        {
            player.GetComponent<Speed>().setCanMove();
            player.GetComponent<HealthTicker>().isCurrentlyTicking = true;
        }

        // Let the enemies Move and start ticking down their life
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Speed>().setCanMove();
                enemy.GetComponent<HealthTicker>().isCurrentlyTicking = true;
            }
        }
    }

    private void setCountdownText()
    {
        countdownTextUI.text = $"Game Starts in {Mathf.CeilToInt(timeLeft)} seconds";
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        setCountdownText();
    }

}
