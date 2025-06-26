using UnityEngine;

public class SaveSystemSimple : MonoBehaviour
{
    public Transform cuerpoDelJugador; // Arrastralo desde la escena de juego

    void Start()
    {
        // Si se pidió cargar la posición desde el menú
        if (PlayerPrefs.GetInt("CargarDesdeBoton", 0) == 1)
        {
            CargarPosicion();
            PlayerPrefs.SetInt("CargarDesdeBoton", 0); // Resetear
        }
    }

    public void GuardarPosicion()
    {
        Vector3 pos = cuerpoDelJugador.position;
        PlayerPrefs.SetFloat("posX", pos.x);
        PlayerPrefs.SetFloat("posY", pos.y);
        PlayerPrefs.SetFloat("posZ", pos.z);
        PlayerPrefs.Save();
        Debug.Log(" Posición guardada: " + pos);
    }

    public void CargarPosicion()
    {
        if (PlayerPrefs.HasKey("posX"))
        {
            float x = PlayerPrefs.GetFloat("posX");
            float y = PlayerPrefs.GetFloat("posY");
            float z = PlayerPrefs.GetFloat("posZ");

            Vector3 nuevaPos = new Vector3(x, y, z);
            cuerpoDelJugador.position = nuevaPos;

            Debug.Log(" Posición cargada: " + nuevaPos);
        }
        else
        {
            Debug.LogWarning(" No hay datos guardados aún.");
        }
    }
}
