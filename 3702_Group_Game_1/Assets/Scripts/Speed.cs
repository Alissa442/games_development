using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float movementSpeed = 5f;
    const float MINIMUM_SPEED = 1f;
    public void IncreaseSpeed(float speedBoost, float lengthOfTime)
    {
        movementSpeed += speedBoost;

        StartCoroutine(SpeedBoost(speedBoost, lengthOfTime));
    }

    public void DecreaseSpeed(float speedBoost, float lengthOfTime)
    {
        float changeOfSpeed = Mathf.Clamp(speedBoost, 0 , movementSpeed - MINIMUM_SPEED);
        movementSpeed -= changeOfSpeed;

        StartCoroutine(SpeedBoost(-changeOfSpeed, lengthOfTime));
    }

    private IEnumerator SpeedBoost(float speedBoost, float lengthOfTime)
    {
        yield return new WaitForSeconds(lengthOfTime);

        movementSpeed -= speedBoost;
    }

}
