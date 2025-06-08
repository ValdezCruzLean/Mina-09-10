using System;
using UnityEngine;

public class LightZoneProtection : MonoBehaviour
{
    public float radiusSphere;
    public static LightZoneProtection Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, radiusSphere);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusSphere);
    }
}