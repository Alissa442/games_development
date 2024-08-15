using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Speed : MonoBehaviour
{
    public float movementSpeed = 5f;
    const float MINIMUM_SPEED = 1f;
    private NavMeshAgent _agent;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetAgentSpeed();
    }

    public void IncreaseSpeed(float speedBoost, float lengthOfTime)
    {
        movementSpeed += speedBoost;
        SetAgentSpeed();

        StartCoroutine(SpeedBoost(speedBoost, lengthOfTime));
    }

    public void DecreaseSpeed(float speedBoost, float lengthOfTime)
    {
        float changeOfSpeed = Mathf.Clamp(speedBoost, 0 , movementSpeed - MINIMUM_SPEED);
        movementSpeed -= changeOfSpeed;
        SetAgentSpeed();

        StartCoroutine(SpeedBoost(-changeOfSpeed, lengthOfTime));
    }

    private IEnumerator SpeedBoost(float speedBoost, float lengthOfTime)
    {
        yield return new WaitForSeconds(lengthOfTime);

        movementSpeed -= speedBoost;
        SetAgentSpeed();
    }

    private void SetAgentSpeed()
    {
        if (_agent != null)
            _agent.speed = movementSpeed;
    }
}
