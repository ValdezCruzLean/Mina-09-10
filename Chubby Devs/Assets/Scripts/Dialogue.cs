using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 6)] public string[] dialogueLines;
    public MonoBehaviour playerMovementScript; // Asigná acá el script de movimiento del jugador

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isDialogueActive = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        dialogueText.text = dialogueLines[currentLineIndex];

        if (playerMovementScript != null)
            playerMovementScript.enabled = false; // Desactiva movimiento
    }

    void AdvanceDialogue()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true; // Reactiva movimiento
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (isDialogueActive)
                EndDialogue(); // Cierra si el jugador se va

            dialoguePanel.SetActive(false);
        }
    }
}
