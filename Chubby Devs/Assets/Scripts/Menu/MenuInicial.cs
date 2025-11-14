using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    private AudioSource music;
    public AudioClip ClickAudio;
    public AudioClip switchAudio;

    private void Start()
    {
        music= GetComponent<AudioSource>();
    }
    public void ClickAudioOn() {
        music.PlayOneShot(ClickAudio);
    }

    public void SwitchAudioOn()
    {
        music.PlayOneShot(switchAudio);
    }
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
   
    public void Salir ()
    {
        Application.Quit();
        Debug.Log("Se salio loco");
    }
}
