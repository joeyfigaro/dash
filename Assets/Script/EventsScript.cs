using UnityEngine;
using System.Collections;

public class EventsScript : MonoBehaviour {

	public float playerBaseSpeed = 10f;

	// Use this for initialization
	void Start () {
//		StartCoroutine(engage());
		StartCoroutine(hyperMode());
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

		BeatsEngine beatsEngine = GetComponent<BeatsEngine>();
		beatsEngine.engage();
	}

	IEnumerator hyperMode() {
		Debug.Log ("Calling hypermode Start");
		for( float timer = 0; timer <= 5; timer += Time.deltaTime)
			yield return 0;
		Debug.Log ("Calling hypermode");
		Camera.main.GetComponent<CameraScript>().hyperMode();
	}
}
