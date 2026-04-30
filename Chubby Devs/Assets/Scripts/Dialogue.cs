using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI & Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI continuarDialogoText;

    public GameObject interactionPrompt;
    public GameObject imagenUI;
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

    private bool isTyping = false;

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
            imagenUI.SetActive(true);
            //string tecla = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
            //interactionText.text = $"Presiona {tecla} para hablar";
            if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
            {
                interactionText.text = "Presiona <sprite name=\"Icon_BotonB\"> para hablar";
            }
            else
            {
                interactionText.text = "Presiona <sprite name=\"Icon_E\"> para hablar";
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
            imagenUI.SetActive(false);
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
            //string tecla = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
            //continuarDialogoText.text = $"Presiona {tecla} para continuar";
            if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
            {
                continuarDialogoText.text = "Presiona <sprite name=\"Icon_BotonB\"> para continuar";
            }
            else
            {
                continuarDialogoText.text = "Presiona <sprite name=\"Icon_E\"> para continuar";
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;

        /*if (dialogueLines.Length > 0)
            dialogueText.text = dialogueLines[currentLineIndex];*/

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

       // if (npcAnimator != null)
           // npcAnimator.CrossFade(talkStateName, 0.1f);
         StopAllCoroutines();
         StartCoroutine(ShowLine());

    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[currentLineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }

    void AdvanceDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex]; 
            isTyping = false; 
        }
        else
        {
            currentLineIndex++;
            if (currentLineIndex < dialogueLines.Length)
            {
                StopAllCoroutines();
                StartCoroutine(ShowLine());
            }
            else
            {
                EndDialogue();
            }
        }

        /*currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            StopAllCoroutines(); // importante
            StartCoroutine(ShowLine());
            //dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }*/
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

        //ActivarObjetivoLampara();
        if (!manoActivadorEstaActiva()) 
        {
            ActivarObjetivoLampara();
        }
    }

    bool manoActivadorEstaActiva()
    {
        // Buscamos el objeto manoActivador que está en tu script RecoleccionObjeto
        if (scriptRecoleccion != null)
        {
            // Usamos una referencia a la variable manoActivador del otro script
            // Si ya está activa, significa que ya recogimos la lámpara
            // Nota: Asegúrate que 'manoActivador' en RecoleccionObjeto sea 'public' o usa una variable de control
            return scriptRecoleccion.manoActivadorYaEncendida; 
        }
        return false;
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

            StopAllCoroutines();

            if (isDialogueActive)
                EndDialogue();

            dialoguePanel.SetActive(false);

            if (npcAnimator != null)
                npcAnimator.CrossFade(idleStateName, 0.2f);
        }
    }
}
