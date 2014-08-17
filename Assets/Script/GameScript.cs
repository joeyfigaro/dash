using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	public static GameScript Instance;

	public bool gameOver = false;

	public GameObject cameraBox;

	public float playerBaseSpeed = 30f;

	void Start () {
		StartCoroutine(engage());
//		StartCoroutine(hyperMode());
//		BeatsEngine.Instance.randomizeColor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator engage() {
		for( float timer = 0; timer <= 2; timer += Time.deltaTime)
			yield return 0;

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		PlayerScript ps = player.GetComponent<PlayerScript>();
		ps.baseSpeed = playerBaseSpeed;

		BeatsEngine.Instance.engage();
	}
	
	IEnumerator hyperMode() {
		Debug.Log ("Calling hypermode Start");
		for( float timer = 0; timer <= 5; timer += Time.deltaTime)
			yield return 0;
		Debug.Log ("Calling hypermode");
		cameraBox.GetComponent<CameraScript>().hyperMode();
	}

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of GameScript!");
		}
		Instance = this;
	}

	public Texture2D btnRetryTexture;
	public Texture2D btnMenuTexture;
	public Texture2D imgRainCloudTexture;
	public GUIStyle dashGUIStyle;
	private BeatsEngine beatsEngine;
	public GameObject lightningBolt;
	public GameObject rainDrops;


	void OnGUI()
	{
				if (gameOver) {
						beatsEngine = GetComponent<BeatsEngine> ();
						const int buttonWidth = 256;
						const int buttonHeight = 128;
						int rainCloudWidth = (Screen.width / 2);
						int rainCloudHeight = (Screen.height / 2);



		
						if (
				GUI.Button (
					new Rect (
						Screen.width - (Screen.width / 16) - (buttonWidth),
						(3 * Screen.height / 5) - (buttonHeight / 2),
						buttonWidth,
						buttonHeight
						),
					btnRetryTexture,
					dashGUIStyle
						)) {
								// Reload the level
								Application.LoadLevel ("level");
						}
		
						if (
				GUI.Button (
					new Rect (
						Screen.width - (Screen.width / 16) - (buttonWidth),
						(4 * Screen.height / 5) - (buttonHeight / 2),
						buttonWidth,
						buttonHeight
						),
					btnMenuTexture,
					dashGUIStyle
						)) {
								// Reload the level
								Application.LoadLevel ("intro-3-full");
						}

						// Displays the final score
						GUI.Label (
			new Rect (Screen.width - (Screen.width / 16) - (buttonWidth), (2 * Screen.height / 5) - (buttonHeight / 2),
			          buttonWidth,
			          buttonHeight
						),
					"Final Score: " + beatsEngine.bpm, 
					dashGUIStyle);

						// Decorative "Game Over" Raincloud
						GUI.Label (
				new Rect ((Screen.width / 2) - (rainCloudWidth / 2), 10,
			          rainCloudWidth,
			          rainCloudHeight
						),
					imgRainCloudTexture,
					dashGUIStyle);
						 
				}
		}

		public void gameOverAnimation()
		{
		GameObject lightning = Instantiate(lightningBolt) as GameObject;
		lightning.transform.parent = cameraBox.transform;
		lightningBolt.transform.localPosition = new Vector3(3, 5, 1);

		GameObject rain = Instantiate(rainDrops) as GameObject;
		rain.transform.parent = cameraBox.transform;
		rainDrops.transform.localPosition = new Vector3(4, 2, 1);
		}
	
}