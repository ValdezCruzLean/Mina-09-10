using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RecargaLamp : MonoBehaviour, IAnimationInteraction
{
    public Animator controller;
    public TimeLight timeLight;
    public bool isRecarga = false;
    public CanvasFosforos canvasFosforos;
    private void Update()
    {
        Recarga();
    }
    public void Recarga()
    {
        if (timeLight.readyToReset == false)
        {
            IAnimationInteraction("isRecargaLamp", true);
        }
        else
        {
            IAnimationInteraction("isRecargaLamp", false);
        }
    }
    public void IAnimationInteraction(string _nameAnimation, bool _isActivo)
    {
        string nameAnimation = _nameAnimation;
        bool isActivo = _isActivo;
        controller.SetBool(nameAnimation, isActivo);
    }
}