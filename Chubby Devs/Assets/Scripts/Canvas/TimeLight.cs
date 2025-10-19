using UnityEngine;
using UnityEngine.UI;

public class TimeLight : MonoBehaviour
{
    public static TimeLight Instance;
    public float seconds = 0f;
    public bool readyToReset = true;
    public Text textoTiempo;

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

        if (textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + Mathf.FloorToInt(seconds) + "s";
        }
    }

    public void ResetTimer()
    {
        seconds = 0f;
        readyToReset = false;
    }

    public void IniciarTiempo()
    {
        readyToReset = false;
    }

    public void DetenerTiempo()
    {
        readyToReset = true;
    }
}