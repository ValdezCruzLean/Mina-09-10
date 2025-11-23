using UnityEngine;

public class ReproducirAudioUnaVez : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool audioReproducido = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!audioReproducido && collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            audioReproducido = true;
        }
    }
}
