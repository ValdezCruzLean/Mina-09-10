using UnityEngine;

public class AvisoScreamer : MonoBehaviour
{
    public AudioSource warningSound;
    public Transform player;
    public float distanciaMaxima = 10f;
    private bool yaSono = false;

    void Update()
    {
        if (!yaSono && Vector3.Distance(transform.position, player.position) < distanciaMaxima)
        {
            warningSound.Play();
            yaSono = true;
        }
    }
}
