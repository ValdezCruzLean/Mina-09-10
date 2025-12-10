using UnityEngine;

[CreateAssetMenu(fileName = "NewNote", menuName = "Notes/Note")]
public class NoteData : ScriptableObject
{
    [TextArea(5, 10)]
    public string noteText;
    public int noteID;// Numero para ordenar
    public Sprite noteImage; // imagen personalizada
}