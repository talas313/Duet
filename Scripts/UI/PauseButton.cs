using UnityEngine;

public class PauseButton : MonoBehaviour
{    
    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        PauseUIManager.Instance.Show();
    }
}
