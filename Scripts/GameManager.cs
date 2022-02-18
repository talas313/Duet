using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	#endregion

	[HideInInspector] public bool isGameover = false;

	[SerializeField] UnityEngine.UI.Toggle musicToggle;
	[SerializeField] UnityEngine.UI.Toggle soundEffectsToggle;
	[SerializeField] UnityEngine.UI.Toggle paintToggle;
	[SerializeField] UnityEngine.UI.Toggle backgroundToggle;

	public GameObject music;
	public GameObject background;

	public static bool isMusicOn = true;
	public static bool isSoundEffectsOn = true;
	public static bool isPaintOn = true;
	public static bool isBackgroundOn = true;

	private void Start()
    {
		music = GameObject.Find("Audio");
		background = GameObject.Find("Background");

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
			musicToggle = GameObject.Find("MusicToggle").GetComponent<UnityEngine.UI.Toggle>();
			soundEffectsToggle = GameObject.Find("SoundEffectsToggle").GetComponent<UnityEngine.UI.Toggle>();
			paintToggle = GameObject.Find("PaintToggle").GetComponent<UnityEngine.UI.Toggle>();
			backgroundToggle = GameObject.Find("BackgroundToggle").GetComponent<UnityEngine.UI.Toggle>();

			musicToggle.isOn = isMusicOn;
			soundEffectsToggle.isOn = isSoundEffectsOn;
			paintToggle.isOn = isPaintOn;
			backgroundToggle.isOn = isBackgroundOn;
		}
		
    }
}
