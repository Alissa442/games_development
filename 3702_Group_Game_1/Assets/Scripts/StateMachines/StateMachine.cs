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
    }

    protected virtual void Update()
    {
        if (currentState == null)
        {
            _agent.SetDestination(transform.position); // Stop moving if there is no target
            currentState = defaultState;
        }

        currentState.Tick();
    }
}
