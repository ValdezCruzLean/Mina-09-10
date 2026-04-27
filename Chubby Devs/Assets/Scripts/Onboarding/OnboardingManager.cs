using UnityEngine;
using TMPro;
using System.Collections;
public class OnboardingManager : MonoBehaviour
{
    public static OnboardingManager Instance;

    [Header("UI Components")]
    public GameObject panelOnboarding;
    public TextMeshProUGUI textoOnboarding;

    [Header("Settings")]
    public float tiempoVisible = 5f; // Cuánto dura el mensaje en pantalla

    private Coroutine rutinaOcultar;
    private Coroutine rutinaEscritura;
    private bool isTyping = false;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelOnboarding.SetActive(false);
    }

   /* public void MostrarConsejo(string mensaje)
    {
        if (rutinaOcultar != null) StopCoroutine(rutinaOcultar);

        string mensajeAdaptado = AdaptarTexto(mensaje);
        
        textoOnboarding.text = mensajeAdaptado;
        panelOnboarding.SetActive(true);

        rutinaOcultar = StartCoroutine(OcultarDespuesDeTiempo());
    }*/
    public void MostrarConsejo(string mensaje)
    {
        // Frenar corrutinas anteriores
        if (rutinaOcultar != null) StopCoroutine(rutinaOcultar);
        if (rutinaEscritura != null) StopCoroutine(rutinaEscritura);

        //string mensajeAdaptado = AdaptarTexto(mensaje);
        string mensajeFinal = ProcesarTodasLasEtiquetas(mensaje);

        panelOnboarding.SetActive(true);

        // Arranca efecto máquina de escribir
        //rutinaEscritura = StartCoroutine(EscribirTexto(mensajeAdaptado));
        rutinaEscritura = StartCoroutine(EscribirTexto(mensajeFinal));
    }

    IEnumerator EscribirTexto(string mensaje)
    {
        isTyping = true;
        textoOnboarding.text = "";

        foreach (char letra in mensaje)
        {
            textoOnboarding.text += letra;
            yield return new WaitForSeconds(0.03f); // más rápido que diálogo
        }

        isTyping = false;

        // recién cuando termina de escribir, empieza el temporizador
        rutinaOcultar = StartCoroutine(OcultarDespuesDeTiempo());
    }

    private string ProcesarTodasLasEtiquetas(string original)
    {
        bool esJoystick = ScriptGameManager.CurrentDevice == InputDevice.Joystick;

        /*string interact = esJoystick ? "(B)" : "[E]";
        string cerrar = esJoystick ? "(X)" : "[TAB]";
        string luz = esJoystick ? "(Y)" : "[R]";
        string mapa = esJoystick ? "(Select)" : "[M]";
        string prev = esJoystick ? "LB" : "←";
        string next = esJoystick ? "RB" : "→";*/

        string interact = esJoystick ? "<sprite name=\"Icon_BotonB\">" : "<sprite name=\"Icon_E\">";
        string cerrar = esJoystick ? "<sprite name=\"Icon_BotonX\">" : "<sprite name=\"Icon_TAB\">";
        string luz = esJoystick ? "<sprite name=\"Icon_BotonY\">" : "<sprite name=\"Icon_R\">";
        string mapa = esJoystick ? "<sprite name=\"Icon_Select\">" : "<sprite name=\"Icon_M\">";
        string prev = esJoystick ? "<sprite name=\"Icon_LB\">" : "<sprite name=\"Icon_FlechaIzq\">";
        string next = esJoystick ? "<sprite name=\"Icon_RB\">" : "<sprite name=\"Icon_FlechaDer\">";

        return original.Replace("{INTERACT}", interact)
                       .Replace("{CLOSE}", cerrar)
                       .Replace("{LIGHT}", luz)
                       .Replace("{MAP}", mapa)
                       .Replace("{PREV}", prev)
                       .Replace("{NEXT}", next);
    }

    /*private string AdaptarTexto(string original)
    {
        string teclaAccion = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
        string teclaCerrar = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(X)" : "[TAB]";
        
        string teclaLampara = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(Y)" : "[R]";
        string teclaMapa = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(Select)" : "[M]";

        return original.Replace("{INTERACT}", teclaAccion).Replace("{CLOSE}", teclaCerrar).Replace("{LIGHT}", teclaLampara).Replace("{MAP}", teclaMapa);
    }*/

    /*private string AdaptarTextoNotas(string original)
    {
        string anterior = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "LB" : "←";
        string siguiente = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "RB" : "→";
        
        return original.Replace("{PREV}", anterior).Replace("{NEXT}", siguiente);
    }*/

    IEnumerator OcultarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoVisible);
        panelOnboarding.SetActive(false);
    }

}
