using UnityEngine;

public class ReproducirAudio : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("Si está activado, el audio solo se reproducirá una vez.")]
    [SerializeField] private bool sonarUnaSolaVez = true;

    private bool audioReproducido = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si está configurado para sonar una sola vez
            if (sonarUnaSolaVez)
            {
                if (!audioReproducido)
                {
                    audioSource.Play();
                    audioReproducido = true;
                }
            }
            else
            {
                // Sonar cada vez que colisiona
                audioSource.Play();
            }
        }
    }
}
