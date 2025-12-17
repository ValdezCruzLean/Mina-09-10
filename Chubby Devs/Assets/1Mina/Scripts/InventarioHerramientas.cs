using UnityEngine;

public class InventarioHerramientas : MonoBehaviour
{
    public bool tieneMartillo;
    public bool tieneCortaCadenas;
    public bool tieneLlave;

    public string herramientaActual = "Ninguna";

    private HUDManagerMina hud;

    void Start()
    {
        hud = FindFirstObjectByType<HUDManagerMina>();
        hud.ActualizarHerramienta(herramientaActual);
    }

    /*void Update()
    {
        // Cambiar herramienta activa
        if (Input.GetKeyDown(KeyCode.Alpha1) && tieneMartillo)
        {
            herramientaActual = "Martillo";
            hud.ActualizarHerramienta("Martillo");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && tieneCortaCadenas)
        {
            herramientaActual = "Corta Cadenas";
            hud.ActualizarHerramienta("Corta Cadenas");
            Debug.Log("ARAAA");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && tieneLlave)
        {
            herramientaActual = "Llave";
            hud.ActualizarHerramienta("Llave");
        }
    }*/

    public void ActualizarHUD(string nombre)
    {
        herramientaActual = nombre;
        hud.ActualizarHerramienta(nombre);
    }

    public void AgregarHerramienta(string nombre)
    {
        switch (nombre)
        {
            case "Martillo": tieneMartillo = true; break;
            case "Corta Cadenas": tieneCortaCadenas = true; break;
            case "Llave": tieneLlave = true; break;
        }

        hud.MostrarMensaje($"Has recogido un {nombre}");

        // Equipar autom�ticamente si no tiene nada
        var equipamiento = GetComponent<EquipamientoJugador>();
        if (herramientaActual == "Ninguna")
        {
            equipamiento.EquiparHerramienta(nombre);
            //herramientaActual = nombre;
            //hud.ActualizarHerramienta(nombre);
        }
    }

    public void QuitarHerramienta(string nombre)
    {
        switch (nombre)
        {
            case "Martillo": tieneMartillo = false; break;
            case "Corta Cadenas": tieneCortaCadenas = false; break;
            case "Llave": tieneLlave = false; break;
        }

        herramientaActual = "Ninguna";
        hud.ActualizarHerramienta(herramientaActual);

        var equipamiento = GetComponent<EquipamientoJugador>();
        equipamiento.QuitarHerramienta();
    }
}
