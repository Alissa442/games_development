using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _initialHealth = 100;
    [Tooltip("How much extra healing will healing effects give. Negatives can have food deal you damage.")]
    public float healingBonus = 0f;
    [Tooltip("How much less damage will poison effects deal. Too much can have you heal when eating poison.")]
    public float poisonBonus = 0f;

    [SerializeField]
    private float _currentHealth;

	public FloatEvent onHealthChange;
    public UnityEvent onDeath;

    public int Value
	{
		get { return (int) _currentHealth; }
		set { 
            if (value == _currentHealth) return;
            _currentHealth = value;

            onHealthChange.Invoke(_currentHealth);
            if (_currentHealth <= 0)
            {
                onDeath.Invoke();
            }
        }
    }


    private void Awake()
    {
        SetHealth(_initialHealth);
    }

    public void SetHealth(float value)
    {
        _currentHealth = value;
        onHealthChange.Invoke(_currentHealth);
    }

    // Used to poison the character
    public void TakeDamage(float damage)
    {
        float takingDamage = damage - poisonBonus;
        Debug.Log(damage + " - " + poisonBonus + " = " + takingDamage);
        _currentHealth = _currentHealth - takingDamage;
        onHealthChange.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    // Used by the ticker to damage the character each second
    // Also intended to be used by boss attacks
    public void TakeUnavoidableDamage( float damage )
    {
        _currentHealth = _currentHealth - damage;
        onHealthChange.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    // Used to heal the character (food)
    public void Heal(float healAmount)
    {
        _currentHealth = _currentHealth + healAmount + healingBonus;
        onHealthChange.Invoke(_currentHealth);
    }

}
