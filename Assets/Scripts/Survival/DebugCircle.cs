using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCircle : MonoBehaviour
{
    public bool ShowDebugShootRadius = false;
    public float radius;
    private void OnDrawGizmos()
    {
        if (ShowDebugShootRadius)
            Gizmos.DrawWireSphere(transform.position, radius);
    }
}
