using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool openForward = true;
    public bool openRight = false;

    [Header("Door Unlock Object")]
    public GameObject unlockObject; // Objeto a vigilar (candado, llave, etc.)

    private bool openDoor = false;
    private bool hasOpened = false;

    private Quaternion targetRotation;

    void Start()
    {
        // Calculamos dirección de apertura
        Vector3 axis = Vector3.up;

        if (!openForward) axis = -axis;
        if (openRight) axis = -axis;

        targetRotation = Quaternion.Euler(transform.eulerAngles + axis * openAngle);

        if (unlockObject != null)
        {
            Debug.Log("🔒 Objeto de desbloqueo asignado: " + unlockObject.name);
        }
        else
        {
            Debug.LogWarning("⚠ No se asignó ningún objeto de desbloqueo. La puerta se abrirá inmediatamente.");
            openDoor = true;
        }
    }

    void Update()
    {
        // ✔ Si el objeto fue desactivado
        if (!openDoor && unlockObject != null && !unlockObject.activeInHierarchy)
        {
            Debug.Log("✔ El objeto fue DESACTIVADO → Abriendo puerta...");
            openDoor = true;
        }

        // ✔ Si el objeto fue destruido
        if (!openDoor && unlockObject == null)
        {
            Debug.Log("✔ El objeto fue DESTRUIDO → Abriendo puerta...");
            openDoor = true;
        }

        // Movimiento de apertura
        if (openDoor && !hasOpened)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                hasOpened = true;
                Debug.Log("✅ Puerta completamente abierta.");
            }
        }
    }
}
