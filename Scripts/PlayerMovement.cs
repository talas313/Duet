using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
	#region Singleton class: PlayerMovement

	public static PlayerMovement Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	#endregion

	[SerializeField] CircleCollider2D redBallCollider;
	[SerializeField] CircleCollider2D blueBallCollider;

	[SerializeField] GameObject obstacle;

	static int repeatedTimes = 1;

	private float speed = 2f;
	private float rotationSpeed = 250f;

	Rigidbody2D rb;

	Vector3 startPosition;

	Camera cam;

	float touchPosX = 0f;

	private AudioClip soundEffect;
	private AudioSource audioSource;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();

		startPosition = transform.position;

		cam = Camera.main;

		audioSource = GetComponent<AudioSource>();

		soundEffect = (AudioClip)Resources.Load("SoundEffect");

		obstacle = GameObject.Find("Obstacles");

		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            RotateRight();
        }
        else
        {
            MoveUp();
        } 
	}

	void Update ()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0 && !GameManager.Instance.isGameover) {
			//Add mobile inputs (touch on screen sides)
			if (Input.GetMouseButtonDown (0))
				touchPosX = cam.ScreenToWorldPoint (Input.mousePosition).x;

            if (Input.GetMouseButton (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
			{
				if (touchPosX > 0.01f)
					RotateRight();
				else
					RotateLeft();
            } else
				rb.angularVelocity = 0f;


			//Unity editor inputs < & > keys
			#if UNITY_EDITOR
			if (Input.GetKey (KeyCode.LeftArrow))
				RotateLeft ();
			else if (Input.GetKey (KeyCode.RightArrow))
				RotateRight ();

			//stop rotation when key is released
			if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow))
				rb.angularVelocity = 0f;
			#endif

		}
	}

	void MoveUp ()
	{
		rb.velocity = Vector2.up * speed;
	}

	void RotateLeft ()
	{
		rb.angularVelocity = rotationSpeed;
	}

	void RotateRight ()
	{
		rb.angularVelocity = -rotationSpeed;
	}

	public void Restart ()
	{
		if (GameManager.isSoundEffectsOn)
		{
			audioSource.clip = soundEffect;
			audioSource.Play();

		}

		redBallCollider.enabled = false;
		blueBallCollider.enabled = false;
		rb.angularVelocity = 0f;
		rb.velocity = Vector2.zero;


		//back to start position
		transform
			.DORotate (Vector3.zero, 1f)
			.SetDelay (1f)
			.SetEase (Ease.InOutBack);

		transform
			.DOMove (startPosition, 1f)
			.SetDelay (1f)
			.SetEase (Ease.OutFlash)

			.OnComplete (() => {
			redBallCollider.enabled = true;
			blueBallCollider.enabled = true;

			GameManager.Instance.isGameover = false;

            if (repeatedTimes == 3)
            {

				//AdMobScript.Instance.ShowInterstitialAd();
				//AdMobScript.Instance.RequestInterstitial();
				//repeatedTimes = 1;
				StartCoroutine(AdRoutine());
            }
            else
            {
				repeatedTimes++;
            }

			MoveUp ();
		});
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("LevelEnd")) {
			Destroy (other.gameObject);

			if(obstacle != null)			
			obstacle.SetActive(false);

            rb.angularVelocity = 0f;
            rb.velocity = Vector2.zero;

            //back to start position
            transform
				.DORotate(Vector3.zero, 1f)
				.SetDelay(1f)
				.SetEase(Ease.InOutBack);

			transform
				.DOMove(startPosition, 1f)
				.SetDelay(1f)
				.SetEase(Ease.OutFlash)

				.OnComplete(() => {

					//AdMobScript.Instance.ShowInterstitialAd();
					//AdMobScript.Instance.RequestInterstitial();
					//repeatedTimes = 1;
					StartCoroutine(AdRoutine());
					StartCoroutine(LoadScene());
				});
		}
	}
	private System.Collections.IEnumerator AdRoutine()
    {
		AdMobScript.Instance.ShowInterstitialAd();

		while (!AdMobScript.isAdClosed)
			yield return null;

		yield return new WaitForEndOfFrame();

		AdMobScript.Instance.RequestInterstitial();

		repeatedTimes = 1;
	}
	private System.Collections.IEnumerator LoadScene()
	{
		while (!AdMobScript.isAdClosed)
			yield return null;

		yield return new WaitForEndOfFrame();

		int currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

		AdMobScript.isAdClosed = false;

		if (currentLevelIndex == 5)
		{
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
		}
		else if (currentLevelIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(++currentLevelIndex);
	}
}
