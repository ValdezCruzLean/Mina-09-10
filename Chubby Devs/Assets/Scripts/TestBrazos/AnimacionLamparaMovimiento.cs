using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class AnimacionLamparaMovimiento : MonoBehaviour
{
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] Animator animatorController;
    string animationStateName = "LevantarLampara";

    public void StartAnimation()
    {
        firstPersonController.cameraCanMove = false;
        firstPersonController.playerCanMove = false;
        animatorController.SetBool("isAnimating", true);
        StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        yield return null;
        while (!animatorController.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
        {    
            yield return null;
        }
        float duration = animatorController.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);
        EndAnimation();
    }

    public void EndAnimation() 
    {
        firstPersonController.cameraCanMove = true;
        firstPersonController.playerCanMove = true;
        animatorController.SetBool("isAnimating", false);
    }
}