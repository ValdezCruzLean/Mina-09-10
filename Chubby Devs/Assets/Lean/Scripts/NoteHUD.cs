using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NoteHUD : MonoBehaviour
{
    public static NoteHUD Instance;

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

        // Solo permitir navegación si el HUD está abierto
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
        noteText.text = playerNotes[currentIndex].noteText;
        counterText.text = $"Nota {currentIndex + 1}/{playerNotes.Count}";
    }
}

