using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    public void Jugar(string Escena_prueba)
    {
        SceneManager.LoadScene(Escena_prueba);
    }
   
    public void Salir ()
    {
        Application.Quit();
        Debug.Log("Se salio loco");
    }
}
