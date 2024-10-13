using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthTicker))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody _rb;
    //public float speed = 4.0f;
    public ControllerState state = ControllerState.ManualControl;
    public float turnSpeed = 10.0f;
    public Speed _speed;

    public float clampLeft = -10f;
    public float clampRight = 10f;
    public float clampTop = 10f;
    public float clampBottom = -10f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = GetComponent<Speed>();
    }

    void Update()
    {
        // Don't move if you can't move
        if (!_speed.canMove)
            return;

        if (state == ControllerState.ManualControl)
        {
            // Top down 2d Player Controller on the X and Z axis
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Move the player
            Vector3 movement = new Vector3(horizontal, 0, vertical);
            transform.position += movement * _speed.movementSpeed * Time.deltaTime;

            // Clamp the position to clampLeft, clampRight, clampTop, clampBottom
            Vector3 newPosition = transform.position + movement * _speed.movementSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, clampLeft, clampRight);
            newPosition.z = Mathf.Clamp(newPosition.z, clampBottom, clampTop);
            transform.position = newPosition;

            // lerp to face the direction of movement
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed * Time.deltaTime);
            }
        }

    }
}

public enum ControllerState
{
    ManualControl,
    NavMeshAgent
}
