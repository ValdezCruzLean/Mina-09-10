using UnityEngine;
using UnityEngine.Rendering;

public class LightZoneProtectionZone : MonoBehaviour
{
    [SerializeField] float radiusSphere = 0;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, radiusSphere);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusSphere);
    }
}