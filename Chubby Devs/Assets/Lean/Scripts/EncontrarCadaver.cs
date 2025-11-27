using UnityEngine;

public class EncontrarCadaver : MonoBehaviour
{
   // [SerializeField] private int puntoCadaver = 1;

    private void OnCollisionEnter(Collision collision)
    {
      /*  if (collision.gameObject.CompareTag("Player"))
        {
            ScriptGameManager.instance.SumarCadaveres(puntoCadaver);

            gameObject.SetActive(false);  // ✅ Desactiva el objeto en vez de destruirlo
        }*/
    }
}