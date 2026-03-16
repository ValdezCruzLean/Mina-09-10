using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI & Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI continuarDialogoText;

    public GameObject interactionPrompt;
    public TextMeshProUGUI interactionText;


    [TextArea(3, 6)] public string[] dialogueLines;

    [Header("Player")]
    public MonoBehaviour playerMovementScript;

    [Header("Animator")]
    public Animator npcAnimator;
    public string idleStateName = "Iddle"; 
    public string talkStateName = "Talk";

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isDialogueActive = false;

    public bool dialogoCompletado { get; private set; }

    public RecoleccionObjeto scriptRecoleccion;

    void Start()
    {
        if (npcAnimator != null)
            npcAnimator.Play(idleStateName, 0, 0f);
    }

    void Update()
    {
        bool interaccionInput =
            Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.JoystickButton1); // 🎮 B



        if (playerInRange && !isDialogueActive)
        {
            interactionPrompt.SetActive(true);
            string tecla = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
            interactionText.text = $"Presiona {tecla} para hablar";
        }
        else
        {
            interactionPrompt.SetActive(false);
        }    
            

        if (isDialogueActive)
        {
            ActualizarTextoContinuar();
        }

        if (playerInRange && interaccionInput)
        {
            if (!isDialogueActive) StartDialogue();
            else AdvanceDialogue();
        }
    }

    void ActualizarTextoContinuar()
    {
        if (continuarDialogoText != null)
        {
            string tecla = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
            continuarDialogoText.text = $"Presiona {tecla} para continuar";
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;

        if (dialogueLines.Length > 0)
            dialogueText.text = dialogueLines[currentLineIndex];

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        if (npcAnimator != null)
            npcAnimator.CrossFade(talkStateName, 0.1f);
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
            playerMovementScript.enabled = true;

        if (npcAnimator != null)
            npcAnimator.CrossFade(idleStateName, 0.2f);

        dialogoCompletado = true;

        ActivarObjetivoLampara();
    }

    void ActivarObjetivoLampara()
    {
        if (scriptRecoleccion != null)
        {
            scriptRecoleccion.enabled = true;

            if (OnboardingManager.Instance != null)
            {
                OnboardingManager.Instance.MostrarConsejo("Recoge la lámpara.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (isDialogueActive)
                EndDialogue();

            dialoguePanel.SetActive(false);

            if (npcAnimator != null)
                npcAnimator.CrossFade(idleStateName, 0.2f);
        }
    }
}
