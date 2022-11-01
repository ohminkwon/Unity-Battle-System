using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnLedgeDetectEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnLedgeDetectEvent?.Invoke(other.ClosestPoint(transform.position), other.transform.forward);
    }
}
