using System.Collections;
using UnityEngine;

public class EfectoVisionJugador : MonoBehaviour
{
    [Header("Configuración de Ceguera")]
    public CanvasGroup panelCeguera;
    public float tiempoCiego = 3f; 
    public float velocidadFade = 2f;

    private bool estaCegado = false;

    void Start()
    {
        if (panelCeguera != null)
        {
            panelCeguera.alpha = 0f;
            panelCeguera.gameObject.SetActive(false);
        }
    }

    public void IniciarCeguera()
    {
        if (!estaCegado)
        {
            StartCoroutine(RutinaCeguera());
        }
    }

    IEnumerator RutinaCeguera()
    {
        estaCegado = true;
        panelCeguera.gameObject.SetActive(true);

        while (panelCeguera.alpha < 1f)
        {
            panelCeguera.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }
        panelCeguera.alpha = 1f;

        yield return new WaitForSeconds(tiempoCiego);

        while (panelCeguera.alpha > 0f)
        {
            panelCeguera.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }
        panelCeguera.alpha = 0f;
        panelCeguera.gameObject.SetActive(false);
        
        estaCegado = false;
    }
}
