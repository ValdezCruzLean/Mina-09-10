using UnityEngine;

public class DesbloquearZona : MonoBehaviour
{
    public void AbrirCamino()
    {
        Debug.Log("¡Zona desbloqueada! Desactivando pared...");
        
        gameObject.SetActive(false); 
    }
}
