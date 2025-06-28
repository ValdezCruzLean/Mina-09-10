using UnityEngine;

public class RecoleccionObjeto : MonoBehaviour
{
    [SerializeField]float distancia;
    public AnimationInteraction AnimationInteraction;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * distancia, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distancia))
        {
            if (hit.collider.CompareTag("Fosforo") || hit.collider.CompareTag("Interactable"))
            {
                AnimationInteraction.animator.SetBool("isRecoger", true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                AnimationInteraction.animator.SetBool("isRecoger", false);
            }
        }
    }
}