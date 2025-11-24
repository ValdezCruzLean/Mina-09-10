using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CreditsController : MonoBehaviour
{
    [Header("Título del Juego")]
    public CanvasGroup tituloGroup; // Para el fade del título
    public float tituloFadeInTime = 2f;
    public float tituloFadeOutTime = 2f;
    public float tiempoVisibleTitulo = 2f;

    [Header("Créditos")]
    public RectTransform contenidoCreditos; // Lo que sube
    public float velocidadScroll = 40f;     // Velocidad del scroll
    public float delayAntesDeCreditos = 1f;

    private bool empezarScroll = false;

    private void Start()
    {
        StartCoroutine(SecuenciaCreditos());
    }

    private IEnumerator SecuenciaCreditos()
    {
        // 1. Asegurar que el título esté invisible al inicio
        tituloGroup.alpha = 0f;

        // 2. Fade-in del título
        yield return StartCoroutine(FadeCanvasGroup(tituloGroup, 0f, 1f, tituloFadeInTime));

        // 3. Tiempo visible
        yield return new WaitForSeconds(tiempoVisibleTitulo);

        // 4. Fade-out del título
        yield return StartCoroutine(FadeCanvasGroup(tituloGroup, 1f, 0f, tituloFadeOutTime));

        // 5. Espera
        yield return new WaitForSeconds(delayAntesDeCreditos);

        // 6. Iniciar scroll de créditos
        empezarScroll = true;
    }

    private void Update()
    {
        if (empezarScroll)
        {
            contenidoCreditos.anchoredPosition += Vector2.up * velocidadScroll * Time.deltaTime;
        }
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float tiempo)
    {
        float t = 0;
        while (t < tiempo)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / tiempo);
            yield return null;
        }
    }
}
