using UnityEngine;

public class DetectorDeMirada : MonoBehaviour
{
    public Camera camaraJugador;
    public float rangoVision = 50f;
    public LayerMask capaEnemigo;
    public ControladorCamara controladorCamara;

    void Update()
    {
        Ray rayo = new Ray(camaraJugador.transform.position, camaraJugador.transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit hit, rangoVision, capaEnemigo))
        {
            if (hit.collider.CompareTag("Enemigo"))
            {
                controladorCamara.IniciarEvento(hit.collider.gameObject);
            }
        }
    }
}
