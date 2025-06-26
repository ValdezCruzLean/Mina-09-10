using UnityEngine;

public class RecolectarObjeto : MonoBehaviour
{
    [SerializeField] private int puntoObjeto = 1;

    /* Este metodo implementado de la interfaz TakeDamage. 
        Se ejecuta cuando el jugador interactua con un trampa u enemigo.*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            {
                ScriptGameManager.instance.SumarObjetos(puntoObjeto);

                Destroy(gameObject);        // Destruye el objeto moneda para eliminarlo del juego.

            }
        }
    }
}
