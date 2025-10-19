using UnityEngine;

public class EncontrarCadaver : MonoBehaviour
{
    [SerializeField] private int puntoCadaver = 1;

    /* Este metodo implementado de la interfaz TakeDamage. 
        Se ejecuta cuando el jugador interactua con un trampa u enemigo.*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            {
                ScriptGameManager.instance.SumarCadaveres(puntoCadaver);

                Destroy(gameObject);        // Destruye el objeto moneda para eliminarlo del juego.


            }
        }
    }
}