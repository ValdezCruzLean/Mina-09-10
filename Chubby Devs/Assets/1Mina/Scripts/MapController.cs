using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject mapUI;                // Panel completo del mapa
    public RectTransform playerIconUI;      // Icono del jugador
    public RectTransform mapImageUI;        // Imagen del mapa

    [Header("Referencias del Mundo")]
    public Transform player;                // Transform del jugador

    [Header("Límites del Mapa en el Mundo REAL")]
    public float worldMinX;
    public float worldMaxX;
    public float worldMinZ;
    public float worldMaxZ;

    private bool isOpen = false;

    void Update()
    {
        // Abrir y cerrar mapa con tecla M
        if (Input.GetKeyDown(KeyCode.M))
        {
            isOpen = !isOpen;
            mapUI.SetActive(isOpen);
        }

        if (isOpen)
            UpdatePlayerIcon();
    }

    void UpdatePlayerIcon()
    {
        // Normalizar la posicion del jugador entre 0 y 1
        float normalizedX = Mathf.InverseLerp(worldMinX, worldMaxX, player.position.x);
        float normalizedZ = Mathf.InverseLerp(worldMinZ, worldMaxZ, player.position.z);

        // Obtener el tamaño del mapa UI
        float mapWidth = mapImageUI.rect.width;
        float mapHeight = mapImageUI.rect.height;

        // Convertir a coordenadas UI dentro de la imagen
        float posX = (normalizedX * mapWidth) - (mapWidth / 2f);
        float posY = (normalizedZ * mapHeight) - (mapHeight / 2f);

        playerIconUI.anchoredPosition = new Vector2(posX, posY);

        // Rotar icono segun direccion del jugador
        playerIconUI.localEulerAngles = new Vector3(0, 0, -player.eulerAngles.y);
    }
}
