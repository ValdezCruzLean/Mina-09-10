using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFosforos : MonoBehaviour
{
    public int cantidadFosforos = 0;
    public int limiteFosforo;
    public Text textoUI;
    public Lamp Lamp;
    public bool quiereEncenderLampara = false;

    private void Start()
    {
        ActualizarTexto();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (cantidadFosforos > 0)
            {
                quiereEncenderLampara = true;
            }
            else
            {
                textoUI.text = "No tiene fosforos";
            }
        }
        else
        {
            quiereEncenderLampara = false;
        }
    }

    public void SumarFosforo()
    {
        if (cantidadFosforos < limiteFosforo)
        {
            cantidadFosforos++;
            ActualizarTexto();
        }
    }

    public void RestarFosforo()
    {
        if (cantidadFosforos > 0)
        {
            cantidadFosforos--;
            ActualizarTexto();
        }
    }

    public bool TieneFosforo()
    {
        return cantidadFosforos > 0;
    }

    private void ActualizarTexto()
    {
        textoUI.text = "Fosforos: " + cantidadFosforos;
    }
}