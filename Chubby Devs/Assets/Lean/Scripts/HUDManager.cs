using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private TextMeshProUGUI mensajeTexto;

    private void Awake()
    {
        instance = this;
        mensajeTexto.text = "";
    }

    public void MostrarMensaje(string mensaje)
    {
        mensajeTexto.text = mensaje;
    }

    public void OcultarMensaje()
    {
        mensajeTexto.text = "";
    }
}
