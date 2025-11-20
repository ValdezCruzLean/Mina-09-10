using UnityEngine;

public class AutoRotateToTarget : MonoBehaviour
{
    [Header("Target al que debe rotar el jugador")]
    public Transform targetToLook;

    [Header("Velocidad de rotación automática")]
    public float rotationSpeed = 5f;

    private bool isRotating = false;
    private bool hasRotatedOnce = false;   // ← NUEVO: evita que vuelva a rotar

    private MonoBehaviour playerLookScript;

    private void Start()
    {
        playerLookScript = GetComponentInChildren<MonoBehaviour>();
    }

    private void Update()
    {
        if (isRotating && targetToLook != null)
        {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (targetToLook.position - transform.position).normalized;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isRotating = false;

            if (targetToLook.gameObject.activeSelf)
                targetToLook.gameObject.SetActive(false);

            if (playerLookScript != null)
                playerLookScript.enabled = true;

            hasRotatedOnce = true;  // ← marca que ya se usó
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // si ya rotó una vez, no volver a activar nada
        if (hasRotatedOnce) return;

        if (other.CompareTag("Wendigo"))
        {
            if (playerLookScript != null)
                playerLookScript.enabled = false;

            isRotating = true;
        }
    }
}
