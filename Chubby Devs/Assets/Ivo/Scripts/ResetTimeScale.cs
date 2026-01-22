using UnityEngine;

public class ResetTimeScale : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
