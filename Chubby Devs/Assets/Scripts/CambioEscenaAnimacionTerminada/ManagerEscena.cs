using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerEscena : MonoBehaviour
{
    public void CambiarEscena() 
    {
        SceneManager.LoadScene(2);
    }
}