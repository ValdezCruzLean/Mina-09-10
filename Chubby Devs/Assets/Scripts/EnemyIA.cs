using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyIA : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private Transform lampTransform;
    private LightZoneProtectionZone lightZoneProtectionZone;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lightZoneProtectionZone = FindAnyObjectByType<LightZoneProtectionZone>();
        if (lightZoneProtectionZone != null)
        {
            lampTransform = lightZoneProtectionZone.transform;
        }
    }
    void Update()
    {
        /*Si no se encontraron los componentes entonces que se corte la funcion con el return*/
        if (lampTransform == null || lightZoneProtectionZone == null)
        {
            return;
        }
        EnemyGoToPlayer();
    }
    public void EnemyGoToPlayer()
    {
        float distance = Vector3.Distance(transform.position, lampTransform.position);
        float stopRadius = lightZoneProtectionZone.RadiusSphere;
        if (distance > stopRadius)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(lampTransform.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }
}