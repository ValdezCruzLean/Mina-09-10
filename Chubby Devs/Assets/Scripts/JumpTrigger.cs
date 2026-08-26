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

    public GameObject blackFade;

    public GameObject HUD;
    public GameObject otroHUD;

    public GameObject bloodSplash;
    public float bloodFadeInTime = 0.12f;
    public float bloodFadeOutTime = 0.8f;
    public float bloodVisibleTime = 0.25f;

    public float bloodDelay = 0.20f;
    public EfectoVisionJugador efectoVision;

    public void ActivarJumpscareManualmente()
    {
        if (efectoVision != null)
        {
            efectoVision.LimpiarCegueraInmediata();
        }

        if (JumpCam != null)
        {
            JumpCam.SetActive(true);
        }
        OnTriggerEnter(null); 
    }
    void OnTriggerEnter(Collider other)
    {
        Scream.Play();
        JumpCam.SetActive(true);
        ThePlayer.SetActive(false);

        blackFade.SetActive(true);

        if (HUD != null && otroHUD !=null)
            HUD.SetActive(false);
            otroHUD.SetActive(false);


        enemyScript.TriggerJump();
        movimientoJugador.GetComponent<FirstPersonController>().enabled = false;
        //FlashImg.SetActive(true);
        //StartCoroutine(ShowBloodSplash());
        StartCoroutine(JumpCam.GetComponent<CameraShake>().Shake(1.0f, 0.3f));

        if (bloodSplash != null)
            //StartCoroutine(ShowBloodSplash());
            StartCoroutine(ShowBloodSplashWithDelay());

        StartCoroutine(EndJump());
    }

    IEnumerator ShowBloodSplashWithDelay()
    {
        yield return new WaitForSeconds(bloodDelay);  // <<< retrasa el efecto
        yield return StartCoroutine(ShowBloodSplash());
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(2.03f);
        
        

        transitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1.0f);

        ThePlayer.SetActive(true);
        JumpCam.SetActive(false);

        string escenaActual = SceneManager.GetActiveScene().name;

        if (escenaActual == "Mina")
        {
            SceneManager.LoadScene(5); // si está en Mina, cargar escena 5
        }
        else
        {
            SceneManager.LoadScene("EscenaPerder");  // si no, cargar EscenaPerder
        }
        //SceneManager.LoadScene("EscenaPerder");

        activador.SetActive(false);
    }

    IEnumerator ShowBloodSplash()
    {
        CanvasGroup cg = bloodSplash.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = bloodSplash.AddComponent<CanvasGroup>();
        }

        cg.alpha = 0f;
        bloodSplash.SetActive(true);

        // Fade in
        float t = 0f;
        while (t < bloodFadeInTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / bloodFadeInTime);
            yield return null;
        }
        cg.alpha = 1f;

        yield return new WaitForSeconds(bloodVisibleTime);

        // Fade out
        t = 0f;
        while (t < bloodFadeOutTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, t / bloodFadeOutTime);
            yield return null;
        }
        cg.alpha = 0f;
        bloodSplash.SetActive(false);
    }

}
