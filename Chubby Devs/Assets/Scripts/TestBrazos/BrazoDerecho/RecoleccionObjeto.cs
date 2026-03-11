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

        bool recogerInput =
            Input.GetKeyDown(KeyCode.E) ||
            Input.GetKeyDown(KeyCode.JoystickButton1); // 🎮 B


        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * distancia, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distancia))
        {
            if (hit.collider.CompareTag("LamparaViejo"))
            {
                recogerLampara.text = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) 
                    ? "Presiona (B) para recoger" 
                    : "Presiona [E] para recoger";

                recogerLampara.gameObject.SetActive(true);

                if (recogerInput)
                {
                    Destroy(hit.collider.gameObject);
                    manoActivador.SetActive(true);
                    lamparaMovimiento.StartAnimation();
                    TimeLight.Instance.MostrarSoloSilueta();
                }
            }

            if (hit.collider.CompareTag("Fosforo"))
            {
                recogerAceite.text = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) 
                    ? "Recoger aceite (B)" 
                    : "Recoger aceite [E]";

                recogerAceite.gameObject.SetActive(true);

                if (recogerInput && canvasFosforos.cantidadFosforos < canvasFosforos.limiteFosforo)
                {
                    Destroy(hit.collider.gameObject);
                    canvasFosforos.SumarFosforo();
                }
            }
        }
    }
}