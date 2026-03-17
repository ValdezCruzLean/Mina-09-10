using UnityEngine;

public class NPConboarding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnboardingManager.Instance.MostrarConsejo("Habla con el extraño.");
            Destroy(gameObject); // Para que solo aparezca una vez
        }
    }
}
