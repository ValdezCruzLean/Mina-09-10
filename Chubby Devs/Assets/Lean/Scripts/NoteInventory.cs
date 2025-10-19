using System.Collections.Generic;
using UnityEngine;

public class NoteInventory : MonoBehaviour
{
    public static NoteInventory Instance;
    private List<NoteData> notes = new List<NoteData>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddNote(NoteData note)
    {
        if (!notes.Contains(note))
        {
            notes.Add(note);
            notes.Sort((a, b) => a.name.CompareTo(b.name));
            NoteHUD.Instance.ShowSingleNote(note);
        }
    }

    public List<NoteData> GetNotes()
    {
        return notes;
    }
}