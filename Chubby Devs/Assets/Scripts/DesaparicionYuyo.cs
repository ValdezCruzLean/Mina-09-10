using UnityEngine;

public class DesaparicionYuyo : MonoBehaviour
{
    public GameObject objetoADesaparecer;
    public Camera camara;
    public MonoBehaviour controlDeCamara;
    public Transform puntoObjetivo;
    public float velocidadRotacion = 2f;

    private int contadorEntradas = 0;
    private bool girarCamara = false;
    private Quaternion rotacionObjetivo;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            contadorEntradas++;

            if (contadorEntradas == 2)
            {
                if (objetoADesaparecer != null)
                    objetoADesaparecer.SetActive(false);

                if (controlDeCamara != null)
                    controlDeCamara.enabled = false;

                // Calcular rotación hacia el punto
                if (puntoObjetivo != null)
                {
                    Vector3 direccion = puntoObjetivo.position - camara.transform.position;
                    rotacionObjetivo = Quaternion.LookRotation(direccion);
                    girarCamara = true;
                }
            }
        }
    }

    void Update()
    {
        if (girarCamara)
        {
            camara.transform.rotation = Quaternion.Slerp(
                camara.transform.rotation,
                rotacionObjetivo,
                Time.deltaTime * velocidadRotacion
            );

            // Cuando llega al objetivo
            if (Quaternion.Angle(camara.transform.rotation, rotacionObjetivo) < 1f)
            {
                girarCamara = false;

                // Reactivar el control sin restablecer rotación
                if (controlDeCamara != null)
                    controlDeCamara.enabled = true;
            }
        }
    }
}

