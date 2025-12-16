using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPerder : MonoBehaviour
{
    [Header("Botones")]
    public GameObject botonInicial;   // Reintentar (recomendado)
    
    private Vector3 lastMousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botonInicial);

        lastMousePosition = Input.mousePosition;

        // Si usas UI con navegaci�n por teclado/gamepad, selecciona un bot�n

    }

    // Update is called once per frame
    void Update()
    {
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
            EventSystem.current.SetSelectedGameObject(botonInicial);
        }
    }
}
