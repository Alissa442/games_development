using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(LineRenderer))]
public class Range : MonoBehaviour
{
    public float range = 2f;
    const float MINIMUM_RANGE = 1f;
    SphereCollider _sphereCollider;
    LineRenderer _lineRenderer;

    public void IncreaseRange(float rangeBoost, float lengthOfTime)
    {
        //Debug.Log("Increasing Range");
        range += rangeBoost;
        _sphereCollider.radius = range;
        UpdateRangeVisual();

        StartCoroutine(RangeBoost(rangeBoost, lengthOfTime));
    }

    public void DecreaseRange(float rangeBoost, float lengthOfTime)
    {
        float changeOfRange = Mathf.Clamp(rangeBoost, 0, range - MINIMUM_RANGE);
        range -= changeOfRange;
        _sphereCollider.radius = range;
        UpdateRangeVisual();

        StartCoroutine(RangeBoost(-changeOfRange, lengthOfTime));
    }

    private IEnumerator RangeBoost(float rangeBoost, float lengthOfTime)
    {
        yield return new WaitForSeconds(lengthOfTime);

        range -= rangeBoost;
        _sphereCollider.radius = range;
        UpdateRangeVisual();
    }

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        if (_sphereCollider == null)
        {
            _sphereCollider = gameObject.AddComponent<SphereCollider>();
        }
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = range;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = 50; // Number of segments in the circle
        UpdateRangeVisual();
    }

    private void UpdateRangeVisual()
    {
        int segments = 20; // Increase the number of segments for a smoother circle
        _lineRenderer.positionCount = segments + 1; // +1 to close the loop

        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * range;
            float z = Mathf.Sin(angle) * range;
            _lineRenderer.SetPosition(i, new Vector3(x, 0.25f, z));
            angle += 2 * Mathf.PI / segments;
        }

        //float angle = 0f;
        //for (int i = 0; i < _lineRenderer.positionCount; i++)
        //{
        //    float x = Mathf.Cos(angle) * range;
        //    float z = Mathf.Sin(angle) * range;
        //    _lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        //    angle += 2 * Mathf.PI / _lineRenderer.positionCount;
        //}
    }

    // Draw the range as a ring in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
