using UnityEngine;

public class ReproducirAudio : MonoBehaviour
{   
    //Recibe Audio por parametros
    [SerializeField] private AudioSource audioSource;

    //Si está activado, el audio solo se reproducira una vez
    [SerializeField] private bool sonarUnaSolaVez = true;

    private bool audioReproducido = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si esta configurado para sonar una sola vez
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
