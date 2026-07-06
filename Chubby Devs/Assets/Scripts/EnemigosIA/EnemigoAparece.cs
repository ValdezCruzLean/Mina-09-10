using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemigoAparece : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAparicion = 10f;
    public float tiempoEntreApariciones = 15f; 

    [Header("Mecánica de Lámpara")]
    public Lamp lamparaJugador; 
    public float distanciaApagarLuz = 3.5f;
    private bool yaSaboteoEnEstaAparicion = false;
    
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

            ChequearDistanciaLampara();
        }
    }

    void ChequearDistanciaLampara()
    {
        if (lamparaJugador == null || yaSaboteoEnEstaAparicion) return;

        float distanciaActual = Vector3.Distance(transform.position, jugador.position);

        if (distanciaActual <= distanciaApagarLuz)
        {
            if (lamparaJugador.lamparaEncendida)
            {
                yaSaboteoEnEstaAparicion = true;

                lamparaJugador.Invoke("ApagarLuz", 0f); 

                if (lamparaJugador.canvasFosforos != null)
                {
                    lamparaJugador.canvasFosforos.RestarFosforo();
                }

                Debug.Log("¡La bruja sopló tu lámpara y perdiste un fósforo/aceite por proximidad!");
            }
        }
    }

    IEnumerator CicloApariciones()
    {
        while (true)
        {
            TeletransportarCercaDelJugador();

            yaSaboteoEnEstaAparicion = false;

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
