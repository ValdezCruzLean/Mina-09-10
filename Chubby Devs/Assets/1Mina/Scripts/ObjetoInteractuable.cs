using UnityEngine;
using System.Collections;

public class ObjetoInteractuable : MonoBehaviour
{
    public enum TipoInteraccion { Cadenas, Maderas, Cerradura }
    public TipoInteraccion tipo;

    private HUDManagerMina hud;

    void Start()
    {
        hud = FindFirstObjectByType<HUDManagerMina>();
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
        Color color = rend.material.color;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, t);
            rend.material.color = color;
            yield return null;
        }

        gameObject.SetActive(false);
        inv.QuitarHerramienta(herramientaUsada);
        hud.MostrarMensaje($"{herramientaUsada} se ha roto");
    }
}
