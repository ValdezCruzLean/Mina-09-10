using UnityEngine;
using TMPro;
public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance = 3f;
    public LayerMask interactLayer;
    public GameObject interactionUI;
    public GameObject imagenUI;
    [SerializeField] private TMP_Text textoUI;

    private NotePickup currentNote;

    /*no toque nada*/
    void Update()
    {
        bool recogerInput =
            Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.JoystickButton1); // 🎮 B
       // Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactLayer))
        {
            currentNote = hit.collider.GetComponent<NotePickup>();

            if (currentNote != null)
            {
                if (textoUI != null)
                {
                    //string tecla = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) ? "(B)" : "[E]";
                    //textoUI.text = $"Presiona {tecla} para leer nota";

                    if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
                    {
                        textoUI.text = "Presiona <sprite name=\"Icon_BotonB\"> para leer nota";
                    }
                    else
                    {
                        textoUI.text = "Presiona <sprite name=\"Icon_E\"> para leer nota";
                    }
                }

                interactionUI.SetActive(true);
                imagenUI.SetActive(true);

                if (recogerInput)
                {
                    currentNote.PickUp();
                    interactionUI.SetActive(false);
                    imagenUI.SetActive(false);
                    currentNote = null;
                }

                return;
            }
        }

        interactionUI.SetActive(false);
        imagenUI.SetActive(false);
        currentNote = null;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}