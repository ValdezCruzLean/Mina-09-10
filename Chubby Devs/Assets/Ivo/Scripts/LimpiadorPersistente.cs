using UnityEngine;

public class LimpiadorPersistente : MonoBehaviour
{
    void Awake()
    {
        var objs = FindObjectsByType<GameObject>(
            FindObjectsSortMode.None
        );
        
        foreach (var obj in objs)
        {
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }
    }
}
