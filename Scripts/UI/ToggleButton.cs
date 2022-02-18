using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Toggle toggle;

    public void ChangeValueOfMusic()
    {
        if (toggle.isOn)
        {
            GameManager.isMusicOn = true;
            GameManager.Instance.music.GetComponent<AudioSource>().Play();
        }
        else
        {
            GameManager.isMusicOn = false;
            GameManager.Instance.music.GetComponent<AudioSource>().Stop();
        }
    }
    public void ChangeValueOfSoundEffects()
    {
        if (toggle.isOn)
            GameManager.isSoundEffectsOn = true;
        else
            GameManager.isSoundEffectsOn = false;
    }
    public void ChangeValueOfPaint()
    {
        if (toggle.isOn)
            GameManager.isPaintOn = true;
        else
            GameManager.isPaintOn = false;
    }
    public void ChangeValueOfBackground()
    {
        if (toggle.isOn)
        {
            GameManager.isBackgroundOn = true;
            GameManager.Instance.background.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GameManager.isBackgroundOn = false;
            GameManager.Instance.background.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
