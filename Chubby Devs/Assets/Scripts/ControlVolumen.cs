using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicaSlider;
    public Slider efectosSlider;
    public Slider screamersSlider;

    private void Awake()
    {
        musicaSlider.onValueChanged.AddListener(ControlMusicaVolumen);
        efectosSlider.onValueChanged.AddListener(ControlEfectoVolumen);
        screamersSlider.onValueChanged.AddListener(ControlScreamersVolumen);
    }
    private void Start()
    {
        Cargar();
    }
    private void ControlMusicaVolumen(float valor)
    {
        mixer.SetFloat("VolumenMusica", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenMusica", musicaSlider.value);
    }
    private void ControlEfectoVolumen(float valor)
    {
        mixer.SetFloat("VolumenEfectos", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenEfectos", efectosSlider.value);
    }

  
    private void ControlScreamersVolumen(float valor)
    {
        mixer.SetFloat("VolumenScreamers", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenScreamers", screamersSlider.value);
    }

    private void Cargar()
    {
        musicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
        efectosSlider.value = PlayerPrefs.GetFloat("VolumenEfectos", 0.75f);
        screamersSlider.value = PlayerPrefs.GetFloat("VolumenScreamers", 0.75f); 

        ControlMusicaVolumen(musicaSlider.value);
        ControlEfectoVolumen(efectosSlider.value);
        ControlScreamersVolumen(screamersSlider.value);
    }
}