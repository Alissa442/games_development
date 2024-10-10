using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthTicker : MonoBehaviour
{
    [SerializeField]
    float _timeBetweenTicks = .5f;
    [SerializeField]
    float _damagePerTick = .5f;

    [SerializeField] 
    Health _health;

    private float timeLeftInTick = 0;

    [Tooltip("Whether we're ticking down life currently. Used at the start of the game or for invulnerability Vampires maybe?")]
    public bool isCurrentlyTicking = true;

    void Start()
    {
        _health = GetComponent<Health>();
        timeLeftInTick = _timeBetweenTicks;
    }

    void Update()
    {
        // If we are not ticking, we don't need to do anything
        if (!isCurrentlyTicking) return;

        timeLeftInTick -= Time.deltaTime;

        if (timeLeftInTick <= 0)
        {
            _health.TakeUnavoidableDamage(_damagePerTick);
            timeLeftInTick += _timeBetweenTicks;
        }
        
    }
}
