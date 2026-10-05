using UnityEngine;
using TMPro;

public class RecolectarObjetoInventario : MonoBehaviour
{
     public float distanciaRay = 3f;

    public Camera camaraJugador;
    public TMP_Text textoInteraccion;
    public GameObject imagenUI;

    private GameObject objetoDetectado;
    //private InventarioJugador inventario;
    public InventarioJugador inventario;

    public Sprite iconoEspinas;
    public Sprite iconoAguaBendita;
    public Sprite iconoObjeto3;
    void Start()
    {
        textoInteraccion.gameObject.SetActive(false);
        imagenUI.gameObject.SetActive(false);

        //inventario = GetComponent<InventarioJugador>();
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

        Ray rayo = new Ray(
            camaraJugador.transform.position,
            camaraJugador.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distanciaRay))
        {
            if (hit.collider.CompareTag("ObjetosInventario"))
            {
                objetoDetectado = hit.collider.gameObject;

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
            Input.GetKeyDown(KeyCode.JoystickButton1);

        if (objetoDetectado != null && recogerInput)
        {
            AgregarAlInventario(objetoDetectado);
        }
    }

    void AgregarAlInventario(GameObject objeto)
    {
        inventario.AgregarObjeto(objeto);

        textoInteraccion.gameObject.SetActive(false);
        imagenUI.gameObject.SetActive(false);
    }

}
