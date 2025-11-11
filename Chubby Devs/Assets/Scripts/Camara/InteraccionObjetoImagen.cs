using UnityEngine;
using UnityEngine.UI;

public class InteraccionObjetoImagen : MonoBehaviour
{
    public float rayDistance = 3f;
    [SerializeField]LayerMask interactLayer;
    [SerializeField] Image handImage;
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, interactLayer))
        {
            handImage.enabled = true;
        }
        else
        {
            handImage.enabled = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}