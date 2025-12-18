using UnityEngine;

public class MenuNivelesToggle : MonoBehaviour
{
    [Header("Botones de niveles")]
    public GameObject botonNivel1;
    public GameObject botonNivel2;

    private bool menuActivo = false;

    public void ToggleMenu()
    {
        menuActivo = !menuActivo;

        botonNivel1.SetActive(menuActivo);
        botonNivel2.SetActive(menuActivo);
    }
}
