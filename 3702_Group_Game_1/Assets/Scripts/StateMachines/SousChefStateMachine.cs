using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SousChefStateMachine : MonoBehaviour
{
    public GameObject deliveryLocation;
    public ChefStateMachine chefStateMachine;
    public IngregientsCarrier carrier;

    public Spawner foodSpawner;
    public Spawner poisonSpawner;

    public bool isChefRaging = false;

    void Start()
    {
        // Find the chef state machine
        chefStateMachine = FindAnyObjectByType<ChefStateMachine>();


    }

}
