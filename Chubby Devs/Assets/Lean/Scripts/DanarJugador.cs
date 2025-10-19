using UnityEngine;

public class DanarJugador : MonoBehaviour
{
    /*Esta variable representa el valor de los puntos de da�o.*/
    [SerializeField] private int scoreDamage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // destruye este objeto
                                 // Encuentra el objeto GameManager en la escena y le resta los puntos correspondientes.
            ScriptGameManager.instance.RestarPuntosV(scoreDamage);

            Destroy(gameObject);        // Destruye el objeto moneda para eliminarlo del juego.
        }
    }
}
    
        

    
