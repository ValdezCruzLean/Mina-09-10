using UnityEngine;

public class ObjetosPuerta : MonoBehaviour
{
    public GameObject[] objetos;   // Los 6 objetos a recoger
    public GameObject objetoInvisible;  // El objeto que abre la puerta

    void Update()
    {
        bool todosDesactivados = true;

        foreach (GameObject obj in objetos)
        {
            if (obj.activeSelf)   // Si alguno sigue activo
            {
                todosDesactivados = false;
                break;
            }
        }

        if (todosDesactivados)
        {
            objetoInvisible.SetActive(false); // Desactiva el objeto invisible
            enabled = false; // Para que no siga revisando
        }
    }
}
