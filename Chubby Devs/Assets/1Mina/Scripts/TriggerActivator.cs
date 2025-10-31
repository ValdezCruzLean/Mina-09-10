using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    [Header("Objeto a activar o desactivar")]
    public GameObject objetoObjetivo;

    [Header("Tag del jugador")]
    public string tagJugador = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Si el que entra tiene el tag del jugador...
        if (other.CompareTag(tagJugador))
        {
            if (objetoObjetivo != null)
            {
                // Cambia su estado (activo/inactivo)
                //bool nuevoEstado = !objetoObjetivo.activeSelf;
                //objetoObjetivo.SetActive(nuevoEstado);
                objetoObjetivo.SetActive(true);
                //Debug.Log($"Objeto {objetoObjetivo.name} ahora está {(nuevoEstado ? "ACTIVO" : "INACTIVO")}");
            }
            else
            {
                Debug.LogWarning("No se asignó ningún objeto objetivo en el inspector.");
            }
        }
    }
}