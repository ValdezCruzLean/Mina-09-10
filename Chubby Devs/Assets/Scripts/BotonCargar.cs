using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCargar : MonoBehaviour
{
    public void CargarJuegoYPosicion()
    {
        PlayerPrefs.SetInt("CargarDesdeBoton", 1); // Marca que hay que cargar
        SceneManager.LoadScene("Escena_prueba");    // Carga la escena intermedia
    }
}