using UnityEngine;
using System.Collections;

public class EventsScript : MonoBehaviour {
	
	public float trailLifetime = .1f;
	public float starsSpeed = 100f;
	public Vector2 scrollSpeed = new Vector2(10f, 10f);

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
//		ps.baseSpeed = .1f;
		ps.baseSpeed = 10f;
//		ParticleSystem trail = player.GetComponent<ParticleSystem>();
//		trail.startLifetime = trailLifetime;

//		ScrollingScript scroller = player.GetComponentInChildren<ScrollingScript>();
//		scroller.speed = scrollSpeed;

//		Rigidbody2D body = player.GetComponent<Rigidbody2D>();
//		body.velocity = new Vector2(50f, 0f);

		
		GameObject terrain = GameObject.Find("Terrain");
		terrain.GetComponent<TerrainGeneration>().startGateGeneration();

		ParticleSystem stars = GameObject.Find("Stars").GetComponentInChildren<ParticleSystem>();
//		stars.startSpeed = starsSpeed;
	}
}
