using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PauseUIManager : MonoBehaviour
{
    RectTransform rectTransform;

    #region Getter
    static PauseUIManager instance;
    public static PauseUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PauseUIManager>();
            return instance;
        }
    }
    #endregion Getter

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosX(rectTransform.rect.width, 0f);
    }

    public void Show(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(0, 0.3f).SetDelay(delay).SetUpdate(true);
    }

    public void Hide(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(rectTransform.rect.width, 0.3f).SetDelay(delay).SetUpdate(true);
        Time.timeScale = 1;
    }
    public void ShowHomeScreen()
    {
        StartCoroutine(LoadScene());
    }
    private System.Collections.IEnumerator LoadScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        // Wait until the level finish loading
        Time.timeScale = 1;
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
    }
}
