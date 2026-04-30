using UnityEngine;
using TMPro;

public class RecolectarObjetosAmigos : MonoBehaviour
{
    
    public float distanciaRay = 3f;  // Distancia para recoger

    
    public Camera camaraJugador;         // La camara del jugador
    public TMP_Text textoInteraccion;    // texto de Presiona E para recoger
    public GameObject imagenUI;

    
    public int puntosASumar = 1;
    private GameObject objetoDetectado;

    void Start()
    {
        textoInteraccion.gameObject.SetActive(false);
        imagenUI.gameObject.SetActive(false);
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
        imagenUI.gameObject.SetActive(false);

        Ray rayo = new Ray(camaraJugador.transform.position, camaraJugador.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distanciaRay))
        {
            if (hit.collider.CompareTag("ObjetosDeAmigos"))
            {
                objetoDetectado = hit.collider.gameObject;

                //textoInteraccion.text = "Presiona E para recoger";
                /*if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
                {
                    textoInteraccion.text = "Presiona (B) para recoger";
                }
                else
                {
                    textoInteraccion.text = "Presiona [E] para recoger";
                }*/

                if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
                {
                    textoInteraccion.text = "Presiona <sprite name=\"Icon_BotonB\"> para recoger";
                }
                else
                {
                    textoInteraccion.text = "Presiona <sprite name=\"Icon_E\"> para recoger";
                }

                textoInteraccion.gameObject.SetActive(true);
                imagenUI.gameObject.SetActive(true);
            }
        }
    }

    void RevisarEntrada()
    {
        bool recogerInput =
            Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.JoystickButton1); // 🎮 B

        if (objetoDetectado != null && recogerInput)
        {
            ScriptGameManager.instance.SumarObjetos(puntosASumar);

            // Oculta el mensaje
            textoInteraccion.gameObject.SetActive(false);
            imagenUI.gameObject.SetActive(false);

            // Desactiva el objeto
            objetoDetectado.SetActive(false);

            
        }
    }
}