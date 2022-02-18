using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager musicManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (musicManager == null)
            musicManager = this;
        else
            Destroy(gameObject);
    }
}
