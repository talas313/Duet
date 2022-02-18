using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private static BackgroundManager backgroundManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (backgroundManager == null)
            backgroundManager = this;
        else
            Destroy(gameObject);
    }
}
