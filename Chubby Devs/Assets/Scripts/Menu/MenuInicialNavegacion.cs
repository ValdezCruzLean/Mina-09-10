using UnityEngine;
using UnityEngine.EventSystems;

public class MenuInicialNavegacion : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelMenuPrincipal;
    public GameObject panelControles;
    public GameObject panelOpciones;

    [Header("Botones")]
    public GameObject botonMenuPrincipal;
    public GameObject botonVolverControles;
    public GameObject botonVolverOpciones;

    private Vector3 lastMousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AbrirMenuPrincipal();
    }

    // Update is called once per frame
    void Update()
    {
        bool backInput =
        Input.GetKeyDown(KeyCode.Escape) ||
        Input.GetKeyDown(KeyCode.JoystickButton1); // B

        // 🔙 PRIORIDAD: volver si estamos en submenú
        if (backInput)
        {
            if (panelControles.activeSelf || panelOpciones.activeSelf)
            {
                AbrirMenuPrincipal();
                return;
            }
        }

        // 🖱️ Mouse rompe selección
        if ((Input.mousePosition - lastMousePosition).sqrMagnitude > 1f)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        lastMousePosition = Input.mousePosition;

        // 🎮 Recuperar selección con joystick
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool joystickMoved = Mathf.Abs(h) > 0.3f || Mathf.Abs(v) > 0.3f;

        if (joystickMoved && EventSystem.current.currentSelectedGameObject == null)
        {
            if (panelMenuPrincipal.activeSelf)
                EventSystem.current.SetSelectedGameObject(botonMenuPrincipal);
            else if (panelControles.activeSelf)
                EventSystem.current.SetSelectedGameObject(botonVolverControles);
            else if (panelOpciones.activeSelf)
                EventSystem.current.SetSelectedGameObject(botonVolverOpciones);
        }
    }

    public void AbrirMenuPrincipal()
    {
        panelMenuPrincipal.SetActive(true);
        panelControles.SetActive(false);
        panelOpciones.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botonMenuPrincipal);
    }

    public void AbrirControles()
    {
        panelMenuPrincipal.SetActive(false);
        panelControles.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botonVolverControles);
    }

    public void AbrirOpciones()
    {
        panelMenuPrincipal.SetActive(false);
        panelOpciones.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botonVolverOpciones);
    }

}
