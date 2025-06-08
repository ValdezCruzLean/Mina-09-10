using UnityEngine;

public class CambioDePisada : MonoBehaviour
{
    public AudioClip clipPasos;
    public GameObject player; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("Hola");
            player.GetComponent<AudioSource>().clip = clipPasos;
        }
    }
}
