using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
public class NoteHUD : MonoBehaviour
{
    public static NoteHUD Instance;
    public Image noteImage; //imagen de la nota
    public GameObject hudPanel;
    public TextMeshProUGUI noteText;
    public TextMeshProUGUI counterText;

    [SerializeField] private TextMeshProUGUI textoCerrarHUD;

    private int currentIndex = 0;
    private List<NoteData> playerNotes => NoteInventory.Instance.GetNotes();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        hudPanel.SetActive(false);
    }

    void Update()
    {
         bool toggleHUD =
        Input.GetKeyDown(KeyCode.Tab) ||
        Input.GetKeyDown(KeyCode.JoystickButton2); // 🎮 X

        if (toggleHUD)
        {
            if (hudPanel.activeSelf)
                CloseHUD();
            else if (playerNotes.Count > 0)
                OpenHUD();
        }

        // Solo permitir navegacion si el HUD esta abierto
        if (hudPanel.activeSelf)
        {
            ActualizarTextoControles();    

            bool next =
                Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.JoystickButton5); // 🎮 RB

            bool prev =
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.JoystickButton4); // 🎮 LB

            if (next)
                NextNote();

            if (prev)
                PreviousNote();
        }
    }

    void ActualizarTextoControles()
    {
        if (textoCerrarHUD == null) return;

        if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
        {
            textoCerrarHUD.text = "Cerrar (X)";
        }
        else
        {
            textoCerrarHUD.text = "Cerrar [TAB] ";
        }
    }

    public void ShowSingleNote(NoteData note)
    {
        hudPanel.SetActive(true);
        noteImage.sprite = note.noteImage;
        noteImage.enabled = note.noteImage != null;
        noteText.text = note.noteText;
        counterText.text = $"Nueva nota";
        Time.timeScale = 0f;
    }

    public void OpenHUD()
    {
        currentIndex = 0;
        UpdateHUD();
        hudPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseHUD()
    {
        hudPanel.SetActive(false);
        Time.timeScale = 1f;
    }

   public void NextNote()
    {
        currentIndex = (currentIndex + 1) % playerNotes.Count;
        UpdateHUD();
    }

    public void PreviousNote()
    {
        currentIndex = (currentIndex - 1 + playerNotes.Count) % playerNotes.Count;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        var note = playerNotes[currentIndex];
        noteImage.sprite = note.noteImage;
        noteImage.enabled = note.noteImage != null;
        noteText.text = playerNotes[currentIndex].noteText;
        counterText.text = $"Nota {currentIndex + 1}/{playerNotes.Count}";
       
       ActualizarTextoControles();
    }
}

