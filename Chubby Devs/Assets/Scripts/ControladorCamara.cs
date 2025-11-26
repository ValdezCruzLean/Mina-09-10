using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ControladorCamara : MonoBehaviour
{
    public Camera camaraJugador;
    public Transform jugador;
    public GameObject controladorMovimientoJugador; // objeto con el script de movimiento
    public GameObject pasosJugador;
    public float duracion = 3f;

    private bool eventoActivo = false;
    //private GameObject enemigoObjetivo;
    private Transform enemigoObjetivo;

    public void IniciarEvento(GameObject enemigo)
    {
        if (eventoActivo) return;

        //enemigoObjetivo = enemigo;
        enemigoObjetivo = enemigo.transform; 
        eventoActivo = true;

        //controladorMovimientoJugador.SetActive(false); // desactiva movimiento
        controladorMovimientoJugador.GetComponent<FirstPersonController>().enabled = false;
        
        pasosJugador.GetComponent<SonidoPasos>().enabled = false;
        AudioSource audio = pasosJugador.GetComponent<AudioSource>();
        if (audio != null) audio.Stop();

        StartCoroutine(SeguirEnemigo());
    }

    IEnumerator SeguirEnemigo()
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            if (enemigoObjetivo != null)
            {
                //camaraJugador.transform.LookAt(enemigoObjetivo.transform.position);

                // 1. ROTAR al jugador hacia el enemigo (solo eje horizontal)
                Vector3 objetivo = enemigoObjetivo.position;
                objetivo.y = jugador.position.y; 
                jugador.LookAt(objetivo);

                camaraJugador.transform.LookAt(enemigoObjetivo.position);
            }

            tiempo += Time.deltaTime;
            Debug.Log("Tiempo seguimiento: " + tiempo);
            yield return null;
        }

        //controladorMovimientoJugador.SetActive(true);
        controladorMovimientoJugador.GetComponent<FirstPersonController>().enabled = true;
        pasosJugador.GetComponent<SonidoPasos>().enabled = true;
        eventoActivo = false;
    }
}
