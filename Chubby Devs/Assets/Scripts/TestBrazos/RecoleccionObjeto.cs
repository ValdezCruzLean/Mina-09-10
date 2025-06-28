using UnityEngine;

public class RecoleccionObjeto : MonoBehaviour
{
    [SerializeField]float distancia;
    public AnimationInteraction AnimationInteraction;

    public CanvasFosforos canvasFosforos;
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
                if (Input.GetKeyDown(KeyCode.E) && (canvasFosforos.cantidadFosforos <= canvasFosforos.limiteFosforo && canvasFosforos.cantidadFosforos != canvasFosforos.limiteFosforo))
                {
                    Destroy(hit.collider.gameObject);
                    canvasFosforos.SumarFosforo();
                }
            }
            else
            {
                AnimationInteraction.animator.SetBool("isRecoger", false);
            }
        }
    }
}