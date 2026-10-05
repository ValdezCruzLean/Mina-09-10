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
    [Header("Persecución Final")]
    public InventarioJugador inventario;
    public float velocidadPersecucionFinal = 6f;
    public float giroPersecucionFinal = 720f;

    private Coroutine cicloApariciones;
    private bool persecucionFinal = false;
    //private bool estaAcechando = true;
    private bool estaAcechando = false;

    private JumpTrigger miJumpscare;

    private bool iaActivada = false;
    private Collider miCollider;
    private AudioSource miAudioSource;

    private bool encuentroFinalIniciado = false;
    /*[Header("Mecánica de Trampa")]
    public string parametroAnimacionAtrapada = "Atrapada";
    private bool estaAtrapada = false;*/
    private bool atrapada = false;
    public float distanciaTrampa = 1.5f;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        renderers = GetComponentsInChildren<Renderer>();   

        miJumpscare = GetComponent<JumpTrigger>();
        miCollider = GetComponent<Collider>();

        miAudioSource = GetComponent<AudioSource>();

        //StartCoroutine(CicloApariciones());
        CambiarVisibilidad(false);
        estaAcechando = false;
    }

    public void ActivarIA()
    {
        if (!iaActivada)
        {
            iaActivada = true;
            //StartCoroutine(CicloApariciones());
            cicloApariciones = StartCoroutine(CicloApariciones());
        }
    }
    public void IniciarPersecucionFinal()
    {
        if (persecucionFinal)
        {
            return;
        }

        persecucionFinal = true;

        if (cicloApariciones != null)
        {
            StopCoroutine(cicloApariciones);
        }

        CambiarVisibilidad(true);

        estaAcechando = true;

        agente.speed = velocidadPersecucionFinal;
        agente.angularSpeed = giroPersecucionFinal;
        agente.acceleration = 30f;
        agente.isStopped = false;

        if (miAudioSource != null)
        {
            miAudioSource.Play();
        }

        Debug.Log("¡¡¡PERSECUCIÓN FINAL INICIADA!!!");
    }

    public void TerminarPersecucionFinal()
    {
        if (!persecucionFinal)
        {
            return;
        }

        persecucionFinal = false;
        encuentroFinalIniciado = true;
        estaAcechando = false;

        if (agente != null)
        {
            agente.isStopped = true;
        }

        Debug.Log("Persecución final terminada. Comienza el encuentro con la bruja.");
    }
 
    public void AtraparBruja()
    {
        if (atrapada)
        {
            return;
        }

        atrapada = true;

        estaAcechando = false;

        if (agente != null)
        {
            agente.isStopped = true;
            agente.velocity = Vector3.zero;
        }

        Debug.Log("¡La bruja quedó atrapada en las espinas!");
        }

    void Update()
    {
        if (!persecucionFinal && !encuentroFinalIniciado && inventario != null &&
        inventario.objetoSlot1 != null &&
        inventario.objetoSlot2 != null &&
        inventario.objetoSlot3 != null)
        {
            IniciarPersecucionFinal();
        }
        
        if (iaActivada && estaAcechando && agente.enabled && agente.isOnNavMesh && jugador != null)
        {
            agente.SetDestination(jugador.position);

            //ChequearDistanciaAtaque();
            if (persecucionFinal)
            {
                ComprobarEspinas();
            }
            else
            {
                ChequearDistanciaAtaque();
            }
        }
    }
    void ComprobarEspinas()
{
    if (atrapada)
    {
        return;
    }

    TrampaEspinas trampa = FindFirstObjectByType<TrampaEspinas>();

    if (trampa == null)
    {
        return;
    }

    float distancia = Vector3.Distance(transform.position, trampa.transform.position);

    if (distancia <= distanciaTrampa)
    {
        AtraparBruja();
    }
}


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

            if (miAudioSource != null)
            {
                miAudioSource.Play();
            }
            
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
        /*if (NavMesh.SamplePosition(posicionObjetivo, out hit, 5f, NavMesh.AllAreas))
        {
            agente.enabled = true; 
            agente.Warp(hit.position);
            agente.enabled = false; 
        }*/
        int mascaraPermitida = agente != null ? agente.areaMask : NavMesh.AllAreas;

        if (NavMesh.SamplePosition(posicionObjetivo, out hit, 5f, mascaraPermitida))
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
        //if (estaAtrapada) return;

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
