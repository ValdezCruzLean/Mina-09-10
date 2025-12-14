using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    public MonoBehaviour scriptCamara;
    public GameObject MenuSalir;

    //public GameObject primerBoton;
    private Vector3 lastMousePosition;

    [Header("Panels")]
    public GameObject panelPausa;
    public GameObject panelOpciones;

    [Header("UI")]
    public GameObject primerBoton;
    public GameObject primerBotonOpciones;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        bool pausaInput =
        Input.GetKeyDown(KeyCode.Escape) ||
        Input.GetKeyDown(KeyCode.JoystickButton7);

        if (pausaInput)
        {
            if (panelOpciones.activeSelf)
            {
                panelOpciones.SetActive(false);
                EventSystem.current.SetSelectedGameObject(primerBoton);
                return;
            }
            if (!Pausa)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                scriptCamara.enabled = false;

                EventSystem.current.SetSelectedGameObject(primerBoton);
                lastMousePosition = Input.mousePosition;
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

        if (Pausa)
        {
            if ((Input.mousePosition - lastMousePosition).sqrMagnitude > 1f)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            lastMousePosition = Input.mousePosition;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            bool joystickMoved = Mathf.Abs(h) > 0.3f || Mathf.Abs(v) > 0.3f;

            if (joystickMoved && EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(primerBoton);
            }
        }
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        panelOpciones.SetActive(false);
        MenuSalir.SetActive(false);

        Pausa = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;

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

