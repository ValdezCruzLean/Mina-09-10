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

    void OnTriggerEnter()
    {
        Scream.Play();
        JumpCam.SetActive(true);
        ThePlayer.SetActive(false);

        enemyScript.TriggerJump();
        movimientoJugador.GetComponent<FirstPersonController>().enabled = false;
        //FlashImg.SetActive(true);
        StartCoroutine(EndJump());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(2.03f);
        activador.SetActive(false);
        ThePlayer.SetActive(true);
        JumpCam.SetActive(false);

        //FlashImg.SetActive(false);
        SceneManager.LoadScene("EscenaPerder");
    }
}
