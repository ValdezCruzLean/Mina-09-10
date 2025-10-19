using UnityEngine;

public class JumpScareEnemy : MonoBehaviour
{
    public Transform targetPosition; // Un Empty frente a la cámara
    public float jumpSpeed = 10f;
    private bool isJumping = false;

    void Update()
    {
        if (isJumping)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, jumpSpeed * Time.deltaTime);
        }
    }

    public void TriggerJump()
    {
        isJumping = true;
    }
}
