using UnityEngine;
using TMPro;

public class HUDManagerMina : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI mensajeInteraccion;
    public TextMeshProUGUI herramientaActual;

    void Start()
    {
        mensajeInteraccion.text = "";
        herramientaActual.text = "Herramienta: Ninguna";
    }

    public void MostrarMensaje(string texto, float duracion = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarTemporal(texto, duracion));
    }

    private System.Collections.IEnumerator MostrarTemporal(string texto, float duracion)
    {
        mensajeInteraccion.text = texto;
        yield return new WaitForSeconds(duracion);
        mensajeInteraccion.text = "";
    }

    public void ActualizarHerramienta(string nombre)
    {
        herramientaActual.text = "Herramienta: " + nombre;
    }
}
