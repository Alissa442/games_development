using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the script where we hook up the interconnected events for the enemies
/// </summary>

public class Enemy : MonoBehaviour
{
    private Die _die;
    private Health health;

    private GameGlobalEventsSO _gameGlobalEvents;

    private void OnEnable()
    {
        _gameGlobalEvents = GameStateSO.Instance.gameGlobalEvents;

        // Register the enemy with the game global events
        _gameGlobalEvents.onEnemySpawned.Invoke(this.gameObject);

        _die = GetComponent<Die>();
        health = GetComponent<Health>();

        health.onDeath.AddListener(_die.Invoke);
        health.onDeath.AddListener(EnemyDied);

    }


    private void OnDisable()
    {
        health.onDeath.RemoveListener(_die.Invoke);
        health.onDeath.RemoveListener(EnemyDied);
    }

    private void EnemyDied()
    {
        // Do something when the player dies
        _gameGlobalEvents.onEnemyDied.Invoke(this.gameObject);
    }

}
