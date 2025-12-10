using UnityEngine;

public class RecolectarObjeto : MonoBehaviour
{
    [SerializeField] private int puntoObjeto = 1;
    [SerializeField] private string mensajeHUD = "Presiona E para recoger";

    private bool puedeRecoger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeRecoger = true;
            HUDManager.instance.MostrarMensaje(mensajeHUD);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puedeRecoger = false;
            HUDManager.instance.OcultarMensaje();
        }
    }

    private void Update()
    {
        if (puedeRecoger && Input.GetKeyDown(KeyCode.E))
        {
            ScriptGameManager.instance.SumarObjetos(puntoObjeto);
            gameObject.SetActive(false); // Desactiva el objeto en vez de destruirlo 
            HUDManager.instance.OcultarMensaje();
        }
    }
}
