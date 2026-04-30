using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class RecoleccionObjeto : MonoBehaviour
{
    [SerializeField] float distancia;
    public GameObject manoActivador;
    public CanvasFosforos canvasFosforos;
    public AnimacionLamparaMovimiento lamparaMovimiento;
    //[SerializeField] Text recogerLampara, recogerAceite;
    [SerializeField] TextMeshProUGUI recogerLampara, recogerAceite;
    public GameObject imagenUI;
    private bool mensajeLamparaMostrado = false;
    public bool manoActivadorYaEncendida = false;

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
                /*recogerLampara.text = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) 
                    ? "Presiona (B) para recoger" 
                    : "Presiona [E] para recoger";*/
                
                if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
                {
                    recogerLampara.text = "Presiona <sprite name=\"Icon_BotonB\"> para recoger";
                }
                else
                {
                    recogerLampara.text = "Presiona <sprite name=\"Icon_E\"> para recoger";
                }

                recogerLampara.gameObject.SetActive(true);
                imagenUI.gameObject.SetActive(true);

                if (recogerInput)
                {
                    Destroy(hit.collider.gameObject);
                    manoActivador.SetActive(true);

                    manoActivadorYaEncendida = true;

                    lamparaMovimiento.StartAnimation();
                    TimeLight.Instance.MostrarSoloSilueta();

                    if (OnboardingManager.Instance != null)
                    {
                        OnboardingManager.Instance.MostrarConsejo("Recoge el aceite.");
                    }
                }
            }

            if (hit.collider.CompareTag("Fosforo"))
            {
                /*recogerAceite.text = (ScriptGameManager.CurrentDevice == InputDevice.Joystick) 
                    ? "Recoger aceite (B)" 
                    : "Recoger aceite [E]";*/

                if (ScriptGameManager.CurrentDevice == InputDevice.Joystick)
                {
                    recogerAceite.text = "Recoger aceite <sprite name=\"Icon_BotonB\">";
                }
                else
                {
                    recogerAceite.text = "Recoger aceite <sprite name=\"Icon_E\">";
                }

                recogerAceite.gameObject.SetActive(true);
                imagenUI.gameObject.SetActive(true);


                if (recogerInput && canvasFosforos.cantidadFosforos < canvasFosforos.limiteFosforo)
                {
                    Destroy(hit.collider.gameObject);
                    canvasFosforos.SumarFosforo();

                    if (OnboardingManager.Instance != null && !mensajeLamparaMostrado)
                    {
                        OnboardingManager.Instance.MostrarConsejo("Aceite obtenido. Presiona {LIGHT} para encender la lámpara.");
                        mensajeLamparaMostrado = true;
                    }
                }
            }
        }
    }
}