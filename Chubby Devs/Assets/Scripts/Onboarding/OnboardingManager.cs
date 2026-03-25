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

    string mensajeAdaptado = AdaptarTexto(mensaje);

    panelOnboarding.SetActive(true);

    // Arranca efecto máquina de escribir
    rutinaEscritura = StartCoroutine(EscribirTexto(mensajeAdaptado));
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

    private string AdaptarTexto(string original)
    {
        string teclaAccion = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
        string teclaCerrar = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(X)" : "[TAB]";
        
        string teclaLampara = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(Y)" : "[R]";

        return original.Replace("{INTERACT}", teclaAccion).Replace("{CLOSE}", teclaCerrar).Replace("{LIGHT}", teclaLampara);;
    }

    IEnumerator OcultarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoVisible);
        panelOnboarding.SetActive(false);
    }

}
