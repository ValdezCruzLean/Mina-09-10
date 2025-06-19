using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    private void Update()
    {
        DeteccionObjetoRayCast(100, Color.blue, "fosforo");
    }
    public void DeteccionObjetoRayCast(int escalarRayCast, Color colorDeteccion, string NombreObjeto)
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       Debug.DrawRay(ray.origin, ray.direction * escalarRayCast, Color.red);
       RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.name == NombreObjeto)
        {
            Debug.DrawRay(ray.origin, ray.direction * escalarRayCast, colorDeteccion);
            Debug.Log("Objeto detectado: " + hit.collider.name);
        }
    }
}