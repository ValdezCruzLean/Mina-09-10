using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    public MonoBehaviour scriptCamara;
    public GameObject MenuSalir;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pausa)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                scriptCamara.enabled = false;
               /* AudioSource[] sonidos = FindObjectOfType<AudioSource>();

                for (int i = 0; i< sonidos.Length; i++)
                {
                    sonidos[i].Pause();
                }*/
            }
            else
            {
                Resumir(); 
            }
        }
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        MenuSalir.SetActive(false);
        Pausa = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        scriptCamara.enabled = true;
       /* AudioSource[] sonidos = FindObjectOfType<AudioSource>();

        for (int i = 0; i < sonidos.Length; i++)
        {
            sonidos[i].Play();
        }*/
    }
    public void Menu(string MenuPrincipal)
    {
        SceneManager.LoadScene(MenuPrincipal);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Se salio campeon");
    }
}

