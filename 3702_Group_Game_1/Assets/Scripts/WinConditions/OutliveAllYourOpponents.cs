using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutliveAllYourOpponents : WinCondition
{
    private int enemiesAlive = 0;
    private int playersAlive = 0;

    void Start()
    {
        // Find all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesAlive = enemies.Length;

        // Register with the enemies Die Script the event of their death
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Health>().onDeath.AddListener(EnemyDied);
        }

        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playersAlive = 1;

        // Register with the player Die Script the event of their death
        player.GetComponent<Health>().onDeath.AddListener(PlayerDied);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive == 0)
        {
            IsWinConditionMet = true;
        }
    }

    public void PlayerDied()
    {
        playersAlive--;
        if (playersAlive == 0)
        {
            HasLost = true;
        }
    }

}
