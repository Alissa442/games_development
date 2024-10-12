using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the commonly used Game States.
/// This is a Scriptable Object so that it can be accessed from anywhere
/// Like a Singleton but better
/// </summary>
[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/Game State")]
public class GameStateSO : ScriptableObject
{

    public GameGlobalEventsSO gameGlobalEvents;

    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> players = new List<GameObject>();

    public List<GameObject> allConsumables = new List<GameObject>();
    public List<GameObject> food = new List<GameObject>();
    public List<GameObject> speedBoosts = new List<GameObject>();
    public List<GameObject> rangeBoosts = new List<GameObject>();
    public List<GameObject> poisons = new List<GameObject>();

    public List<GameObject> ingredients = new List<GameObject>();
    public List<GameObject> freezes = new List<GameObject>();
    public List<GameObject> gems = new List<GameObject>();
    public List<GameObject> mines = new List<GameObject>();

    // Lazy Loading Singleton pattern inside a SCRIPTABLE OBJECT!
    private static GameStateSO instance;
    public static GameStateSO Instance
    {
        get
        {
            // Lazy loading. If it's not already loaded.
            if (GameStateSO.instance == null)
            {
                instance = Resources.Load<GameStateSO>("GameState");
            }
            return instance;
        }
        private set { instance = value; }
    }

    private void OnEnable()
    {
        // Service locator doesn't seem to be working...
        //ServiceLocator.Instance.RegisterService("GameState", this);

        gameGlobalEvents = GameGlobalEventsSO.Instance;

        // Subscribe to the appropriate events
        gameGlobalEvents.onEnemySpawned.AddListener(OnEnemySpawned);
        gameGlobalEvents.onEnemyDied.AddListener(OnEnemyDied);
        gameGlobalEvents.onPlayerSpawned.AddListener(OnPlayerSpawned);
        gameGlobalEvents.onPlayerDied.AddListener(OnPlayerDied);

        gameGlobalEvents.onConsumableSpawned.AddListener(OnConsumableSpawned);
        gameGlobalEvents.onConsumablePickedUp.AddListener(OnConsumablePickedUp);
        gameGlobalEvents.onFoodSpawned.AddListener(OnFoodSpawned);
        gameGlobalEvents.onFoodPickedUp.AddListener(OnFoodPickedUp);
        gameGlobalEvents.onSpeedBoostSpawned.AddListener(OnSpeedBoostSpawned);
        gameGlobalEvents.onSpeedBoostPickedUp.AddListener(OnSpeedBoostPickedUp);
        gameGlobalEvents.onRangeBoostSpawned.AddListener(OnRangeBoostSpawned);
        gameGlobalEvents.onRangeBoostPickedUp.AddListener(OnRangeBoostPickedUp);
        gameGlobalEvents.onPoisonSpawned.AddListener(OnPoisonSpawned);
        gameGlobalEvents.onPoisonPickedUp.AddListener(OnPoisonPickedUp);

        gameGlobalEvents.onIngredientSpawned.AddListener(OnIngredientsSpawned);
        gameGlobalEvents.onIngredientPickedUp.AddListener(OnIngredientsPickedUp);
        gameGlobalEvents.onFreezeSpawned.AddListener(OnFreezesSpawned);
        gameGlobalEvents.onFreezePickedUp.AddListener(OnFreezesPickedUp);
        gameGlobalEvents.onMineSpawned.AddListener(OnMinesSpawned);
        gameGlobalEvents.onMinePickedUp.AddListener(OnMinesPickedUp);
        gameGlobalEvents.onGemSpawned.AddListener(OnGemsSpawned);
        gameGlobalEvents.onGemPickedUp.AddListener(OnGemsPickedUp);

    }

    private void OnDisable()
    {
        // Unregisters the service
        ServiceLocator.Instance.UnregisterService("GameState");

        // Unsubscribe from the appropriate events
        gameGlobalEvents.onEnemySpawned.RemoveListener(OnEnemySpawned);
        gameGlobalEvents.onEnemyDied.RemoveListener(OnEnemyDied);
        gameGlobalEvents.onPlayerSpawned.RemoveListener(OnPlayerSpawned);
        gameGlobalEvents.onPlayerDied.RemoveListener(OnPlayerDied);

        gameGlobalEvents.onConsumableSpawned.RemoveListener(OnConsumableSpawned);
        gameGlobalEvents.onConsumablePickedUp.RemoveListener(OnConsumablePickedUp);
        gameGlobalEvents.onFoodSpawned.RemoveListener(OnFoodSpawned);
        gameGlobalEvents.onFoodPickedUp.RemoveListener(OnFoodPickedUp);
        gameGlobalEvents.onSpeedBoostSpawned.RemoveListener(OnSpeedBoostSpawned);
        gameGlobalEvents.onSpeedBoostPickedUp.RemoveListener(OnSpeedBoostPickedUp);
        gameGlobalEvents.onRangeBoostSpawned.RemoveListener(OnRangeBoostSpawned);
        gameGlobalEvents.onRangeBoostPickedUp.RemoveListener(OnRangeBoostPickedUp);
        gameGlobalEvents.onPoisonSpawned.RemoveListener(OnPoisonSpawned);
        gameGlobalEvents.onPoisonPickedUp.RemoveListener(OnPoisonPickedUp);

        gameGlobalEvents.onIngredientSpawned.RemoveListener(OnIngredientsSpawned);
        gameGlobalEvents.onIngredientPickedUp.RemoveListener(OnIngredientsPickedUp);
        gameGlobalEvents.onFreezeSpawned.RemoveListener(OnFreezesSpawned);
        gameGlobalEvents.onFreezePickedUp.RemoveListener(OnFreezesPickedUp);
        gameGlobalEvents.onGemSpawned.RemoveListener(OnGemsSpawned);
        gameGlobalEvents.onGemPickedUp.RemoveListener(OnGemsPickedUp);
        gameGlobalEvents.onMineSpawned.RemoveListener(OnMinesSpawned);
        gameGlobalEvents.onMinePickedUp.RemoveListener(OnMinesPickedUp);
    }

    // Shortcuts for Player and NPC Listeners
    private void OnEnemySpawned(GameObject obj) { enemies.Add(obj); }
    private void OnEnemyDied(GameObject obj) { enemies.Remove(obj); }
    private void OnPlayerSpawned(GameObject obj) { players.Add(obj); }
    private void OnPlayerDied(GameObject obj) { players.Remove(obj); }

    // Consumables Listeners
    private void OnConsumableSpawned(GameObject obj) { allConsumables.Add(obj); }
    private void OnConsumablePickedUp(GameObject obj) { allConsumables.Remove(obj); }
    private void OnFoodSpawned(GameObject obj) { food.Add(obj); }
    private void OnFoodPickedUp(GameObject obj) { food.Remove(obj); }
    private void OnSpeedBoostSpawned(GameObject obj) { speedBoosts.Add(obj); }
    private void OnSpeedBoostPickedUp(GameObject obj) { speedBoosts.Remove(obj); }
    private void OnRangeBoostSpawned(GameObject obj) { rangeBoosts.Add(obj); }
    private void OnRangeBoostPickedUp(GameObject obj) { rangeBoosts.Remove(obj); }
    private void OnPoisonSpawned(GameObject obj) { poisons.Add(obj); }
    private void OnPoisonPickedUp(GameObject obj) { poisons.Remove(obj); }

    private void OnIngredientsSpawned(GameObject obj) { ingredients.Add(obj); }
    private void OnIngredientsPickedUp(GameObject obj) { ingredients.Remove(obj); }
    private void OnFreezesSpawned(GameObject obj) { freezes.Add(obj); }
    private void OnFreezesPickedUp(GameObject obj) { freezes.Remove(obj); }
    private void OnGemsSpawned(GameObject obj) { gems.Add(obj); }
    private void OnGemsPickedUp(GameObject obj) { gems.Remove(obj); }
    private void OnMinesSpawned(GameObject obj) { mines.Add(obj); }
    private void OnMinesPickedUp(GameObject obj) { mines.Remove(obj); }


    // Helper Method

    /// <summary>
    /// Find the closest item from a list of items
    /// </summary>
    /// <param name="source"></param>
    /// <param name="items">List of lists. Eg. food, speed, range</param>
    /// <returns></returns>
    public GameObject FindClosestItem(GameObject source, params List<GameObject>[] items)
    {
        GameObject closestItem = null;
        float closestDistance = Mathf.Infinity;
        Vector3 sourcePosition = source.transform.position;

        foreach (var itemList in items)
        {
            foreach (var item in itemList)
            {
                float distance = Vector3.Distance(sourcePosition, item.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        //Debug.Log("Closest Item is " + closestItem.name);
        //Debug.Log("Closest Distance is " + closestDistance);
        return closestItem;
    }
}
