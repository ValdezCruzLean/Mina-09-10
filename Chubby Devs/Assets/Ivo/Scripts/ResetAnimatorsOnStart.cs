using UnityEngine;

public class ResetAnimatorsOnStart : MonoBehaviour
{
    public Animator animator;
    public string estadoInicial = "Presentacion";

    void Awake()
    {
        animator.Rebind();
        animator.Update(0f);
        animator.Play(estadoInicial, 0, 0f);
    }
}
