using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthUI : MonoBehaviour
{
    private int currentHealth;
    public TextMeshProUGUI _healthText; // If using TextMeshPro
    public Vector2 offset = new Vector2(0, 2); // Adjust the offset as needed

    private Health _health;
    private Vector3 _offset;

    void Start()
    {
        if (_health == null)
            Debug.LogError("Health component not found on " + gameObject.name);

        _offset = new Vector3(offset.x, 0.5f, offset.y);
        _health = GetComponent<Health>();
        UpdateHealth();
    }

    void Update()
    {
        UpdateHealth();
        UpdateHealthTextScreenPosition();
    }

    private void UpdateHealthTextScreenPosition()
    {
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2); // Adjust the offset as needed
        Vector3 newPosition = transform.position + _offset;
        _healthText.transform.position = newPosition;
    }
    private void UpdateHealth()
    {
        _healthText.text = _health.Value.ToString();
    }
}
