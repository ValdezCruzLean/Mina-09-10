using UnityEngine;

public class SeguirDetras : MonoBehaviour
{
    public Transform jugador;     
    public float distanciaDetras = 15f;

    public float intervalo = 5f;
    private float tiempoSiguienteSonido = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Vector3 direccionDetras = -jugador.forward;
        transform.position = jugador.position + direccionDetras * distanciaDetras;

        transform.LookAt(jugador);

        if (Time.time >= tiempoSiguienteSonido)
        {
            audioSource.Play();
            tiempoSiguienteSonido = Time.time + intervalo;
        }
    }
}
