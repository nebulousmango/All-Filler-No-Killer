using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Attached to DontDestroy object in 00_StartMenu scene.

    public static DontDestroy Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}