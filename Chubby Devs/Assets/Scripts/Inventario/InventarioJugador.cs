using UnityEngine;
using UnityEngine.UI;

public class InventarioJugador : MonoBehaviour
{
    public GameObject objetoSlot1;
    public GameObject objetoSlot2;
    public GameObject objetoSlot3;

    public Image iconoSlot1;
    public Image iconoSlot2;
    public Image iconoSlot3;

    public Sprite iconoEspinas;
    public Sprite iconoAguaBendita;
    public Sprite iconoObjeto3;

    public GameObject seleccionSlot1;
    public GameObject seleccionSlot2;
    public GameObject seleccionSlot3;

    public int slotSeleccionado = 1;

    void Start()
    {
        iconoSlot1.gameObject.SetActive(false);
        iconoSlot2.gameObject.SetActive(false);
        iconoSlot3.gameObject.SetActive(false);

        seleccionSlot1.SetActive(false);
        seleccionSlot2.SetActive(false);
        seleccionSlot3.SetActive(false);

        slotSeleccionado = 0;
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && objetoSlot1 != null)
        {
            slotSeleccionado = 1;
            ActualizarSeleccion();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && objetoSlot2 != null)
        {
            slotSeleccionado = 2;
            ActualizarSeleccion();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && objetoSlot3 != null)
        {
            slotSeleccionado = 3;
            ActualizarSeleccion();
        }
    }

    public void AgregarObjeto(GameObject objeto)
    {
        ObjetoInventario objetoInventario = objeto.GetComponent<ObjetoInventario>();

        if (objetoInventario == null)
        {
            Debug.LogError("El objeto no tiene el componente ObjetoInventario.");
            return;
        }

        if (objetoSlot1 == null)
        {
            objetoSlot1 = objeto;

            MostrarIcono(iconoSlot1, objetoInventario.tipo);

            objeto.SetActive(false);
        }
        else if (objetoSlot2 == null)
        {
            objetoSlot2 = objeto;

            MostrarIcono(iconoSlot2, objetoInventario.tipo);

            objeto.SetActive(false);
        }
        else if (objetoSlot3 == null)
        {
            objetoSlot3 = objeto;

            MostrarIcono(iconoSlot3, objetoInventario.tipo);

            objeto.SetActive(false);
        }
        else
        {
            Debug.Log("El inventario está lleno.");
        }
    }

    void MostrarIcono(Image imagen, TipoObjeto tipo)
    {
        if (tipo == TipoObjeto.Espinas)
        {
            imagen.sprite = iconoEspinas;
        }
        else if (tipo == TipoObjeto.AguaBendita)
        {
            imagen.sprite = iconoAguaBendita;
        }
        else if (tipo == TipoObjeto.Objeto3)
        {
            imagen.sprite = iconoObjeto3;
        }

        imagen.gameObject.SetActive(true);
    }

    void ActualizarSeleccion()
    {
        seleccionSlot1.SetActive(false);
        seleccionSlot2.SetActive(false);
        seleccionSlot3.SetActive(false);

        if (slotSeleccionado == 1)
        {
            seleccionSlot1.SetActive(true);
        }
        else if (slotSeleccionado == 2)
        {
            seleccionSlot2.SetActive(true);
        }
        else if (slotSeleccionado == 3)
        {
            seleccionSlot3.SetActive(true);
        }
    }

    public void ConsumirObjetoSeleccionado()
{
    if (slotSeleccionado == 1)
    {
        if (objetoSlot1 != null)
        {
            Destroy(objetoSlot1);
            objetoSlot1 = null;
        }

        iconoSlot1.sprite = null;
        iconoSlot1.gameObject.SetActive(false);

        seleccionSlot1.SetActive(false);

        slotSeleccionado = 0;
    }
    else if (slotSeleccionado == 2)
    {
        if (objetoSlot2 != null)
        {
            Destroy(objetoSlot2);
            objetoSlot2 = null;
        }

        iconoSlot2.sprite = null;
        iconoSlot2.gameObject.SetActive(false);

        seleccionSlot2.SetActive(false);

        slotSeleccionado = 0;
    }
    else if (slotSeleccionado == 3)
    {
        if (objetoSlot3 != null)
        {
            Destroy(objetoSlot3);
            objetoSlot3 = null;
        }

        iconoSlot3.sprite = null;
        iconoSlot3.gameObject.SetActive(false);

        seleccionSlot3.SetActive(false);

        slotSeleccionado = 0;
    }
}

}
