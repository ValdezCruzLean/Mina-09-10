using System.Security.Cryptography;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    Light pointLight;

    [SerializeField] float pingPongSpeed = 2f;
    [SerializeField] float maxIntensity = 2f;
    [SerializeField] float normalIntensity = 1.5f;

    [SerializeField] float maxRange = 5f;
    [SerializeField] float minRange = 2f;

    bool isPingPongActive = false;

    private void Awake()
    {
        pointLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (TimeLight.Instance.seconds >= 15)
        {
            pointLight.intensity = 0f;
            pointLight.range = 0f;
            isPingPongActive = false;
        }
        else if (TimeLight.Instance.seconds >= 6)
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
            float pingRange = Mathf.PingPong(Time.time * pingPongSpeed, maxRange - minRange);
            pointLight.range = minRange + pingRange;
        }
        LightZoneProtection.Instance.radiusSphere = pointLight.range;
    }
}
