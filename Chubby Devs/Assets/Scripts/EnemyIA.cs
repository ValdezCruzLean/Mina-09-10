using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyIA : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private Transform lampTransform;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lampTransform = FindAnyObjectByType<Lamp>().transform;
        if (LightZoneProtection.Instance != null)
        {
            lampTransform = LightZoneProtection.Instance.transform;
        }
    }
    void Update()
    {
        /*Si no se encontraron los componentes entonces que se corte la funcion con el return*/
        if (lampTransform == null || LightZoneProtection.Instance == null)
        {
            return;
        }
        EnemyGoToPlayer();
    }
    public void EnemyGoToPlayer()
    {
        float distance = Vector3.Distance(transform.position, lampTransform.position);
        float stopRadius = LightZoneProtection.Instance.radiusSphere;
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