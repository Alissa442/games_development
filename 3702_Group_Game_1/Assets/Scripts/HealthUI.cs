using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthUI : MonoBehaviour
{
    [Tooltip("The GamePrefabsSO to use for this game.")]
    public GamePrefabsSO gamePrefabs;
    [Tooltip("The offset of the health text from the object it's attached to")]
    public Vector2 offset = new Vector2(0, 2);

    private Health _health;                 // The health component of the object
    private Vector3 _offset;                // Calculated offset based on the inspector value
    private GameObject _worldSpaceCanvas;   // The canvas to attach the health text to
    private TextMeshProUGUI _healthText;    // The created HealthText

    private void Awake()
    {
        // Fetch the GamePrefabs from the Resources folder if it hasn't been hooked up
        if (gamePrefabs == null)
            gamePrefabs = Resources.Load<GamePrefabsSO>("GamePrefabs");
        _worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas");

        // If the world space canvas doesn't exist, create it
        if (_worldSpaceCanvas == null)
        {
            _worldSpaceCanvas = Instantiate(gamePrefabs.worldSpaceCanvas);
            _worldSpaceCanvas.name = "WorldSpaceCanvas";
        }

        // Create the health text object
        _healthText = Instantiate(gamePrefabs.healthUIPrefab, _worldSpaceCanvas.transform).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        // If the health text doesn't exist, something really went wrong.
        if (_healthText == null)
            Debug.LogError("HealthText TextMeshPro component not found for " + gameObject.name);

        // Set the offset based on the inspector value
        _offset = new Vector3(offset.x, 0.5f, offset.y);

        // If the health component hasn't been hooked up, get it
        if (_health == null)
            _health = GetComponent<Health>();

        // Gets the initial health value and sets it to the health text
        UpdateHealth(_health.Value);

        // Registers a listener to update the health text when the health changes
        _health.onHealthChange.AddListener(UpdateHealth);
    }

    void Update()
    {
        UpdateHealthTextScreenPosition();
    }

    /// <summary>
    /// Moves the health text to the position of the object it's attached to
    /// </summary>
    private void UpdateHealthTextScreenPosition()
    {
        Vector3 newPosition = transform.position + _offset;
        _healthText.transform.position = newPosition;
    }

    /// <summary>
    /// Updates the health text to reflect the current health value
    /// This is called by an UnityEvent listener on the health component
    /// </summary>
    private void UpdateHealth(float value)
    {
        // Convert to an int for display
        int currentHealth = (int)value;
        // Update the health text
        _healthText.text = currentHealth.ToString();
    }

    private void OnDestroy()
    {
        // Unregisters the listener when the object is destroyed
        _health.onHealthChange.RemoveListener(UpdateHealth);

        // Cleanup the when the object is destroyed
        if (_healthText != null)
            Destroy(_healthText.gameObject);
    }
}
