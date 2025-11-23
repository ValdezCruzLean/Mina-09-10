using UnityEngine;

public class DesactivarVariosObjetosAlTocarPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] objetosADesactivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objetosADesactivar)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
        }
    }
}

