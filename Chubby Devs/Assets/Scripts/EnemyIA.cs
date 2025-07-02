//using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class EnemyIA : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private Transform lampTransform;

    public Animator animador; // Asignar en el Inspector
    public string triggerAnimacion = "Aparecer"; // Nombre del Trigger en el Animator
    public float tiempoAntesDeDerrota = 2f; // Tiempo para esperar antes de cambiar de escena
    public string escenaDerrota = "Derrota"; // Nombre de la escena de derrota
    //private Animador animador:
    private bool yaActivo = false;

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

    private void OnTriggerEnter(Collider other) // Si es 2D: OnTriggerEnter2D
    {
        if (!yaActivo && other.CompareTag("Player"))
        {
            yaActivo = true;

            navMeshAgent.isStopped = true;

            if (animador != null)
            {
                animador.SetTrigger(triggerAnimacion);
            }

            StartCoroutine(PerderTrasAnimacion());
        }
    }

    public System.Collections.IEnumerator PerderTrasAnimacion()
    {
        yield return new WaitForSeconds(tiempoAntesDeDerrota);
        SceneManager.LoadScene(escenaDerrota);
    }
}
