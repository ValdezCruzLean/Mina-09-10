using UnityEngine;

public class ActivarUICuandoObjetosDesactivados : MonoBehaviour
{
    [Header("Objetos que deben desactivarse")]
    public GameObject[] objetosARevisar;

    [Header("UI a activar cuando todos estén desactivados")]
    public GameObject uiAActivar;

    private bool uiActivada = false;

    void Update()
    {
        if (!uiActivada && TodosDesactivados())
        {
            uiActivada = true;
            uiAActivar.SetActive(true);
        }
    }

    bool TodosDesactivados()
    {
        foreach (GameObject obj in objetosARevisar)
        {
            if (obj.activeSelf) // Si alguno está activo → aún no
                return false;
        }
        return true;
    }
}
