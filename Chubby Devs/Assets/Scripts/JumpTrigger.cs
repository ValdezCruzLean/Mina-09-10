using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpTrigger : MonoBehaviour
{
    public AudioSource Scream;
    public GameObject ThePlayer;
    public GameObject JumpCam;

    public JumpScareEnemy enemyScript;

    public GameObject activador;
    public GameObject movimientoJugador;
    //public GameObject FlashImg;

    public Animator transitionAnimator;


    void OnTriggerEnter(Collider other)
    {
        Scream.Play();
        JumpCam.SetActive(true);
        ThePlayer.SetActive(false);

        enemyScript.TriggerJump();
        movimientoJugador.GetComponent<FirstPersonController>().enabled = false;
        //FlashImg.SetActive(true);
        StartCoroutine(JumpCam.GetComponent<CameraShake>().Shake(1.0f, 0.3f));
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(2.03f);
        
        ThePlayer.SetActive(true);
        JumpCam.SetActive(false);

        transitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1.0f);

        //FlashImg.SetActive(false);
        SceneManager.LoadScene("EscenaPerder");
        activador.SetActive(false);
    }
}
