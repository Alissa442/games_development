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
    public GameGlobalEventsSO gameGlobalEvents;
    public IState defaultState;

    protected virtual void OnEnable()
    {
        if (gameGlobalEvents == null)
            gameGlobalEvents = Resources.Load<GameGlobalEventsSO>("GameGlobalEvents");

        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (currentState == null)
        {
            _agent.SetDestination(transform.position); // Stop moving if there is no target
            currentState = defaultState;
        }

        currentState.Tick(this);
    }
}
