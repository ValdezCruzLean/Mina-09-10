using UnityEngine;

public class EquipamientoJugador : MonoBehaviour
{
    [Header("Punto donde se sostiene la herramienta")]
    public Transform puntoDeMano;

    [Header("Prefabs de herramientas")]
    public GameObject prefabMartillo;
    public GameObject prefabCortaCadenas;
    public GameObject prefabLlave;

    private GameObject herramientaActualGO;
    private string herramientaActual = "Ninguna";

    private InventarioHerramientas inventario;

    private float cooldownDpad = 0.3f;
    private float tiempoDpad;

    void Start()
    {
        inventario = GetComponent<InventarioHerramientas>();
    }

    void Update()
    {
        // Detectar cambio de herramienta (usa la info del inventario)
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventario.tieneMartillo)
            EquiparHerramienta("Martillo");
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventario.tieneCortaCadenas)
            EquiparHerramienta("Corta Cadenas");
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventario.tieneLlave)
            EquiparHerramienta("Llave");


         // 🎮 D-Pad real (no interfiere con movimiento)
        float dpadH = Input.GetAxisRaw("DPadHorizontal");
        float dpadV = Input.GetAxisRaw("DPadVertical");

        if (Time.time > tiempoDpad)
        {
            if (dpadH < -0.5f && inventario.tieneMartillo)
            {
                EquiparHerramienta("Martillo");
                tiempoDpad = Time.time + cooldownDpad;
            }
            else if (dpadH > 0.5f && inventario.tieneCortaCadenas)
            {
                EquiparHerramienta("Corta Cadenas");
                tiempoDpad = Time.time + cooldownDpad;
            }
            else if (dpadV > 0.5f && inventario.tieneLlave)
            {
                EquiparHerramienta("Llave");
                tiempoDpad = Time.time + cooldownDpad;
            }
            else if (dpadV < -0.5f)
            {
                QuitarHerramienta();
                tiempoDpad = Time.time + cooldownDpad;
            }
        }
    }

    public void EquiparHerramienta(string nombre)
    {
        // Si ya hay una herramienta equipada, destruirla
        if (herramientaActualGO != null)
            Destroy(herramientaActualGO);

        // Instanciar la nueva herramienta si existe
        GameObject prefab = null;
        switch (nombre)
        {
            case "Martillo": prefab = prefabMartillo; break;
            case "Corta Cadenas": prefab = prefabCortaCadenas; break;
            case "Llave": prefab = prefabLlave; break;
        }

        if (prefab != null)
        {
            herramientaActualGO = Instantiate(prefab, puntoDeMano);
            herramientaActualGO.transform.localPosition = Vector3.zero;
            herramientaActualGO.transform.localRotation = Quaternion.identity;
            herramientaActual = nombre;

            // 🔥 AVISAR AL INVENTARIO
            inventario.herramientaActual = nombre;
            inventario.ActualizarHUD(nombre);
        }
        else
        {
            herramientaActual = "Ninguna";
        }
    }

    public void QuitarHerramienta()
    {
        if (herramientaActualGO != null)
        {
            Destroy(herramientaActualGO);
            herramientaActualGO = null;
        }
        herramientaActual = "Ninguna";

        // 🔥 avisar al inventario
        inventario.herramientaActual = "Ninguna";
        inventario.ActualizarHUD("Ninguna");
    }
}
