using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;
    Quaternion desiredRotation;

    private void Update()
    {
        desiredRotation = target.rotation;
        transform.position = target.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}