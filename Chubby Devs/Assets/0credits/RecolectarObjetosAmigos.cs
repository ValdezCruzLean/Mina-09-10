using UnityEngine;
using TMPro;

public class RecolectarObjetosAmigos : MonoBehaviour
{
    
    public float distanciaRay = 3f;  // Distancia para recoger

    
    public Camera camaraJugador;         // La camara del jugador
    public TMP_Text textoInteraccion;    // "Presiona E para recoger"

    
    public int puntosASumar = 1;

    // Interno
    private GameObject objetoDetectado;

    void Start()
    {
        textoInteraccion.gameObject.SetActive(false);
    }

    void Update()
    {
        DetectarObjeto();
        RevisarEntrada();
    }

    void DetectarObjeto()
    {
        objetoDetectado = null;
        textoInteraccion.gameObject.SetActive(false);

        Ray rayo = new Ray(camaraJugador.transform.position, camaraJugador.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distanciaRay))
        {
            if (hit.collider.CompareTag("ObjetosDeAmigos"))
            {
                objetoDetectado = hit.collider.gameObject;
                textoInteraccion.text = "Presiona E para recoger";
                textoInteraccion.gameObject.SetActive(true);
            }
        }
    }

    void RevisarEntrada()
    {
        if (objetoDetectado != null && Input.GetKeyDown(KeyCode.E))
        {
            ScriptGameManager.instance.SumarObjetos(puntosASumar);

            // Oculta el mensaje
            textoInteraccion.gameObject.SetActive(false);

            // Desactiva el objeto
            objetoDetectado.SetActive(false);

            
        }
    }
}