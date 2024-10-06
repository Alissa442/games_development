using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetXSpeed : WinCondition
{
    public float speedToGet = 3;
    public TextMeshProUGUI speedText;

    private GameObject player;
    private Speed playerSpeed;

    private float highestSpeed = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeed = player.GetComponent<Speed>();

        highestSpeed = playerSpeed.movementSpeed;
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerSpeed();
    }

    private void CheckPlayerSpeed()
    {
        if (playerSpeed.movementSpeed > highestSpeed)
        {
            highestSpeed = playerSpeed.movementSpeed;
            UpdateUI();
        }

        if (highestSpeed >= speedToGet)
        {
            IsWinConditionMet = true;
        }
    }

    private void UpdateUI()
    {
        // Update the UI to show the highest range and range to get
        speedText.text = $"Get Speed\n{highestSpeed} / {speedToGet}";
    }

    protected override void OnValidate()
    {
        // Call the base class's OnValidate method
        base.OnValidate();

        // Derived class validation logic
        UpdateUI();
    }

}
