using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPerder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;

        EventSystem.current.SetSelectedGameObject(null);

        // Si usas UI con navegación por teclado/gamepad, selecciona un botón

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
