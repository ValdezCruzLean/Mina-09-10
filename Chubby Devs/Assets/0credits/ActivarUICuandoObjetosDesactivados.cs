using UnityEngine;

public class ActivarUICuandoObjetosDesactivados : MonoBehaviour
{
    //Array de objetos que deben desactivarse
    public GameObject[] objetosARevisar;

    //UI a activar cuando todos esten desactivados
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
            if (obj.activeSelf) // Si alguno de los objetos esta activo ann no se visualiza ui
                return false;
        }
        return true;
    }
}
