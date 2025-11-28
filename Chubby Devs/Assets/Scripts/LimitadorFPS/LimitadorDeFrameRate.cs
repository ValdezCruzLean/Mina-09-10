using UnityEngine;

public class LimitadorDeFrameRate : MonoBehaviour
{
    private int limiteDeFps = 30;
    private void Start()
    {
        Application.targetFrameRate = limiteDeFps;
    }
}