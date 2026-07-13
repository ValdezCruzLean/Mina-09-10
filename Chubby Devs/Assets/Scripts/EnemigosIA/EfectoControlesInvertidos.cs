using System.Collections;
using UnityEngine;

public class EfectoControlesInvertidos : MonoBehaviour
{
    [Header("Configuración")]
    public Transform camaraJugador;
    
    private bool estaInvertido = false;
    private float tiempoRestante = 0f;

    public void ActivarInversion(float duracion)
    {
        tiempoRestante = duracion;
        if (!estaInvertido)
        {
            StartCoroutine(RutinaInversion());
        }
    }

    IEnumerator RutinaInversion()
    {
        estaInvertido = true;
        Debug.Log("¡Controles Invertidos aplicando rotación sigilosa!");

        transform.Rotate(0, 180, 0);

        if (camaraJugador != null)
        {
            camaraJugador.Rotate(0, -180, 0);
        }

        while (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        transform.Rotate(0, 180, 0);
        
        if (camaraJugador != null)
        {
            camaraJugador.Rotate(0, -180, 0);
        }

        estaInvertido = false;
        Debug.Log("Controles devueltos a la normalidad.");
    }
}
