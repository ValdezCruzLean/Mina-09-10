using UnityEngine;
using UnityEngine.UI;

public class CanvasFosforos : MonoBehaviour
{
    public int cantidadFosforos = 0;
    public int limiteFosforo;
    public bool isRecogerFosforo = true;
    [SerializeField] Text textoUI;

    private void Start()
    {
        textoUI.text = "Fosforos: " + cantidadFosforos.ToString();
    }
    public void SumarFosforo()
    {
        if (cantidadFosforos <= limiteFosforo && cantidadFosforos != limiteFosforo)
        {
            cantidadFosforos++;
            //isRecogerFosforo = true;
            textoUI.text = "Fosforos: " + cantidadFosforos.ToString();
        }
    }
}