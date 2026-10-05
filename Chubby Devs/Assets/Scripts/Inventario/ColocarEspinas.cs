using UnityEngine;

public class ColocarEspinas : MonoBehaviour
{
    public Camera camaraJugador;
    public GameObject espinasPreview;
     public GameObject espinasPrefab;
    public InventarioJugador inventario;

    public float distanciaColocacion = 5f;

    private GameObject previewActual;
    private bool colocandoEspinas = false;
    public bool puedeColocar = false;

    void Update()
    {
        if (inventario == null)
        {
            return;
        }
        
        if (!puedeColocar)
        {
            return;
        }

        if (inventario.slotSeleccionado == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!colocandoEspinas)
                {
                    CrearPreview();
                }
                else
                {
                    ConfirmarColocacion();
                }
            }
        }

        if (colocandoEspinas)
        {
            MoverPreview();
        }
    }

    void CrearPreview()
    {
        if (espinasPreview == null)
        {
            Debug.LogError("No asignaste Espinas Preview en ColocarEspinas.");
            return;
        }

        previewActual = Instantiate(espinasPreview);

        colocandoEspinas = true;

        MoverPreview();
    }

    void MoverPreview()
{
    if (previewActual == null)
    {
        return;
    }

    Ray rayo = new Ray(
        camaraJugador.transform.position,
        camaraJugador.transform.forward
    );

    RaycastHit hit;

    if (Physics.Raycast(rayo, out hit, distanciaColocacion))
    {
        // Evitamos colocar las espinas sobre el propio jugador
        if (hit.collider.transform.IsChildOf(transform))
        {
            return;
        }

        // Primero colocamos el objeto en el punto del suelo
        previewActual.transform.position = hit.point;

        // Buscamos todos los Renderer del modelo
        Renderer[] renderers = previewActual.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            Bounds limites = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
            {
                limites.Encapsulate(renderers[i].bounds);
            }

            // Distancia entre el suelo y la parte más baja del modelo
            float diferenciaSuelo = hit.point.y - limites.min.y;

            // Levantamos el modelo esa cantidad
            previewActual.transform.position += Vector3.up * diferenciaSuelo;
        }
    }
}

    void ConfirmarColocacion()
{
    if (previewActual == null)
    {
        return;
    }

    if (espinasPrefab == null)
    {
        Debug.LogError("No asignaste Espinas Prefab en ColocarEspinas.");
        return;
    }

    // Creamos las espinas reales
    Instantiate(
        espinasPrefab,
        previewActual.transform.position,
        previewActual.transform.rotation
    );

    // Eliminamos la previsualización
    Destroy(previewActual);
    previewActual = null;

    colocandoEspinas = false;

    // Consumimos las espinas del inventario
    inventario.ConsumirObjetoSeleccionado();
}
}
