using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCheckpoints : MonoBehaviour
{
    // Lista de checkpoints que el prefab debe seguir
    public Transform[] checkpoints;
    // Velocidad de movimiento
    public float speed = 5f;
    // Velocidad de rotación
    public float rotationSpeed = 5f;
    // Índice del checkpoint actual
    private int currentCheckpointIndex = 0;

    // Umbral para considerar que llegó al checkpoint
    public float checkpointRadius = 0.1f;

    void Update()
    {
        // Verificar que haya checkpoints asignados
        if (checkpoints.Length == 0)
        {
            Debug.LogWarning("No checkpoints assigned.");
            return;
        }

        // Obtener el checkpoint actual
        Transform targetCheckpoint = checkpoints[currentCheckpointIndex];

        // Mover el prefab hacia el checkpoint usando MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, targetCheckpoint.position, speed * Time.deltaTime);

        // Dirección hacia el siguiente checkpoint
        Vector3 direction = (targetCheckpoint.position - transform.position).normalized;

        // Si la dirección no es cero, rotar hacia esa dirección
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            // Rotación suave hacia la nueva dirección
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Comprobar si el prefab ha alcanzado el checkpoint
        if (Vector3.Distance(transform.position, targetCheckpoint.position) < checkpointRadius)
        {
            // Cambiar al siguiente checkpoint
            currentCheckpointIndex++;

            // Si hemos alcanzado el último checkpoint, reiniciar
            if (currentCheckpointIndex >= checkpoints.Length)
            {
                currentCheckpointIndex = 0; // O puedes detener el movimiento si prefieres
            }
        }
    }
}
