using System.Security.Cryptography;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    Light pointLight;
    public LightZoneProtectionZone lightZoneProtectionZone;
    [SerializeField]int valor = 0;
    private void Awake()
    {
        pointLight = GetComponent<Light>();
    }
    private void Update()
    {
        EffectPingPongLight();
        pointLight.range = lightZoneProtectionZone.RadiusSphere;
    }
    void EffectPingPongLight()
    {
        pointLight.intensity = Mathf.PingPong(Time.time, 2 * valor);
    }
}
