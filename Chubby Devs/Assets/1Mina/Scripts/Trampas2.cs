using UnityEngine;

public class Trampas2 : MonoBehaviour
{
    [Header("Configuración de la Plataforma")]
    [Tooltip("Tiempo (en segundos) antes de que la plataforma empiece a caer.")]
    public float tiempoAntesDeCaer = 2f;

    [Tooltip("Velocidad de caída de la plataforma.")]
    public float velocidadDeCaida = 5f;

    [Header("Efecto de Temblor")]
    [Tooltip("Intensidad del temblor antes de caer.")]
    public float intensidadTemblor = 0.1f;

    [Tooltip("Frecuencia del temblor.")]
    public float frecuenciaTemblor = 25f;

    [Header("Desactivación Automática")]
    [Tooltip("Tiempo (en segundos) después de empezar a caer para desactivar la plataforma.")]
    public float tiempoAntesDeDesactivar = 1f;

    private bool activado = false;   // Ya fue activada por el jugador
    private bool cayendo = false;    // Ya empezó a caer
    private Vector3 posicionOriginal;
    private float tiempoTranscurrido = 0f;
    private float tiempoTemblando = 0f;

    private Rigidbody rb;

    void Start()
    {
        posicionOriginal = transform.position;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true; // No cae hasta que lo activemos
    }

    void Update()
    {
        if (activado && !cayendo)
        {
            tiempoTranscurrido += Time.deltaTime;

            // Mientras espera el tiempo, que tiemble
            if (tiempoTranscurrido < tiempoAntesDeCaer)
            {
                tiempoTemblando += Time.deltaTime * frecuenciaTemblor;
                float desplazamientoX = Mathf.Sin(tiempoTemblando) * intensidadTemblor;
                float desplazamientoZ = Mathf.Cos(tiempoTemblando) * intensidadTemblor;
                transform.position = posicionOriginal + new Vector3(desplazamientoX, 0, desplazamientoZ);
            }
            else
            {
                ComenzarCaida();
            }
        }

        // Si ya está cayendo, aplicamos movimiento hacia abajo
        if (cayendo)
        {
            transform.position += Vector3.down * velocidadDeCaida * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!activado && collision.collider.CompareTag("Player"))
        {
            activado = true; // Solo la primera vez
        }
    }

    void ComenzarCaida()
    {
        cayendo = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        // Llamamos a Desactivar() después del tiempo configurado
        Invoke(nameof(Desactivar), tiempoAntesDeDesactivar);
    }

    void Desactivar()
    {
        gameObject.SetActive(false);
    }

    // Opcional: para reiniciar la plataforma (si la reactivas luego por script)
    public void Reiniciar()
    {
        CancelInvoke(nameof(Desactivar));
        cayendo = false;
        activado = false;
        tiempoTranscurrido = 0f;
        tiempoTemblando = 0f;
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = posicionOriginal;
        gameObject.SetActive(true);
    }
}
