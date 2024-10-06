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
    [SerializeField]
    float timeOfLastTick = 0;
    [SerializeField]
    float timeOfNextTick = 0;

    void Start()
    {
        _health = GetComponent<Health>();
        timeOfLastTick = Time.time;
        timeOfNextTick = Time.time + _timeBetweenTicks;
    }

    void Update()
    {
        if (Time.time >= timeOfNextTick)
        {
            _health.ApplyHealthTickerDamage(_damagePerTick);
            timeOfNextTick = timeOfNextTick + _timeBetweenTicks;
            timeOfLastTick = Time.time;
        }
        
    }
}
