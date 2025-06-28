using UnityEngine;

public class AnimationInteraction : MonoBehaviour
{
    public Animator animator;
    //public MonoBehaviour[] controlesDeshabilitar;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
/*if (Input.GetKeyDown(KeyCode.E)) 
        {
            animator.SetBool("isRecoger", true);
            // Desactivar controles
            /*foreach (var ctrl in controlesDeshabilitar)
                ctrl.enabled = false;   */   
        
        //animator.SetBool("isRecoger",false);