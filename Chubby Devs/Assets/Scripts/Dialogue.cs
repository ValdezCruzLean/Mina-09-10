using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI & Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 6)] public string[] dialogueLines;

    [Header("Player")]
    public MonoBehaviour playerMovementScript;

    [Header("Animator")]
    public Animator npcAnimator;
    public string idleStateName = "Iddle"; // pon EXACTO el nombre del estado
    public string talkStateName = "Talk";  // pon EXACTO el nombre del estado

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isDialogueActive = false;

    void Start()
    {
        if (npcAnimator == null)
            Debug.LogWarning("NPCDialogue: npcAnimator no asignado.");
        // Asegurarnos de que empiece en idle
        if (npcAnimator != null)
            npcAnimator.Play(idleStateName, 0, 0f);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueActive) StartDialogue();
            else AdvanceDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        if (dialogueLines.Length > 0)
            dialogueText.text = dialogueLines[currentLineIndex];

        if (playerMovementScript != null) playerMovementScript.enabled = false;

        if (npcAnimator != null)
        {
            // Forzamos el estado Talk en la layer 0 sin transición (0s)
            npcAnimator.CrossFade(talkStateName, 0f, 0, 0f);
        }
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

        if (playerMovementScript != null) playerMovementScript.enabled = true;

        if (npcAnimator != null)
        {
            // Volvemos al idle forzadamente
            npcAnimator.CrossFade(idleStateName, 0f, 0, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (isDialogueActive) EndDialogue();
            dialoguePanel.SetActive(false);

            if (npcAnimator != null)
                npcAnimator.CrossFade(idleStateName, 0f, 0, 0f);
        }
    }
}
