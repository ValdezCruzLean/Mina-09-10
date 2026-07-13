using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemigoAparece : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAparicion = 10f;
    public float tiempoEntreApariciones = 15f; 

    /*[Header("Mecánica de Lámpara")]
    public Lamp lamparaJugador; 
    public float distanciaApagarLuz = 3.5f;*/

    [Header("Mecánica de Ataques por Cercanía")]
    public float distanciaEfecto = 3.5f;
    public Lamp lamparaJugador;
    public EfectoVisionJugador efectoVision;
    public EfectoConfusionCamara confusionCamara;
    //public EfectoControlesInvertidos efectoControles;
    //private bool yaSaboteoEnEstaAparicion = false;
    private int ataqueElegido; 
    private bool yaAtacoEnEstaAparicion = false;
    
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

            //ChequearDistanciaLampara();
            ChequearDistanciaAtaque();
        }
    }

    /*void ChequearDistanciaLampara()
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
    }*/

    void ChequearDistanciaAtaque()
    {
        if (yaAtacoEnEstaAparicion) return;

        float distanciaActual = Vector3.Distance(transform.position, jugador.position);

        if (distanciaActual <= distanciaEfecto)
        {
            yaAtacoEnEstaAparicion = true;

            switch (ataqueElegido)
            {
                case 0:
                    AtaqueApagarLampara();
                    break;
                case 1:
                    AtaqueAfectarVision();
                    break;
                case 2:
                    AtaqueInvertirControles();
                    break;
            }
        }
    }

    void AtaqueApagarLampara()
    {
        if (lamparaJugador != null && lamparaJugador.lamparaEncendida)
        {
            lamparaJugador.Invoke("ApagarLuz", 0f); 

            if (lamparaJugador.canvasFosforos != null)
            {
                lamparaJugador.canvasFosforos.RestarFosforo();
            }
            
            if (TimeLight.Instance != null)
            {
                TimeLight.Instance.VaciarTemporizador();
            }
            Debug.Log("🎲 [Probabilidad] La bruja eligió: ¡Apagar Lámpara!");
        }
    }

    void AtaqueAfectarVision()
    {
        Debug.Log("🎲 [Probabilidad] La bruja eligió: ¡Cegar al jugador!");
        if (efectoVision != null)
        {
            efectoVision.IniciarCeguera();
        }
    }

    //Afecta la camara para dar la impresion de una confusion o mareo
    void AtaqueInvertirControles()
    {
        Debug.Log("🎲 [Probabilidad] La bruja eligió: ¡Invertir controles!");

        /*if (efectoControles != null)
        {
            efectoControles.ActivarInversion(4f); 
        }*/
        if (confusionCamara != null)
        {
            // Duración: 3.5 segundos | Intensidad: 0.15f (puedes subirlo si quieres que tiemble más)
            confusionCamara.ActivarSacudida(3.5f, 0.15f); 
        }

    }

    IEnumerator CicloApariciones()
    {
        while (true)
        {
            TeletransportarCercaDelJugador();

            //yaSaboteoEnEstaAparicion = false;
            ataqueElegido = Random.Range(0, 3); 
            yaAtacoEnEstaAparicion = false;

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
