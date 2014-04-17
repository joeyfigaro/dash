using UnityEngine;
using System.Collections;

public class EventsScript : MonoBehaviour {

	public float playerBaseSpeed = 10f;

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

		BeatEngine beatEngine = GetComponent<BeatEngine>();
		beatEngine.engage();
	}
}
