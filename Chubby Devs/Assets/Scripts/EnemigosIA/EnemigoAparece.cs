using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemigoAparece : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAparicion = 10f;
    public float tiempoEntreApariciones = 15f; 
    
    private NavMeshAgent agente;
    private Renderer[] renderers;
    //private bool estaAcechando = true;
    private bool estaAcechando = false;

    private JumpTrigger miJumpscare;

    private bool iaActivada = false;
    private Collider miCollider;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        renderers = GetComponentsInChildren<Renderer>();   

        miJumpscare = GetComponent<JumpTrigger>();
        miCollider = GetComponent<Collider>();

        //StartCoroutine(CicloApariciones());
        CambiarVisibilidad(false);
        estaAcechando = false;
    }

    public void ActivarIA()
    {
        if (!iaActivada)
        {
            iaActivada = true;
            StartCoroutine(CicloApariciones());
        }
    }

    void Update()
    {
        /*if (estaAcechando && agente.enabled && agente.isOnNavMesh && jugador != null)
        {
            agente.SetDestination(jugador.position);
        }*/
        if (iaActivada && estaAcechando && agente.enabled && agente.isOnNavMesh && jugador != null)
        {
            agente.SetDestination(jugador.position);
        }
    }

    IEnumerator CicloApariciones()
    {
        while (true)
        {
            TeletransportarCercaDelJugador();

            CambiarVisibilidad(true);
            estaAcechando = true;
            
            yield return new WaitForSeconds(12f);

            CambiarVisibilidad(false);
            estaAcechando = false;
            
            yield return new WaitForSeconds(tiempoEntreApariciones);
        }
    }

    void TeletransportarCercaDelJugador()
    {
        if (jugador == null) return;

        Vector2 circuloAleatorio = Random.insideUnitCircle.normalized * distanciaAparicion;
        Vector3 posicionObjetivo = new Vector3(jugador.position.x + circuloAleatorio.x, jugador.position.y, jugador.position.z + circuloAleatorio.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(posicionObjetivo, out hit, 5f, NavMesh.AllAreas))
        {
            agente.enabled = true; 
            agente.Warp(hit.position);
            agente.enabled = false; 
        }
    }

    void CambiarVisibilidad(bool visible)
    {
        foreach (var r in renderers)
        {
            if (r != null && !(r is ParticleSystemRenderer)) 
                r.enabled = visible;
        }
        
        agente.enabled = visible;

        if (miCollider != null)
        {
            miCollider.enabled = visible;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Player") || collision.transform == jugador)
        {
            EjecutarSusto();
        }*/
        if (iaActivada && (collision.gameObject.CompareTag("Player") || collision.transform == jugador))
        {
            EjecutarSusto();
        }
    }

    void EjecutarSusto()
    {
        if (miJumpscare != null && estaAcechando)
        {
            estaAcechando = false;
            agente.enabled = false;

            if (miCollider != null) miCollider.enabled = false;
            
            miJumpscare.ActivarJumpscareManualmente();
        }
    }
}
