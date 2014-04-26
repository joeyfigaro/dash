using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	public static GameScript Instance;

	public float playerBaseSpeed = 30f;

	// Use this for initialization
	void Start () {
		StartCoroutine(engage());
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

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of GameScript!");
		}
		Instance = this;
	}
}
