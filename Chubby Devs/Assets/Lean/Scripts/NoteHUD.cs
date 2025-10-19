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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (hudPanel.activeSelf)
                CloseHUD();
            else if (playerNotes.Count > 0)
                OpenHUD();
        }

        // Solo permitir navegaci�n si el HUD est� abierto
        if (hudPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                NextNote();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousNote();
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
       
    }
}

