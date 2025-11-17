using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;

public class RecoleccionObjeto : MonoBehaviour
{
    [SerializeField] float distancia;
    [SerializeField] GameObject manoActivador;
    public CanvasFosforos canvasFosforos;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * distancia, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distancia))
        {
            if (hit.collider.CompareTag("Fosforo"))
            {
                if (Input.GetKeyDown(KeyCode.E) && (canvasFosforos.cantidadFosforos <= canvasFosforos.limiteFosforo && canvasFosforos.cantidadFosforos != canvasFosforos.limiteFosforo))
                {
                    Destroy(hit.collider.gameObject);
                    canvasFosforos.SumarFosforo();
                }
            }
            if (hit.collider.CompareTag("LamparaViejo") && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(hit.collider.gameObject);
                manoActivador.SetActive(true);

                TimeLight.Instance.MostrarSoloSilueta();

                //TimeLight.Instance.MostrarBarraLampara(true);
                //TimeLight.Instance.ResetTimer();
            }
        }
    }
}