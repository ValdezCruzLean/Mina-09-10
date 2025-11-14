using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BotonesFadeAutomatico : MonoBehaviour
{
    public CanvasGroup uiGroup;
    public float delay = 1.5f;       // tiempo que tarda el video en aparecer
    public float fadeDuration = 1f;  // tiempo del fade de los botones

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Esperar al fade del video
        yield return new WaitForSeconds(delay);

        float t = 0f;
        uiGroup.alpha = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            uiGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        uiGroup.alpha = 1f;
        uiGroup.interactable = true;
        uiGroup.blocksRaycasts = true;
    }
}
