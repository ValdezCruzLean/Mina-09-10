using UnityEngine;
using UnityEngine.UI;

public class TimeLight : MonoBehaviour
{
    public Text timeText;
    public float seconds = 0;
    public static TimeLight Instance;
    private bool readyToReset = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UpdateTime();
    }
    private void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= 15 && !readyToReset)
        {
            readyToReset = true;
        }
        if (readyToReset && Input.GetKeyDown(KeyCode.L))
        {
            seconds = 0;
            readyToReset = false;
        }
        UpdateTime();
    }
    public void UpdateTime() 
    {
        timeText.text = seconds.ToString("f0");
    }
}