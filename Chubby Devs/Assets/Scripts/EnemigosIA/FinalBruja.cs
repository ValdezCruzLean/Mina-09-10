using UnityEngine;

public class FinalBruja : MonoBehaviour
{
    public EnemigoAparece bruja;
    public ColocarEspinas colocarEspinas;

    private bool jugadorLlego = false;

    private void OnTriggerEnter(Collider other)
    {
        if (jugadorLlego)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            jugadorLlego = true;

            colocarEspinas.puedeColocar = true;

            Debug.Log("El jugador llegó al lugar final. ¡Puede preparar la trampa!");
        }
    }
}
