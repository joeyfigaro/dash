using UnityEngine;
using System.Collections;

public class EventsScript : MonoBehaviour {
	
	public int trailSpeed = 10;
	public int starsSpeed = 100;
	public Vector2 scrollSpeed = new Vector2(10, 10);

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
//		PlayerScript playerScript = player.GetComponentInChildren<PlayerScript>();
		ParticleSystem trail = player.GetComponentInChildren<ParticleSystem>();
		trail.startSpeed = trailSpeed;
		ScrollingScript scroller = player.GetComponentInChildren<ScrollingScript>();
		scroller.speed = scrollSpeed;

		ParticleSystem stars = GameObject.Find("Stars").GetComponentInChildren<ParticleSystem>();
		stars.startSpeed = starsSpeed;
	}
}
