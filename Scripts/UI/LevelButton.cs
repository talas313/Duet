using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int level;

    private UnityEngine.UI.Button button;
    private void Start()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);
    }
    
}
