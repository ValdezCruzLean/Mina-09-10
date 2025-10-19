using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 5f;
    public float duracionMovimiento = 3f;
    void OnEnable()
    {
        StartCoroutine(DesactivarEnemigoDespuesDeTiempo());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
    IEnumerator DesactivarEnemigoDespuesDeTiempo()
    {
        yield return new WaitForSeconds(duracionMovimiento);
        gameObject.SetActive(false);  // desactiva el objeto completo
    }
}
