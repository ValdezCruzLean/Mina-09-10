using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CargarNivel1()
    {
        SceneManager.LoadScene("Escena_prueba");
    }

    public void CargarNivel2()
    {
        SceneManager.LoadScene("Mina");
    }
}
