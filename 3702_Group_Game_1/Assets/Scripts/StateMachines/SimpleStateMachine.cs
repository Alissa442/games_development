using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleStateMachine : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Transform target;

    public GameGlobalEventsSO gameGlobalEvents;

    private void Awake()
    {
        if (gameGlobalEvents == null)
            gameGlobalEvents = Resources.Load<GameGlobalEventsSO>("GameGlobalEvents");

        _agent = GetComponent<NavMeshAgent>();

        // Register the event to listen for new food
        gameGlobalEvents.onFoodSpawned.AddListener(NewFood);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void NewFood(GameObject food)
    {
        // Oooh new food.. lets chase.
        target = food.transform;
        _agent.SetDestination(target.position);
    }

    private void OnDestroy()
    {
        // Deregister the event to listen for new food
        gameGlobalEvents.onFoodSpawned.RemoveListener(NewFood);
    }
}
