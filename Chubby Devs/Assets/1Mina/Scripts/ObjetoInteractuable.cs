using UnityEngine;
using System.Collections;

public class ObjetoInteractuable : MonoBehaviour
{
    public enum TipoInteraccion { Cadenas, Maderas, Cerradura }
    public TipoInteraccion tipo;

    [Header("Configuración de Fade")]
    public float fadeDuration = 1.5f;       // Tiempo total del desvanecimiento
    public float fadeSpeed = 1f;            // Velocidad del fade (1 = normal)

    [Header("Sonido")]
    public AudioClip sonidoRomper;
    private AudioSource audioSource;

    private HUDManagerMina hud;

    void Start()
    {
        hud = FindFirstObjectByType<HUDManagerMina>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Interactuar(InventarioHerramientas inventario)
    {
        switch (tipo)
        {
            case TipoInteraccion.Cadenas:
                if (inventario.herramientaActual == "Corta Cadenas")
                    StartCoroutine(DestruirObjeto(inventario, "Corta Cadenas"));
                else
                    hud.MostrarMensaje("No tengo la herramienta necesaria para esto");
                break;

            case TipoInteraccion.Maderas:
                if (inventario.herramientaActual == "Martillo")
                    StartCoroutine(DestruirObjeto(inventario, "Martillo"));
                else
                    hud.MostrarMensaje("No tengo la herramienta necesaria para esto");
                break;

            case TipoInteraccion.Cerradura:
                if (inventario.herramientaActual == "Llave")
                    StartCoroutine(DestruirObjeto(inventario, "Llave"));
                else
                    hud.MostrarMensaje("No tengo la herramienta necesaria para esto");
                break;
        }
    }

    private IEnumerator DestruirObjeto(InventarioHerramientas inv, string herramientaUsada)
    {
        Renderer rend = GetComponent<Renderer>();
        Material mat = rend.material;
        Color color = mat.color;

        // Reproducir sonido
        if (sonidoRomper != null)
            audioSource.PlayOneShot(sonidoRomper);

        float t = 0;

        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            color.a = alpha;
            mat.color = color;

            t += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        gameObject.SetActive(false);

        inv.QuitarHerramienta(herramientaUsada);
        hud.MostrarMensaje($"{herramientaUsada} se ha roto");
    }
}
