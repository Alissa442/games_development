using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IStateMachine
{
    void SetState(IState newState);
    IState GetState();
    NavMeshAgent GetAgent();
    GameObject GetTarget();
    void SetTarget(GameObject newTarget);
}
