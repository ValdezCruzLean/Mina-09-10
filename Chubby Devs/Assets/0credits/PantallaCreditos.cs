using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaCreditos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Creditos");
    }
}
