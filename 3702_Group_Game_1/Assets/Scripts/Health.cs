using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _initialHealth = 100;
    [SerializeField]
    private float _currentHealth;

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

	public FloatEvent onHealthChange;
    public UnityEvent onDeath;

    private void Start()
    {
        _currentHealth = _initialHealth;
    }

    //public void TakeDamage(int damage)
    //{
    //    _currentHealth -= damage;
    //    onHealthChange.Invoke(_currentHealth);
    //    if (_currentHealth <= 0)
    //    {
    //        onDeath.Invoke();
    //    }
    //}

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        onHealthChange.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    //public void Heal(int healAmount)
    //{
    //    _currentHealth += healAmount;
    //    onHealthChange.Invoke(_currentHealth);
    //}

    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        onHealthChange.Invoke(_currentHealth);
    }

}
