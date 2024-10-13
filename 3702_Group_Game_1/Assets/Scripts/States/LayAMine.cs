using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayAMine : IState
{
    private StateMachine _stateMachine; // The state machine that this state is attached to

    public IState onNeedFood;           // What state to change to when found is needed
    private int healthThreshold = 15;   // The health threshold to switch to chase food
    private float mineLayingTime = 3.0f; // The time to lay a mine

    private Vector3 minePosition;       // The position to lay the mine
    private bool isMovingToMine = false; // If the player is moving to lay the mine
    private bool isLayingMine = false;  // If the player is laying the mine
    private GameObject[] minePrefabs;   // The mines that the LayAMine can lay
    private float mineLayingTimer = 0.0f; // Set to when the mine will be laid

    private Range range; // The range component of the player/enemy

    public LayAMine(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;

        range = _stateMachine.GetComponent<Range>();
    }

    public void SetTransitions(params IState[] transitionsTo)
    {
        onNeedFood = transitionsTo[0];
    }

    public void SetHealthThreshold(int healthThreshold)
    {
        this.healthThreshold = healthThreshold;
    }

    public void SetMineLayingTime(float mineLayingTime)
    {
        this.mineLayingTime = mineLayingTime;
    }

    public void SetMinePrefabs(GameObject[] minePrefabs)
    {
        this.minePrefabs = minePrefabs;
    }

    public void Tick()
    {
        //Debug.Log("LayAMine Tick");
        // If minePrefabs is null or is empty, no point in laying mines
        if (minePrefabs == null || minePrefabs.Length == 0)
        {
            Debug.LogError("No mine prefabs set in LayAMine. Can only hunt food");
            _stateMachine.ChangeState(onNeedFood);
            return;
        }

        // Regardless of what's happening and their
        // health is low, go find food.
        if (_stateMachine.health <= healthThreshold)
        {
            isLayingMine = false;
            isMovingToMine = false;
            _stateMachine._agent.stoppingDistance = 0f;
            _stateMachine.ChangeState(onNeedFood);
            return;
        }

        if (!isLayingMine && !isMovingToMine)
        {
            // If not laying a mine and not moving to lay a mine
            // then start the process
            SetMinePosition();
            return;
        }

        if (isMovingToMine)
        {
            MoveTowardsMineLayingPosition();
            return;
        }

        if (isLayingMine)
        {
            if (Time.time >= mineLayingTimer)
            {
                // Is it time to lay the mine
                isLayingMine = false;

                // Choose a random mine from minePrefabs
                int randomMineIndex = Random.Range(0, minePrefabs.Length);
                GameObject minePrefab = minePrefabs[randomMineIndex];

                // Spawn the mine
                if (minePrefab != null)
                {
                    // Spawn the mine
                    GameObject mine = GameObject.Instantiate(minePrefab, minePosition, Quaternion.identity);
                    Debug.Log("Mine laid");
                }
            }
            else
            {
                // Play laying mine animation
            }
        }
    }

    private void SetMinePosition()
    {
        // Get a new mine position from a spawner
        // Not the best solution, but it works for now
        Spawner spawner = GameObject.FindObjectOfType<Spawner>(); // Find the first spawner
        minePosition = spawner.GetRandomSpawnPosition();

        isMovingToMine = true;
        //_stateMachine._agent.SetDestination(minePosition);
        Debug.Log("Moving to lay a mine");
    }

    private void MoveTowardsMineLayingPosition()
    {
        float distanceToLay = (range.range * 0.5f) + 2.5f;

        _stateMachine._agent.SetDestination(minePosition);
        _stateMachine._agent.stoppingDistance = distanceToLay;

        // Lay the mine a distance away from the MineLayer to make sure its out of its own range
        if (Vector3.Distance(_stateMachine.transform.position, minePosition) < distanceToLay)
        {
            isMovingToMine = false;
            isLayingMine = true;
            mineLayingTimer = Time.time + mineLayingTime;
            Debug.Log("Starting to Lay a mine");
        }
    }

}
