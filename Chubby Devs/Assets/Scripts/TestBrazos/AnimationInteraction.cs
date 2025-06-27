using UnityEngine;
using DG.Tweening;
using System;

public class AnimationInteraction : MonoBehaviour
{
    public Transform objetivo;
    public MonoBehaviour[] controlesDeshabilitar;

    private bool enMovimiento = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !enMovimiento)
        {
            Vector3 posicionOriginal = transform.position;
            Quaternion rotacionOriginal = transform.rotation;

            enMovimiento = true;

            // Desactivar controles
            foreach (var ctrl in controlesDeshabilitar)
                ctrl.enabled = false;

            Sequence secuencia = DOTween.Sequence();

            secuencia.Append(transform.DOMove(objetivo.position, 1f).SetEase(Ease.InOutSine));
            secuencia.Join(transform.DORotateQuaternion(objetivo.rotation, 1f));

            secuencia.AppendInterval(1f);

            secuencia.Append(transform.DOMove(posicionOriginal, 1f).SetEase(Ease.InOutSine));
            secuencia.Join(transform.DORotateQuaternion(rotacionOriginal, 1f));

            secuencia.OnComplete(() =>
            {
                foreach (var ctrl in controlesDeshabilitar)
                    ctrl.enabled = true;

                enMovimiento = false;
            });
        }
    }
}