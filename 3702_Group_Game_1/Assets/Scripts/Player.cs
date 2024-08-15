using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Die))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthTicker))]
/// <summary>
/// This is the script where we hook up the interconnected events for the player
/// </summary>
public class Player : MonoBehaviour
{
    private Die _die;
    private Health health;

    private GameGlobalEventsSO _gameGlobalEvents;

    private void OnEnable()
    {
        _gameGlobalEvents = GameStateSO.Instance.gameGlobalEvents;

        // Register the player with the game global events
        _gameGlobalEvents.onPlayerSpawned.Invoke(this.gameObject);

        _die = GetComponent<Die>();
        health = GetComponent<Health>();

        // Connect the health onDeath event to the die script
        health.onDeath.AddListener(_die.Invoke);
        health.onDeath.AddListener(PlayerDied);
    }

    private void OnDisable()
    {
        // Remove the appropriate listners
        health.onDeath.RemoveListener(_die.Invoke);
        health.onDeath.RemoveListener(PlayerDied);
    }

    private void PlayerDied()
    {
        // Do something when the player dies
        _gameGlobalEvents.onPlayerDied.Invoke(this.gameObject);
    }
}
