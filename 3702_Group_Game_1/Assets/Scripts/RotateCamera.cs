using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 4.5f;

    void Update()
    {
        // Rotate the camera around the Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
