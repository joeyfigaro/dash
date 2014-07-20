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
	void OnGUI()
	{
				if (gameOver) {
				
						const int buttonWidth = 120;
						const int buttonHeight = 60;
		
		
		
						if (
			GUI.Button (
			// Center in X, 1/3 of the height in Y
			new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			(1 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
						),
			btnRetryTexture
						)
			) {
								// Reload the level
								Application.LoadLevel ("level");
						}
		
						if (
			GUI.Button (
			// Center in X, 2/3 of the height in Y
			new Rect (
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
						),
			"Back to menu"
						)
			) {
								// Reload the level
								Application.LoadLevel ("intro-3-full");
						}
				}
		}
}
