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

    private void Start()
    {
        // Apply the research for the players abilities

        // Apply FasterShoes research from the playerprefs
        int fasterShoesLevels = PlayerPrefs.GetInt("FasterShoes", 0);
        Speed speed = GetComponent<Speed>();
        speed.IncreaseSpeedPermanently(fasterShoesLevels * 0.1f);

        // Apply RubberArms research from the playerprefs
        int rubberArmsLevels = PlayerPrefs.GetInt("RubberArms", 0);
        Range range = GetComponent<Range>();
        range.IncreaseRangePermanently(rubberArmsLevels * 0.1f);

        // Apply StartingHealth research from the playerprefs
        int startingHealthLevels = PlayerPrefs.GetInt("StartingHealth", 0);
        health.SetHealth(health.Value + startingHealthLevels);

        // Apply BetterDigestion research from the playerprefs
        int betterDigestionLevels = PlayerPrefs.GetInt("BetterDigestion", 0);
        health.healingBonus += betterDigestionLevels;

        // Apply LongerEffects research from the playerprefs
        int longerEffectsLevels = PlayerPrefs.GetInt("LongerEffects", 0);
        speed.effectTimeBonus += longerEffectsLevels;
        range.effectTimeBonus += longerEffectsLevels;

        // Apply GutHealth research from the playerprefs
        int gutHealthLevels = PlayerPrefs.GetInt("GutHealth", 0);
        health.poisonBonus += gutHealthLevels;

        // Apply DietPills research from the playerprefs
        int dietPillsLevels = PlayerPrefs.GetInt("DietPills", 0);
        speed.IncreaseSpeedPermanently(dietPillsLevels * 0.1f);
        health.SetHealth(health.Value - dietPillsLevels);

        // Apply Venomous research from the playerprefs
        int venomousLevels = PlayerPrefs.GetInt("Venomous", 0);
        // Get all GameObjects with the tag "Enemy" and make them hurt more
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            // Get the Health component of the enemy
            Health enemyHealth = enemy.GetComponent<Health>();
            // Decrease the poisonBonus of the enemy - Make them take more damage
            if (enemyHealth != null)
                enemyHealth.poisonBonus -= venomousLevels;
        }

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
