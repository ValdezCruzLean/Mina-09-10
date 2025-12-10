using UnityEngine;

public class DoorController : MonoBehaviour
{
    //Configuraciones de la puerta 
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool openForward = true;
    public bool openRight = false;

    //Puerta cerrada (tiene candado)
    public GameObject unlockObject; // Objeto a vigilar (candado)

    private bool openDoor = false;
    private bool hasOpened = false;

    private Quaternion targetRotation;

    void Start()
    {
        // Calculamos direccion de apertura
        Vector3 axis = Vector3.up;

        if (!openForward) axis = -axis;
        if (openRight) axis = -axis;

        targetRotation = Quaternion.Euler(transform.eulerAngles + axis * openAngle);

        if (unlockObject != null)
        {
            Debug.Log(" Objeto de desbloqueo: " + unlockObject.name);
        }
        else
        {
            Debug.LogWarning("No se hay ningun objeto de desbloqueo. La puerta se abrira de inmediato.");
            openDoor = true;
        }
    }

    void Update()
    {
        // Si el objeto fue desactivado la puerta se abre
        if (!openDoor && unlockObject != null && !unlockObject.activeInHierarchy)
        {
            Debug.Log("El objeto fue desactivado y la puerta se esta abriendo");
            openDoor = true;
        }

        // Si el objeto fue destruido la puerta se abre 
        if (!openDoor && unlockObject == null)
        {
            Debug.Log("El objeto fue destruido y la puerta se esta abriendo");
            openDoor = true;
        }

        // Movimiento de apertura
        if (openDoor && !hasOpened)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                hasOpened = true;
                Debug.Log(" Puerta abierta.");
            }
        }
    }
}
