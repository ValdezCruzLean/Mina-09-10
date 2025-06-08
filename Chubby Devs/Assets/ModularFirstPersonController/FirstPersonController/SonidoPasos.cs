using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    public CharacterController controller;
    public AudioSource pasos;
    private bool Hactivo;
    private bool Vactivo;
    private bool enSuelo = false;
 
    void Update()
    {
        /*if(Input.GetButtonDown("Horizontal") && enSuelo)
        {
            Hactivo=true;
            pasos.Play();
        }
        if(Input.GetButtonDown("Vertical") && enSuelo)
        {
            Vactivo=true;
            pasos.Play();
        }
        if(Input.GetButtonUp("Horizontal"))
        {
            Hactivo=false;
            if(Vactivo==false)
            {
                pasos.Pause();
            }
        }
        if(Input.GetButtonUp("Vertical"))
        {
            Vactivo=false;
            if(Hactivo==false)
            {
                pasos.Pause();
            }
            
        }*/
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            pasos.Pause();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            pasos.Pause();
        }*/

        bool seMueve = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        bool corre = Input.GetKey(KeyCode.LeftShift);

        if (corre)
        {
            pasos.pitch = 1.5f;
        }
        else
        {
            pasos.pitch = 1f;
        }       

        if (seMueve && enSuelo)
        {
            if (!pasos.isPlaying)
                pasos.Play();
        }
        else
        {
            if (pasos.isPlaying)
                pasos.Pause();
        }
    }

    void OnCollisionStay(Collision collision){
        if(collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }
}
