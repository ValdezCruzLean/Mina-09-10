using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance = 3f;
    public LayerMask interactLayer;
    public GameObject interactionUI;

    private NotePickup currentNote;

    void Update()
    {
       // Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactLayer))
        {
            currentNote = hit.collider.GetComponent<NotePickup>();

            if (currentNote != null)
            {
                interactionUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentNote.PickUp();
                    interactionUI.SetActive(false);
                    currentNote = null;
                }

                return;
            }
        }

        interactionUI.SetActive(false);
        currentNote = null;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}