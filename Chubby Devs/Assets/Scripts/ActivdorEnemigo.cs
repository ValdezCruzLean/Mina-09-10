using UnityEngine;

public class ActivdorEnemigo : MonoBehaviour
{
    public GameObject enemigo;       // referencia al enemigo que se activará
    //public Transform spawnPoint;     // dónde aparecerá el enemigo

    private bool activado = false;

    void OnTriggerEnter(Collider other)
    {
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;
            //enemigo.transform.position = spawnPoint.position;
            enemigo.SetActive(true);
        }
    }
}
