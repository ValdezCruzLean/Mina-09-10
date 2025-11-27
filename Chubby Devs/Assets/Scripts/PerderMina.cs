using UnityEngine;
using UnityEngine.SceneManagement;

public class PerderMina : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(5);
        }
    }
}
