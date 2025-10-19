using UnityEngine;

public class Trampas : MonoBehaviour
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

    private bool jugadorEncima = false;
    private bool cayendo = false;
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
            rb.isKinematic = true; // No cae hasta que lo activemos
        }
    }

    void Update()
    {
        if (jugadorEncima && !cayendo)
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
                // Cuando acaba el tiempo, cae
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
        if (collision.collider.CompareTag("Player"))
        {
            jugadorEncima = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            jugadorEncima = false;
        }
    }

    void ComenzarCaida()
    {
        cayendo = true;
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    // Opcional: para reiniciar la plataforma (si la quieres reutilizar)
    public void Reiniciar()
    {
        cayendo = false;
        jugadorEncima = false;
        tiempoTranscurrido = 0f;
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = posicionOriginal;
    }
}