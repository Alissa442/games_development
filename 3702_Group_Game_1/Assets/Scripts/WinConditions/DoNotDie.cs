using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDie : WinCondition
{
    private int playersAlive = 0;

    void Start()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playersAlive = 1;

        // Register with the player Die Script the event of their death
        player.GetComponent<Health>().onDeath.AddListener(PlayerDied);

        IsWinConditionMet = true;
    }

    public void PlayerDied()
    {
        playersAlive--;
        if (playersAlive == 0)
        {
            IsWinConditionMet = false;
            HasLost = true;
        }
    }
}
