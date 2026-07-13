using System.Collections;
using UnityEngine;

public class EfectoConfusionCamara : MonoBehaviour
{
    private Vector3 posicionOriginalLocal;
    private bool estaSacudiendo = false;
    private float tiempoRestante = 0f;

    void Awake()
    {
        posicionOriginalLocal = transform.localPosition;
    }

    public void ActivarSacudida(float duracion, float intensidad)
    {
        tiempoRestante = duracion;
        if (!estaSacudiendo)
        {
            StartCoroutine(RutinaSacudida(intensidad));
        }
    }

    IEnumerator RutinaSacudida(float intensidad)
    {
        estaSacudiendo = true;
        Debug.Log("¡Cámara perturbada por la bruja!");

        while (tiempoRestante > 0)
        {
            float offsetX = Random.Range(-1f, 1f) * intensidad;
            float offsetY = Random.Range(-1f, 1f) * intensidad;
            float offsetZ = Random.Range(-1f, 1f) * intensidad;

            transform.localPosition = posicionOriginalLocal + new Vector3(offsetX, offsetY, offsetZ);

            tiempoRestante -= Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicionOriginalLocal;
        estaSacudiendo = false;
        Debug.Log("Efecto de confusión terminado.");
    }
}
