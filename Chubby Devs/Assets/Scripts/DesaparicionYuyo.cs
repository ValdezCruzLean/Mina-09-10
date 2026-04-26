using UnityEngine;

public class DesaparicionYuyo : MonoBehaviour
{
    public GameObject objetoADesaparecer;
    public Transform personaje;          // ← único transform a rotar
    public Transform puntoObjetivo;      // ← hacia dónde debe mirar
    public float velocidadRotacion = 2f;

    private int contadorEntradas = 0;
    private bool girando = false;
    private Quaternion rotObjetivo;

    public NPCDialogue npcDialogue;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (npcDialogue != null && !npcDialogue.dialogoCompletado)
        return;

        contadorEntradas++;

        if (contadorEntradas == 1)
        {
            if (objetoADesaparecer != null)
                objetoADesaparecer.SetActive(false);

            // --- LÓGICA DE ONBOARDING ---
            if (OnboardingManager.Instance != null)
            {
                OnboardingManager.Instance.MostrarConsejo("Busca indicios de tus amigos.");
            }
            // ----------------------------

            Vector3 dir = puntoObjetivo.position - personaje.position;
            dir.y = 0f; // solo girar en Y

            rotObjetivo = Quaternion.LookRotation(dir);

            girando = true;
        }
    }

    void Update()
    {
        if (!girando) return;

        personaje.rotation = Quaternion.RotateTowards(
            personaje.rotation,
            rotObjetivo,
            velocidadRotacion * Time.deltaTime * 60f
        );

        if (Quaternion.Angle(personaje.rotation, rotObjetivo) < 0.5f)
            girando = false;
    }
}
