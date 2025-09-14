using UnityEngine;
using UnityEngine.UI;

public class cambioCamara : MonoBehaviour
{
    public Animator animator;
    private bool state = false;

    public void CambioPosicion() 
    {
        state = !state;
        animator.SetBool("MenuOpciones?", state);
    }
}