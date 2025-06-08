using UnityEngine;

public class NotePickup : MonoBehaviour
{
    public NoteData noteData;

    public void PickUp()
    {
        NoteInventory.Instance.AddNote(noteData);
        gameObject.SetActive(false);
    }
}