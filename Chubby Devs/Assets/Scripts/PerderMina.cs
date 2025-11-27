using UnityEngine;
using UnityEngine.SceneManagement;

public class PerderMina : MonoBehaviour
{
    public Animator transitionAnimator; 
    public float duracionFade = 1f;
    public int escenaPerder = 5;

    private bool yaPerdio = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaPerdio)
        {
            yaPerdio = true;
            StartCoroutine(TransicionPerder());
        }
    }

    private System.Collections.IEnumerator TransicionPerder()
    {
        // Activar fade
        transitionAnimator.SetTrigger("StartTransition");

        // Esperar que se reproduzca
        yield return new WaitForSeconds(duracionFade);

        // Cargar escena
        SceneManager.LoadScene(escenaPerder);
    }
}
