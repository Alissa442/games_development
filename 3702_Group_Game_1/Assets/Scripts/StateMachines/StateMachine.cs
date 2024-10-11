using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class StateMachine : MonoBehaviour
{
    public NavMeshAgent _agent;
    public GameObject target;
    public IState currentState;
    public IState defaultState;
    [ReadOnly]
    public string currentStateName;

    public GameObject whoPickedUpLastConsumable;
    public float health;

    // Services for the State Machine
    //public ServiceLocator serviceLocator;
    public GameGlobalEventsSO gameGlobalEvents;
    public GameStateSO gameState;

    protected virtual void OnEnable()
    {
        //serviceLocator = ServiceLocator.Instance;
        //gameState = ServiceLocator.Instance.GetService<GameStateSO>("GameState");

        //if (gameGlobalEvents == null)
        //    gameGlobalEvents = Resources.Load<GameGlobalEventsSO>("GameGlobalEvents");
        //if (gameState == null)
        //    gameState = Resources.Load<GameStateSO>("GameState");

        _agent = GetComponent<NavMeshAgent>();

        // Kind of cheating. Turned it into a lazy loading singleton for easy access
        gameState = GameStateSO.Instance;
        gameGlobalEvents = GameGlobalEventsSO.Instance;

        gameGlobalEvents = GameGlobalEventsSO.Instance;
        gameGlobalEvents.onConsumablePickedUpByWhom.AddListener(OnComsumablePickedUp);
        GetComponent<Health>()?.onHealthChange.AddListener(UpdateHealth);

    }

    public void ChangeState(IState newState)
    {
        currentState = newState;
        currentStateName = newState.GetType().Name;
    }

    protected virtual void Update()
    {
        if (currentState == null)
        {
            _agent.SetDestination(transform.position); // Stop moving if there is no target
            ChangeState(defaultState);
        }

        currentState.Tick();
    }

    private void OnDestroy()
    {
        // Be a good citizen and clean up after yourself
        // Removing event listeners
        gameGlobalEvents.onConsumablePickedUpByWhom.RemoveListener(OnComsumablePickedUp);
        GetComponent<Health>().onHealthChange.RemoveListener(UpdateHealth);
    }

    /// <summary>
    /// Records who picked up the last consumable
    /// This way, we can work out if wingebaby
    /// should throw a tantrum or not
    /// </summary>
    /// <param name="obj"></param>
    private void OnComsumablePickedUp(GameObject consumable, GameObject who)
    {
        whoPickedUpLastConsumable = who;
    }

    /// <summary>
    /// Updates the health of the wingebaby
    /// </summary>
    /// <param name="newHealth"></param>
    private void UpdateHealth(float newHealth)
    {
        health = newHealth;
    }

}
