using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaManager : MonoBehaviour
{
    public void CambiarEscena() 
    {
        SceneManager.LoadScene("Escena_prueba");
    }
}
