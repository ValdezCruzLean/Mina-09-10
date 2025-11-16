using UnityEngine;
using TMPro;

public class DetectorEdificio : MonoBehaviour
{
    public TextMeshProUGUI buildingNameText; // Asignar desde el Inspector
    public float fadeDuration = 2f; // Tiempo que dura visible el texto

    private float timer = 0f;

    private void Update()
    {
        // Oculta el texto después de unos segundos
        if (buildingNameText.gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= fadeDuration)
            {
                buildingNameText.gameObject.SetActive(false);
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Edificio>(out Edificio building))
        {
            buildingNameText.text = building.buildingName;
            buildingNameText.gameObject.SetActive(true);
            timer = 0f;

            other.enabled = false;
        }
    }
}
