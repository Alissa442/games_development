using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameGlobalEvents", menuName = "ScriptableObjects/Game Global Events List")]
public class GameGlobalEventsSO : ScriptableObject
{
    // Player Events
    public UnityEvent<GameObject> onPlayerSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onPlayerDied = new UnityEvent<GameObject>();

    // Enemy Events
    public UnityEvent<GameObject> onEnemySpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onEnemyDied = new UnityEvent<GameObject>();

    // All consumable events
    public UnityEvent<GameObject> onConsumableSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onConsumablePickedUp = new UnityEvent<GameObject>();
    // Which was picked up and by whom
    public UnityEvent<GameObject, GameObject> onConsumablePickedUpByWhom = new UnityEvent<GameObject, GameObject>(); // Who picked up the consumable

    // Specific consumable events
    public UnityEvent<GameObject> onFoodSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onFoodPickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onPoisonSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onPoisonPickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onRangeBoostSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onRangeBoostPickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onSpeedBoostSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onSpeedBoostPickedUp = new UnityEvent<GameObject>();

    public UnityEvent<GameObject> onGemSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onGemPickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onFreezeSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onFreezePickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onMineSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onMinePickedUp = new UnityEvent<GameObject>();

    public UnityEvent<GameObject> onIngredientSpawned = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onIngredientPickedUp = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> onIngredientDelivered = new UnityEvent<GameObject>();

    // Lazy Loading Singleton pattern inside a SCRIPTABLE OBJECT!
    private static GameGlobalEventsSO instance;
    public static GameGlobalEventsSO Instance
    {
        get
        {
            // Lazy loading. If it's not
            if (GameGlobalEventsSO.instance == null)
            {
                instance = Resources.Load<GameGlobalEventsSO>("GameGlobalEvents");
            }
            return instance;
        }
        private set { instance = value; }
    }

}
