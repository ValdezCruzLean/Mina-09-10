using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RecoleccionObjeto : MonoBehaviour
{
    [SerializeField] float distancia;
    [SerializeField] GameObject manoActivador;
    public CanvasFosforos canvasFosforos;
    public AnimacionLamparaMovimiento lamparaMovimiento;
    [SerializeField] Text recogerLampara, recogerAceite;
    private void Update()
    {
        recogerLampara.gameObject.SetActive(false);
        recogerAceite.gameObject.SetActive(false);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * distancia, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distancia))
        {
            if (hit.collider.CompareTag("LamparaViejo"))
            {
                recogerLampara.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    manoActivador.SetActive(true);
                    lamparaMovimiento.StartAnimation();
                    TimeLight.Instance.MostrarSoloSilueta();
                }
            }

            if (hit.collider.CompareTag("Fosforo"))
            {
                recogerAceite.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && canvasFosforos.cantidadFosforos < canvasFosforos.limiteFosforo)
                {
                    Destroy(hit.collider.gameObject);
                    canvasFosforos.SumarFosforo();
                }
            }
        }
    }
}