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
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panelOnboarding.SetActive(false);
    }

    // El método mágico que llamarás desde otros scripts
    public void MostrarConsejo(string mensaje)
    {
        // Detener la cuenta regresiva si ya había un mensaje
        if (rutinaOcultar != null) StopCoroutine(rutinaOcultar);

        // Adaptar el texto según el dispositivo
        string mensajeAdaptado = AdaptarTexto(mensaje);
        
        textoOnboarding.text = mensajeAdaptado;
        panelOnboarding.SetActive(true);

        // Iniciar cuenta para desaparecer
        rutinaOcultar = StartCoroutine(OcultarDespuesDeTiempo());
    }

    private string AdaptarTexto(string original)
    {
        string teclaAccion = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
        string teclaCerrar = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(X)" : "[TAB]";
        
        // Aquí puedes reemplazar etiquetas personalizadas
        return original.Replace("{INTERACT}", teclaAccion).Replace("{CLOSE}", teclaCerrar);
    }

    IEnumerator OcultarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoVisible);
        panelOnboarding.SetActive(false);
    }

}
