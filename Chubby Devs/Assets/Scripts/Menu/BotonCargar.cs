using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCargar : MonoBehaviour
{
    public void CargarJuegoYPosicion()
    {
        PlayerPrefs.SetInt("CargarDesdeBoton", 1); // Marca que hay que cargar
        SceneManager.LoadScene("AnimacionAuto");    // Carga la escena intermedia
    }
}