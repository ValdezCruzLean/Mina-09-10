using UnityEngine;
using System.Collections;

public class ParpadeoLuces : MonoBehaviour
{
    [Header("Referencias")]
    public Light miLuz;            
    public MeshRenderer miFoco;     

    [Header("Ajustes de Intensidad")]
    public float luzMin = 500f;
    public float luzMax = 1200f;
    
    [Header("Ajustes de Brillo Material")]
    public Color colorDeLaLuz = Color.yellow;
    public float brilloMin = 1f;    
    public float brilloMax = 10f;    

    [Header("Velocidad")]
    public float velocidadMin = 0.05f;
    public float velocidadMax = 0.2f;

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
        while (true)
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
        }
    }
}
