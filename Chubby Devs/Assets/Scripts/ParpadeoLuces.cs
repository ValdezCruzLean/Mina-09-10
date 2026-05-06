using UnityEngine;
using System.Collections;

public class ParpadeoLuces : MonoBehaviour
{
    [Header("Referencias")]
    public Light miLuz;            
    public MeshRenderer miFoco;     

    [Header("Ajustes de Intensidad")]
    public float luzMin = 0f;
    public float luzMax = 1200f;
    
    [Header("Ajustes de Brillo Material")]
    public Color colorDeLaLuz = Color.yellow;
    public float brilloMin = 0f;    
    public float brilloMax = 10f;    

    [Header("Velocidad")]
    public float velocidadMin = 0.05f;
    public float velocidadMax = 0.2f;

    [Header("Tiempos de Comportamiento")]
    public float tiempoEncendidaMin = 2f;  
    public float tiempoEncendidaMax = 7f; 
    public int parpadeosPorFalla = 5;

    private Material materialFoco;
    void Start()
    {
        if (miFoco != null) 
        {
            materialFoco = miFoco.material; 
            materialFoco.EnableKeyword("_EMISSION");
        }
        
        StartCoroutine(FlickerLogica());
    }

    IEnumerator FlickerLogica()
    {
        /*while (true)
        {
            float randomFactor = Random.value;

            if (miLuz != null)
                miLuz.intensity = Mathf.Lerp(luzMin, luzMax, randomFactor);

            if (materialFoco != null)
            {
                float intensidadActual = Mathf.Lerp(brilloMin, brilloMax, randomFactor);
                materialFoco.SetColor("_EmissionColor", colorDeLaLuz * intensidadActual);
                
                DynamicGI.SetEmissive(miFoco, colorDeLaLuz * intensidadActual);
            }

            yield return new WaitForSeconds(Random.Range(velocidadMin, velocidadMax));
        }*/

        while (true)
        {
            AplicarIntensidad(1f);
            
            yield return new WaitForSeconds(Random.Range(tiempoEncendidaMin, tiempoEncendidaMax));

            int cantidadParpadeos = Random.Range(3, parpadeosPorFalla);
            
            for (int i = 0; i < cantidadParpadeos; i++)
            {
                AplicarIntensidad(Random.Range(0f, 0.3f)); 
                yield return new WaitForSeconds(Random.Range(velocidadMin, velocidadMax));
                
                // Encendido rápido
                AplicarIntensidad(Random.Range(0.7f, 1f));
                yield return new WaitForSeconds(Random.Range(velocidadMin, velocidadMax));
            }
        }
    }

    void AplicarIntensidad(float factor)
    {
        if (miLuz != null)
            miLuz.intensity = Mathf.Lerp(luzMin, luzMax, factor);

        if (materialFoco != null)
        {
            float intensidadActual = Mathf.Lerp(brilloMin, brilloMax, factor);
            materialFoco.SetColor("_EmissionColor", colorDeLaLuz * intensidadActual);
            DynamicGI.SetEmissive(miFoco, colorDeLaLuz * intensidadActual);
        }
    }
}
