using UnityEngine;

public class JugadorInteraccion : MonoBehaviour
{
    public float distanciaInteraccion = 3f;
    public Camera camaraJugador;

    private HUDManagerMina hud;
    private InventarioHerramientas inventario;

    void Start()
    {
        hud = Object.FindFirstObjectByType<HUDManagerMina>();
        inventario = GetComponent<InventarioHerramientas>();
    }

    void Update()
    {
        Ray rayo = new Ray(camaraJugador.transform.position, camaraJugador.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distanciaInteraccion))
        {
            if (hit.collider.CompareTag("Recolectable"))
            {
                hud.MostrarMensaje("Recoger objeto con letra E");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    string nombre = hit.collider.gameObject.name;
                    inventario.AgregarHerramienta(nombre);
                    hit.collider.gameObject.SetActive(false);
                }
            }
            else if (hit.collider.CompareTag("Interactuable"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<ObjetoInteractuable>().Interactuar(inventario);
                }
            }
        }
    }
}
