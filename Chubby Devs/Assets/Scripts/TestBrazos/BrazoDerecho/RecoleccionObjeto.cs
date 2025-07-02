using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;

public class RecoleccionObjeto : MonoBehaviour,IAnimationInteraction
{
    [SerializeField] float distancia;
    public Animator controller;

    public CanvasFosforos canvasFosforos;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * distancia, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distancia))
        {
            if (hit.collider.CompareTag("Fosforo") )
            {
                IAnimationInteraction("isRecoger", true);
                if (Input.GetKeyDown(KeyCode.R) && (canvasFosforos.cantidadFosforos <= canvasFosforos.limiteFosforo && canvasFosforos.cantidadFosforos != canvasFosforos.limiteFosforo))
                {
                    Destroy(hit.collider.gameObject);
                    canvasFosforos.SumarFosforo();
                }
            }
            else
            {
                IAnimationInteraction("isRecoger", false);
            }
        }
    }
    public void IAnimationInteraction(string _nameAnimation, bool _isActivo)
    {
        string nameAnimation = _nameAnimation;
        bool isActivo = _isActivo;
        controller.SetBool(nameAnimation, isActivo);
    }
}