using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDPresionaX : MonoBehaviour
{
    public TMP_Text textoHUD;

    public string nombreEscena = "MenuPrincipal";
    public KeyCode tecla = KeyCode.X;

    [Header("Desvanecimiento")]
    [Range(0.1f, 5f)]
    // Mas alto el valor de la velocidad mas rapido sera el fade
    public float velocidadFade = 1.5f; 

    private float alpha = 1f;
    private bool bajando = true;

    void Start()
    {
        if (textoHUD == null)
            textoHUD = GetComponent<TMP_Text>();
    }

    void Update()
    {
        EfectoFade();
        DetectarTecla();
    }

    void EfectoFade()
    {
        float cambio = velocidadFade * Time.deltaTime;

        if (bajando)
        {
            alpha -= cambio;
            if (alpha <= 0f)
            {
                alpha = 0f;
                bajando = false;
            }
        }
        else
        {
            alpha += cambio;
            if (alpha >= 1f)
            {
                alpha = 1f;
                bajando = true;
            }
        }

        Color color = textoHUD.color;
        color.a = alpha;
        textoHUD.color = color;
    }

    void DetectarTecla()
    {
        if (Input.GetKeyDown(tecla))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
