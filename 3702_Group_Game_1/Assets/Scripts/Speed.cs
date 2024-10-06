using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Speed : MonoBehaviour
{
    [Tooltip("The speed of the agent")]
    public float movementSpeed = 5f;
    [Tooltip("How much longer than the programmed amount will effects last.")]
    public float effectTimeBonus = 0f;

    const float MINIMUM_SPEED = 1f; // The minimum speed
    private NavMeshAgent _agent;    // Navmesh agent cache

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetAgentSpeed();
    }

    // Used to increase speed permanently.
    // The intended use is for applying research upgrades
    public void IncreaseSpeedPermanently(float speedBoost)
    {
        movementSpeed += speedBoost;
        SetAgentSpeed();
    }

    // Used to increase the speed of the agent for a set amount of time
    public void IncreaseSpeed(float speedBoost, float lengthOfTime)
    {
        movementSpeed += speedBoost;
        SetAgentSpeed();

        StartCoroutine(SpeedBoost(speedBoost, lengthOfTime + effectTimeBonus));
    }

    public void DecreaseSpeed(float speedBoost, float lengthOfTime)
    {
        float changeOfSpeed = Mathf.Clamp(speedBoost, 0 , movementSpeed - MINIMUM_SPEED);
        movementSpeed -= changeOfSpeed;
        SetAgentSpeed();

        StartCoroutine(SpeedBoost(-changeOfSpeed, lengthOfTime + effectTimeBonus));
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
