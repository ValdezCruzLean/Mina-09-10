using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlSombra : MonoBehaviour
{
    public GameObject sombraVisual; // GameObject con Animator (desactivado al inicio)
    public Animator animadorSombra; // Animator dentro de sombraVisual
    public string nombrePantallaDerrota = "Derrota";
    public float tiempoAntesDeDerrota = 2f;

    // Offsets ajustables desde el inspector
    public Vector3 offsetPosicion = new Vector3(0, 0, 1.5f); // 1.5 unidades adelante por defecto
    public Vector3 offsetRotacion = Vector3.zero; // sin rotación extra por defecto

    private bool tocoJugador = false;

    private IEnumerator PerderTrasAnimacion()
    {
        yield return new WaitForSeconds(tiempoAntesDeDerrota);
        SceneManager.LoadScene(nombrePantallaDerrota);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!tocoJugador && other.CompareTag("Player"))
        {
            tocoJugador = true;

            Camera cam = Camera.main;

            Vector3 spawnPos = cam.transform.position + cam.transform.forward * 1.5f;
            spawnPos.y = other.transform.position.y; // ajustamos altura para que no quede flotando

            sombraVisual.transform.position = spawnPos;

            sombraVisual.transform.LookAt(cam.transform);
            sombraVisual.transform.Rotate(0, 180f, 0);

            sombraVisual.SetActive(true);
            animadorSombra.SetTrigger("Aparece");

            StartCoroutine(PerderTrasAnimacion());
        }
    }
}
