using UnityEngine;

public class DesaparicionYuyo : MonoBehaviour
{
    public GameObject objetoADesaparecer;
    public Camera camara;
    public MonoBehaviour controlDeCamara;
    public Transform puntoObjetivo;
    public float velocidadRotacion = 2f;

    public Rigidbody rbFarol;
    public float velocidadRotacionMano = 2f;

    public int vecesNecesarias = 2;   // 🔹 Ahora es público y configurable

    private int contadorEntradas = 0;
    private bool girando = false;
    private Quaternion rotObjetivo;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        contadorEntradas++;

        if (contadorEntradas >= vecesNecesarias)   // 🔹 Usa el valor configurado
        {
            if (objetoADesaparecer != null)
                objetoADesaparecer.SetActive(false);

            if (controlDeCamara != null)
                controlDeCamara.enabled = false;

            if (rbFarol != null)
            {
                rbFarol.isKinematic = true;
                rbFarol.linearVelocity = Vector3.zero;
                rbFarol.angularVelocity = Vector3.zero;
            }

            Vector3 dir = puntoObjetivo.position - camara.transform.position;
            rotObjetivo = Quaternion.LookRotation(dir);

            girando = true;
        }
    }

    void Update()
    {
        if (!girando) return;

        camara.transform.rotation = Quaternion.RotateTowards(
            camara.transform.rotation,
            rotObjetivo,
            velocidadRotacion * Time.deltaTime * 60f
        );

        if (camara.transform.parent != null)
        {
            Quaternion targetPlayerRot = Quaternion.Euler(
                0f,
                camara.transform.rotation.eulerAngles.y,
                0f
            );

            camara.transform.parent.rotation = Quaternion.RotateTowards(
                camara.transform.parent.rotation,
                targetPlayerRot,
                velocidadRotacionMano * Time.deltaTime * 60f
            );
        }

        if (Quaternion.Angle(camara.transform.rotation, rotObjetivo) < 0.5f)
        {
            girando = false;

            if (rbFarol != null)
                rbFarol.isKinematic = false;

            if (controlDeCamara != null)
                controlDeCamara.enabled = true;
        }
    }
}
