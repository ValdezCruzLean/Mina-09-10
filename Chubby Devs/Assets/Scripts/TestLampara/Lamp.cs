using System.Security.Cryptography;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public CanvasFosforos canvasFosforos;
    public TimeLight timeLight;

    Light pointLight;

    [SerializeField] float pingPongSpeed = 2f;
    [SerializeField] float maxIntensity = 2f;
    [SerializeField] float normalIntensity = 1.5f;
    [SerializeField] float maxRange = 5f;

    bool isPingPongActive = false;
    bool lamparaEncendida = false;

    private void Awake()
    {
        pointLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (!lamparaEncendida && canvasFosforos.quiereEncenderLampara && canvasFosforos.TieneFosforo())
        {
            lamparaEncendida = true;
            canvasFosforos.RestarFosforo();
            timeLight.ResetTimer();
            canvasFosforos.quiereEncenderLampara = false;
        }

        if (!lamparaEncendida)
        {
            ApagarLuz();
            return;
        }

        if (timeLight.seconds >= 15)
        {
            ApagarLuz();
            timeLight.DetenerTiempo();
        }
        else if (timeLight.seconds >= 6)
        {
            isPingPongActive = true;
        }
        else
        {
            pointLight.intensity = normalIntensity;
            pointLight.range = maxRange;
            isPingPongActive = false;
        }

        if (isPingPongActive)
        {
            pointLight.intensity = Mathf.PingPong(Time.time * pingPongSpeed, maxIntensity);
        }

        LightZoneProtection.Instance.radiusSphere = pointLight.range;
    }

    private void ApagarLuz()
    {
        pointLight.intensity = 0f;
        pointLight.range = 0f;
        isPingPongActive = false;
        lamparaEncendida = false;
    }
}
