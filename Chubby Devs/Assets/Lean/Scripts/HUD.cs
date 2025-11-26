using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    /*Esto declara una variable publica de tipo TextMeshProUGUI llamada puntos. 
      * Se utiliza para mostrar informaci?n textual en la interfaz de usuario del juego.*/
   // public TextMeshProUGUI puntosCadaveres;
    public TextMeshProUGUI puntosObjetos;
    public TextMeshProUGUI puntosV;
    public GameObject objetoAEliminar;
    public GameObject objetoAudio;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*En el metodo update Verifica la escena activa y actualizar el texto de puntos en consecuencia.
     */
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Escena_prueba")
        {
            puntosObjetos.text = " OBJETOS: " + ScriptGameManager.instance.ObjetosTotalesEncontrados.ToString() +"/12";
            puntosV.text = "Vida: "+ ScriptGameManager.instance.PuntosTotalesV.ToString();

            if ( ScriptGameManager.instance.ObjetosTotalesEncontrados == 12)
            {
                //puntosD.text = "Enciende el Generador";
                //Debug.Log("Continuará...");
                objetoAEliminar.SetActive(false);
                objetoAudio.SetActive(true);
            }

        }



    }
    /*Este es un metodo publico que permite actualizar el texto del objeto puntos. 
     * Toma un argumento puntosTotales y establece el texto del objeto puntos en el valor de puntosTotales.*/
   /* public void ActualizarCadaveresEncontrados(int puntosTotales)
    {
        puntosCadaveres.text = puntosTotales.ToString();
    }*/
    public void ActualizarObjetosEncontrados(int puntosTotales)
    {
        puntosObjetos.text = puntosTotales.ToString();
    }
    public void ActualizarPuntosV(int puntosTotales)
    {
        puntosV.text = puntosTotales.ToString();
    }
   
}
