using UnityEngine;

public class ActivarIA : MonoBehaviour
{
    public EnemigoAparece scriptEnemigo; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scriptEnemigo != null)
            {
                scriptEnemigo.ActivarIA(); 
                Debug.Log("¡La IA de la bruja se ha despertado!");
            }

            gameObject.SetActive(false); 
        }
    }
}
