using UnityEngine;

public class DesaparicionYuyo : MonoBehaviour
{
    public GameObject objetoADesaparecer;     // El objeto que desaparece
    public Camera camara;                     // La cámara que se gira
    public MonoBehaviour controlDeCamara;     // El script de control de la cámara (ej. MouseLook)
    public float velocidadRotacion = 2f;

    private int contadorEntradas = 0;
    private bool girarCamara = false;
    private Quaternion rotacionObjetivo;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            contadorEntradas++;

            if (contadorEntradas == 2) // Solo la segunda vez
            {
                if (objetoADesaparecer != null)
                    objetoADesaparecer.SetActive(false);

                if (controlDeCamara != null)
                    controlDeCamara.enabled = false;

                // Calcular la rotación 180° en Y desde donde está la cámara
                rotacionObjetivo = Quaternion.Euler(0, camara.transform.eulerAngles.y + 180f, 0);
                girarCamara = true;
            }
        }
    }

    void Update()
    {
        if (girarCamara)
        {
            camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);

            // Si ya casi llegó, detenemos
            if (Quaternion.Angle(camara.transform.rotation, rotacionObjetivo) < 1f)
            {
                girarCamara = false;
                // Si querés reactivar el control de cámara después:
                if (controlDeCamara != null)
                    controlDeCamara.enabled = true;
            }
        }
    }
}


