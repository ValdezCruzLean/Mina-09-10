using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCargar : MonoBehaviour
{
    public string nombreEscenaDelJuego = "Juego";

    public void CargarJuegoYPosicion()
    {
        PlayerPrefs.SetInt("CargarDesdeBoton", 1); // Marca que hay que cargar
        SceneManager.LoadScene(nombreEscenaDelJuego); // Carga la escena del juego
    }
}
