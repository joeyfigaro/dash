using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = player.transform.position;
		Vector3 vel = player.rigidbody2D.velocity;

		float newField = 10f + (vel.x / 10f);
		Camera.main.orthographicSize = Mathf.Lerp(camera.orthographicSize,newField,Time.deltaTime * 5f);

		float cameraLeftPixel = Camera.main.ViewportToWorldPoint(
			new Vector3(-1, 0, 0)
			).x;
		pos.x = pos.x + ((pos.x - cameraLeftPixel) * (newField - 10) / 60);
		
		pos.x = Mathf.Lerp(transform.position.x, pos.x, Time.deltaTime * newField);
		pos.z = transform.position.z;
		transform.position = pos;
	}
}
