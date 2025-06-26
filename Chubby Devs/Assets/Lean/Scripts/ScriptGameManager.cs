using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class ScriptGameManager : MonoBehaviour
{
    /*Variable tipo Hud para representar el objeto Hud de nuestro juego*/
     public HUD hud;
    /*Esto declara una propiedad estatica llamada instance que permite acceder a una instancia ?nica de la clase ScriptGameManager. 
     * El acceso a la instancia es a traves de ScriptGameManager.instance.*/
    public static ScriptGameManager instance { get; private set; }
    /*Esto declara una propiedad publica llamada PuntosTotales que proporciona acceso a la variable privada puntosTotales. 
     * Esto permite a otras clases obtener el valor de puntos totales sin modificarlo directamente.*/
    public int ObjetosTotalesEncontrados { get { return objetosTotalesEncontrados; } }
    public int CadaveresTotalesEncontrados { get { return cadaveresTotalesEncontrados; } }
    public int PuntosTotalesV { get { return puntosVida; } }

    /*Esta variable privada almacena la cantidad total de objetos de compañeros encontrados en el juego.*/
    private int objetosTotalesEncontrados;
    /*Esta variable privada almacena la cantidad total de cadaveres localizados en el juego.*/
    private int cadaveresTotalesEncontrados;
    /*Esta variable privada almacena la cantidad total de vida del jugador en el juego.*/
    private int puntosVida = 100;
  

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*Este metodo se llama al comienzo de la ejecucion del juego, antes del metodo Start. 
     * Se comprueba si ya existe una instancia de ScriptGameManager y, en caso contrario, se establece esta instancia como la actual. 
     * Esto garantiza que solo haya una instancia de ScriptGameManager en la escena.*/
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Mas de un GameManager en escena");
        }
    }
   
    public void SumarCadaveres(int puntosASumar)
    {
        cadaveresTotalesEncontrados += puntosASumar;
        Debug.Log(cadaveresTotalesEncontrados);
        hud.ActualizarCadaveresEncontrados(cadaveresTotalesEncontrados);


        /* if (SceneManager.GetActiveScene().name == "Level1" && puntosDesechos >= 1)
         {
             SceneManager.LoadScene("YouWin");
         }*/


    }
    public void SumarObjetos(int puntosASumar)
    {
        objetosTotalesEncontrados += puntosASumar;
        Debug.Log(objetosTotalesEncontrados);
        hud.ActualizarObjetosEncontrados(objetosTotalesEncontrados);

        if (objetosTotalesEncontrados >= 50)
        {
            SceneManager.LoadScene("Win");
        }


    }
    public void SumarPuntosV(int puntosASumar)
    {
        puntosVida += puntosASumar;
        Debug.Log(puntosVida);
        hud.ActualizarPuntosV(puntosVida);
    }

    /*Este m?todo se utiliza para restar puntos al puntaje total del jugador. Se suma la cantidad puntosASumar a puntosTotales,
    se actualiza la interfaz de usuario a traves de hud, y se verifica si el jugador ha alcanzado ciertos puntos en escenas especificas
    para cargar pantalla gameover.*/
    public void RestarObjetos(int puntosARestar)
    {
        objetosTotalesEncontrados -= puntosARestar;

        Debug.Log(objetosTotalesEncontrados);
        hud.ActualizarObjetosEncontrados(objetosTotalesEncontrados);

    }
    public void RestarCadaveres(int puntosARestar)
    {
        cadaveresTotalesEncontrados -= puntosARestar;

        Debug.Log(cadaveresTotalesEncontrados);
        hud.ActualizarCadaveresEncontrados(cadaveresTotalesEncontrados);


    }
    public void RestarPuntosV(int puntosARestar)
    {
        puntosVida -= puntosARestar;


        Debug.Log(puntosVida);
        hud.ActualizarPuntosV(puntosVida);
        if (puntosVida <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }

    }
}
