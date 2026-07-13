using UnityEngine;
using UnityEngine.UI;

public class TimeLight : MonoBehaviour
{
    public static TimeLight Instance;
    public float seconds = 0f;
    public bool readyToReset = true;
    //public Text textoTiempo;

    public Image barraLampara;
    public GameObject barraLamparaSilueta;

    public float tiempoMaximo = 60f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!readyToReset)
        {
            seconds += Time.deltaTime;
        }

        /*if (textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + Mathf.FloorToInt(seconds) + "s";
        }*/
        ActualizarBarra();
    }

    public void ResetTimer()
    {
        seconds = 0f;
        readyToReset = false;
         ActualizarBarra();
    }

    public void VaciarTemporizador()
    {
        seconds = tiempoMaximo; 
        readyToReset = true;   
        ActualizarBarra();  
    }

    public void IniciarTiempo()
    {
        readyToReset = false;
    }

    public void DetenerTiempo()
    {
        readyToReset = true;
    }

    void ActualizarBarra()
    {
        if (barraLampara != null)
        {
            float progreso = 1f - (seconds / tiempoMaximo);
            barraLampara.fillAmount = Mathf.Clamp01(progreso);
        }
    }

    /*public void MostrarBarraLampara(bool mostrar)
    {
        if (barraLampara != null)
            barraLampara.gameObject.SetActive(mostrar);

        if (barraLamparaSilueta != null)
            barraLamparaSilueta.SetActive(mostrar);
    }*/

    public void MostrarSoloSilueta()
    {
        barraLamparaSilueta.SetActive(true);
        barraLampara.gameObject.SetActive(false);    }

    public void MostrarBarraCompleta()
    {
        //barraLamparaSilueta.SetActive(false);
        barraLampara.gameObject.SetActive(true);
    }

}