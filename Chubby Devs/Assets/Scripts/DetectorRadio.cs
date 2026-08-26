using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DetectorRadio : MonoBehaviour
{
    [Header("Configuración de Detección")]
    [Tooltip("Tag que tienen los enemigos que activan la estática.")]
    public string tagEnemigo = "Enemigo";
    
    [Tooltip("Distancia máxima a la que la radio empieza a captar la presencia del enemigo.")]
    public float distanciaMaximaDeteccion = 15f;
    
    [Tooltip("Distancia a la que la estática sonará al 100% de volumen.")]
    public float distanciaMinimaSaturacion = 2f;

    [Header("Configuración de Audio")]
    [Range(0f, 1f)]
    public float volumenMaximoEstatica = 0.8f;

    private AudioSource audioSource;
    private GameObject[] enemigos;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0f; 
        
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("⚠️ Por favor, asigna un clip de estática al AudioSource de la Radio.");
        }
    }

    void Update()
    {
        enemigos = GameObject.FindGameObjectsWithTag(tagEnemigo);

        if (enemigos.Length == 0)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 2f);
            return;
        }

        float distanciaMasCercana = Mathf.Infinity;
        foreach (GameObject enemigo in enemigos)
        {

            if (enemigo.activeInHierarchy)
            {
                float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
                if (distancia < distanciaMasCercana)
                {
                    distanciaMasCercana = distancia;
                }
            }
        }

        if (distanciaMasCercana <= distanciaMaximaDeteccion)
        {

            float factorCercania = Mathf.InverseLerp(distanciaMaximaDeteccion, distanciaMinimaSaturacion, distanciaMasCercana);
            
            float volumenObjetivo = factorCercania * volumenMaximoEstatica;

            audioSource.volume = Mathf.Lerp(audioSource.volume, volumenObjetivo, Time.deltaTime * 4f);
        }
        else
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 2f);
        }
    }
}
